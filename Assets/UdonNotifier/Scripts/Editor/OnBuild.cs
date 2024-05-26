using System;
using UnityEditor;
using UnityEngine;
using VRC.SDKBase.Editor.BuildPipeline;

namespace DeanCode 
{
    public class OnBuild : IVRCSDKBuildRequestedCallback
    {
        public int callbackOrder { get; }

        public bool OnBuildRequested(VRCSDKRequestedBuildType requestedBuildType)
        {
            var notifiers = GameObject.FindObjectsByType<UpdateNotifier>(FindObjectsSortMode.None);
            foreach (UpdateNotifier notifier in notifiers)
            {
                notifier.localVersion ++;
                notifier.localUpload = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                EditorUtility.SetDirty(notifier);
            }
            return true;
        }
    }
}