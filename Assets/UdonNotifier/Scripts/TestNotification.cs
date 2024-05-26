
using UdonSharp;
using UnityEngine;

namespace DeanCode
{
    public class TestNotification : UdonSharpBehaviour
    {
        [SerializeField] private NotificationManager manager;
        [SerializeField] private string message;
        [SerializeField] private NotificationType type;
        
        public override void Interact()
        {
            manager.SendNotification(message, type);
        }
    }
}
