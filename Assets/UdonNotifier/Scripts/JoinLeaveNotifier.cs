
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace DeanCode 
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class JoinLeaveNotifier : UdonSharpBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float messageDuration = 3.0f;

        [SerializeField] private string joinMessage = "<player> <color=#85f48b>joined</color>";
        [SerializeField] private string leaveMessage = "<player> <color=#e76464>left</color>";

        [SerializeField] private AudioClip joinSound;
        [SerializeField] private AudioClip leaveSound;
        

        /* Local Fields */
        private NotificationManager manager;
        private float joinTime;


        public void Start()
        {
            manager = GetComponent<NotificationManager>();
            joinTime = Time.time;
        }

        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            if (!player.isLocal && Time.time < joinTime + 1)
                return;

            manager.SendNotification(
                joinMessage.Replace("<player>", player.displayName), 
                NotificationType.Player, 
                joinSound, null,
                messageDuration
            );
        }

        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            manager.SendNotification(
                leaveMessage.Replace("<player>", player.displayName), 
                NotificationType.Player, 
                leaveSound, null,
                messageDuration
            );
        }
    }
}
