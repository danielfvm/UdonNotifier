using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace DeanCode 
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual), RequireComponent(typeof(NotificationManager))]
    public class JoinLeaveNotifier : UdonSharpBehaviour
    {
        [Header("Settings")]
        [SerializeField, Tooltip("The time in seconds the join/leave notification should be visible.")]
        private float messageDuration = 3.0f;
        
        [SerializeField, Tooltip("If true will show join and leave message to the local player as well.")]
        private bool showOwnJoinMessage;
        
        [SerializeField, Tooltip("Format of the message shown when a player joins. Use the <player> wildcard for the player name.")]
        private string joinMessage = "<player> <color=#85f48b>joined</color>";
        
        [SerializeField, Tooltip("Format of the message shown when a player leaves. Use the <player> wildcard for the player name.")] 
        private string leaveMessage = "<player> <color=#e76464>left</color>";

        [SerializeField, Tooltip("Sound being played when a player joins, leave null for no sound.")]
        private AudioClip joinSound;
        
        [SerializeField, Tooltip("Sound being played when a player leaves, leave null for no sound.")]
        private AudioClip leaveSound;
        

        private NotificationManager manager;

        public void Start()
        {
            manager = GetComponent<NotificationManager>();
        }

        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            if (player.isLocal && !showOwnJoinMessage) return;
            if (player.playerId < Networking.LocalPlayer.playerId) return;

            manager.SendNotification(
                joinMessage.Replace("<player>", player.displayName), 
                NotificationType.Player, 
                joinSound, null,
                messageDuration
            );
        }

        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            if (player.isLocal && !showOwnJoinMessage) return;
            
            manager.SendNotification(
                leaveMessage.Replace("<player>", player.displayName), 
                NotificationType.Player, 
                leaveSound, null,
                messageDuration
            );
        }
    }
}
