using System;
using UdonSharp;
using UnityEngine;

namespace DeanCode
{
    [Serializable]
    public enum NotificationType
    {
        Info,
        Warning,
        Error,
        Player,
        Time
    }

    [Serializable]
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

        [HideInInspector] public Notification prevNotification;

        public Notification _SendNotification(string message, NotificationType type, float displayDuration = 5.0f)
        {
            if (string.IsNullOrEmpty(message)) return null;

            var notification = Instantiate(notificationPrefab.gameObject, transform).GetComponent<Notification>();

            notification._Open(this, prevNotification, message, type, displayDuration, defaultFadeIn, defaultFadeOut);
            prevNotification = notification;

            return notification;
        }

        public Notification _SendNotification(string message, NotificationType type, AudioClip fadeInSound, AudioClip fadeOutSound, float displayDuration = 5.0f)
        {
            if (string.IsNullOrEmpty(message)) return null;

            var notification = Instantiate(notificationPrefab.gameObject, transform).GetComponent<Notification>();

            notification._Open(this, prevNotification, message, type, displayDuration, fadeInSound, fadeOutSound);
            prevNotification = notification;

            return notification;
        }
    }
}