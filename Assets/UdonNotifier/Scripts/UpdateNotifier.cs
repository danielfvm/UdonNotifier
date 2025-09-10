using UdonSharp;
using UnityEngine;
using VRC.SDKBase;


namespace DeanCode 
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual), RequireComponent(typeof(NotificationManager))]
    public class UpdateNotifier : UdonSharpBehaviour 
    {
        [Header("Settings")]
        [SerializeField, Tooltip("Seconds time after player joined before showing the popup window.")] 
        private float messageDelay = 3;

        [SerializeField, Tooltip("Seconds to display message.")] 
        private float messageDuration = 10;


        [SerializeField, TextArea, Tooltip("Warning message to show when they joined an outdated instance.")] 
        private string displayMessage = "<color=#e76464>You joined an outdated instance, things might be broken!</color>";

        /* Local Fields */ 
        [HideInInspector] public int localVersion;
        [HideInInspector] public string localUpload;
        [UdonSynced, HideInInspector] public int instanceVersion;
        [UdonSynced, HideInInspector] public string instanceUpload;
 
        public void Start() 
        {
            if (Networking.LocalPlayer.isMaster) 
            {
                instanceVersion = localVersion;
                instanceUpload = localUpload;
                RequestSerialization();
                OnDeserialization();
            }
        }

        public bool IsUpToDate() => instanceVersion == localVersion;

        public int GetInstanceVersion() => instanceVersion;

        public int GetLocalVersion() => localVersion;

        public void CheckVersion()
        {
            if (IsUpToDate())
                return;

            var manager = GetComponent<NotificationManager>();
            manager._SendNotification(displayMessage, NotificationType.Error, messageDuration);
        }
 
        public override void OnDeserialization()
        {
            Debug.Log($"<color=#4287f5>[UpdateNotifier]</color> Local version: {localVersion} ({localUpload}), Instance version: {instanceVersion} ({instanceUpload})");
            SendCustomEventDelayedSeconds(nameof(CheckVersion), messageDelay);
        }
    }
}