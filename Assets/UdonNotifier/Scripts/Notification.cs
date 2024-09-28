
using System;
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


        /* Local Fields */
        private VRCPlayerApi localPlayer;
        private NotificationManager manager;
        private float eyeHeight;
        private AudioClip fadeOutSound;
        public Notification prevNotification, nextNotification;
 
        private float prevOffset;

        private float GetOffset()
        {
            if (manager.layout == NotificationLayout.Bottom)
                return 20f - Offset() * 10f * manager.scale;
            return Offset() * 10f * manager.scale - 20f;;
        }

        public void Update() 
        {
            var rot = GetOffset();
            prevOffset += (rot - prevOffset) * 0.1f;

            var head = localPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head);
            var dir = head.rotation * Quaternion.Euler(prevOffset, 0, 0);
            transform.position = head.position + dir * Vector3.forward * eyeHeight;
            transform.localScale = 0.001f * eyeHeight * manager.scale * Vector3.one;
            transform.rotation = head.rotation;
            
            if (localPlayer.IsUserInVR()) 
                transform.rotation *= Quaternion.Euler(prevOffset, 0, 0);
        }

        public override void OnAvatarEyeHeightChanged(VRCPlayerApi player, float prevEyeHeightAsMeters)
        {
            if (player.isLocal)
                eyeHeight = player.GetAvatarEyeHeightAsMeters();
        }

        public void Open(
            NotificationManager manager,
            Notification prevNotification, 
            string message, 
            NotificationType type, 
            float displayDuration, 
            AudioClip fadeInSound, 
            AudioClip fadeOutSound
        ) {
            this.prevNotification = prevNotification;
            if (prevNotification != null) 
                prevNotification.nextNotification = this;
 
            this.manager = manager;
            this.fadeOutSound = fadeOutSound;
            icon.sprite = icons[(int)type];
            ((TextMeshPro)text).text = message;
            localPlayer = Networking.LocalPlayer;
            eyeHeight = localPlayer.GetAvatarEyeHeightAsMeters();
            prevOffset = GetOffset();

            if (fadeInSound) audioSource.PlayOneShot(fadeInSound);
            animator.SetTrigger("open");
            SendCustomEventDelayedSeconds(nameof(Close), displayDuration);
        }

        public void Close()
        {
            if (fadeOutSound) audioSource.PlayOneShot(fadeOutSound);
            animator.SetTrigger("close");
            SendCustomEventDelayedSeconds(nameof(Delete), 0.25f);
        }

        public void Delete()
        {
            if (manager.prevNotification == this) manager.prevNotification = prevNotification;
            if (prevNotification != null) prevNotification.nextNotification = nextNotification;
            if (nextNotification != null) nextNotification.prevNotification = prevNotification;

            Destroy(gameObject);
        }

        private int Offset()
        {
            // root node
            if (prevNotification == null)
                return 0;
            
            return prevNotification.Offset() + 1;
        }

        public string GetText()
        {
            return ((TextMeshPro)text).text;
        }
    }
}
