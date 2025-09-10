
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace DeanCode
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual), RequireComponent(typeof(NotificationManager))]
    public class WelcomeNotifier : UdonSharpBehaviour
    {
        [Header("Settings")]
        [SerializeField, Tooltip("The time in seconds the welcome message should be visible.")]
        private float messageDuration = 10.0f;
        
        [SerializeField, Tooltip("Message shown to a player joining the world.")] 
        private string welcomeMessage = "Welcome to the UdonNotifier prefab world!";

        [SerializeField, Tooltip("Welcome message sound effect")]
        private AudioClip welcomeSound = null;

        /* Local Fields */
        private NotificationManager manager;

        void Start()
        {
            manager = GetComponent<NotificationManager>();
            manager._SendNotification(
                welcomeMessage, 
                NotificationType.Info, 
                welcomeSound, null,
                messageDuration
            );
        }
    }
}