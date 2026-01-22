using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;

namespace DeanCode
{
    public class Notification : UdonSharpBehaviour
    {
        [Header("Internal References")]
        [SerializeField] private Animator animator;
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text text;
        [SerializeField] private AudioSource audioSource;

        [Header("Asset References")]
        [SerializeField] private Sprite[] icons;

        private VRCPlayerApi localPlayer;
        private NotificationManager manager;
        private float eyeHeight;
        private AudioClip fadeOutSound;

        public Notification prevNotification, nextNotification;

        private float prevOffset;

        private bool isClosed;
        private bool closeScheduled;

        private bool trackingActive;

        private const float BaseDepth = 20f;
        private const float Step = 10f;
        private const float Smooth = 0.1f;
        private const float ScaleMul = 0.001f;

        private float GetOffset()
        {
            if (manager.layout == NotificationLayout.Bottom)
                return BaseDepth - Offset() * Step * manager.scale;

            return Offset() * Step * manager.scale - BaseDepth;
        }

        private void _StartTracking()
        {
            if (trackingActive) return;
            trackingActive = true;
            _TrackingTick();
        }

        private void _StopTracking()
        {
            trackingActive = false;
        }

        public void _TrackingTick()
        {
            if (!trackingActive || isClosed || !manager || !Utilities.IsValid(localPlayer))
            {
                trackingActive = false;
                return;
            }

            var rot = GetOffset();
            prevOffset += (rot - prevOffset) * Smooth;

            var head = localPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head);

            var dir = head.rotation * Quaternion.Euler(prevOffset, 0f, 0f);

            transform.position = head.position + dir * Vector3.forward * eyeHeight;
            transform.localScale = (ScaleMul * eyeHeight * manager.scale) * Vector3.one;

            var r = head.rotation;
            if (localPlayer.IsUserInVR())
                r *= Quaternion.Euler(prevOffset, 0f, 0f);

            transform.rotation = r;

            SendCustomEventDelayedFrames(nameof(_TrackingTick), 1);
        }

        public override void OnAvatarEyeHeightChanged(VRCPlayerApi player, float prevEyeHeightAsMeters)
        {
            if (Utilities.IsValid(player) && player.isLocal)
                eyeHeight = player.GetAvatarEyeHeightAsMeters();
        }

        public void _Open(
            NotificationManager manager,
            Notification prevNotification,
            string message,
            NotificationType type,
            float displayDuration,
            AudioClip fadeInSound,
            AudioClip fadeOutSound
        )
        {
            this.prevNotification = prevNotification;
            if (prevNotification != null)
                prevNotification.nextNotification = this;

            this.manager = manager;
            this.fadeOutSound = fadeOutSound;

            isClosed = false;
            closeScheduled = false;

            if (icon != null) icon.sprite = icons[(int)type];
            text.text = message;

            localPlayer = Networking.LocalPlayer;
            eyeHeight = Utilities.IsValid(localPlayer) ? localPlayer.GetAvatarEyeHeightAsMeters() : 1.6f;
            prevOffset = GetOffset();

            if (fadeInSound) audioSource.PlayOneShot(fadeInSound);
            animator.SetTrigger("open");

            _StartTracking();

            if (displayDuration > 0f)
            {
                closeScheduled = true;
                SendCustomEventDelayedSeconds(nameof(_Close), displayDuration);
            }
        }

        public void _Close()
        {
            if (isClosed) return;

            isClosed = true;
            _StopTracking();

            if (fadeOutSound) audioSource.PlayOneShot(fadeOutSound);
            animator.SetTrigger("close");
            SendCustomEventDelayedSeconds(nameof(_Delete), 0.25f);
        }

        public void _Delete()
        {
            if (manager != null && manager.prevNotification == this) manager.prevNotification = prevNotification;
            if (prevNotification != null) prevNotification.nextNotification = nextNotification;
            if (nextNotification != null) nextNotification.prevNotification = prevNotification;

            Destroy(gameObject);
        }

        private int Offset()
        {
            if (prevNotification == null) return 0;
            return prevNotification.Offset() + 1;
        }

        public string _GetText() => text.text;
    }
}
