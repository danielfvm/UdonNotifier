
using UdonSharp;
using UnityEngine;

namespace DeanCode
{
    [SerializeField]
    public enum NotificationType 
    {
        Info,
        Warning,
        Error,
        Player,
        Time,
    }

    [SerializeField]
    public enum NotificationLayout
    {
        Bottom,
        Top,
    }
    
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class NotificationManager : UdonSharpBehaviour
    {
        [Header("Settings")]
        [SerializeField] public float scale = 1.0f;
        [SerializeField] public NotificationLayout layout = NotificationLayout.Bottom;

        [Header("Asset References")]
        [SerializeField] private Notification notificationPrefab;
        [SerializeField] private AudioClip defaultFadeIn, defaultFadeOut;

        /* Fields */
        [HideInInspector] public Notification prevNotification;

        public void SendNotification(string message, NotificationType type, float displayDuration = 5.0f)
        {
            if (message.Length == 0)
                return;

            var notification = Instantiate(notificationPrefab.gameObject, transform)
                .GetComponent<Notification>();

            notification.Open(this, prevNotification, message, type, displayDuration, defaultFadeIn, defaultFadeOut);
            prevNotification = notification;
        }

        public void SendNotification(string message, NotificationType type, AudioClip fadeInSound, AudioClip fadeOutSound, float displayDuration = 5.0f)
        {
            if (message.Length == 0)
                return;

            var notification = Instantiate(notificationPrefab.gameObject, transform)
                .GetComponent<Notification>();

            notification.Open(this, prevNotification, message, type, displayDuration, fadeInSound, fadeOutSound);
            prevNotification = notification;
        }
    }
}
