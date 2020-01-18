using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Logic {
    public class ReticleEventTrigger : MonoBehaviour
    {
        private long startTime;
        private long durationConstant = 3;
        private StorageEditor storageEditor = new StorageEditor();
        private SceneEditor sceneEditor = new SceneEditor();

        public void OnPointerEnter()
        {
            startTime = DateTime.Now.Ticks;
        }

        public void OnPointerExit(GameObject notification)
        {
            double duration = TimeSpan.FromTicks(DateTime.Now.Ticks - startTime).TotalSeconds;
            string id = notification.transform.Find("Id").GetComponent<TextMeshPro>().text;
            string sourceName = notification.transform.Find("Source").GetComponent<TextMeshPro>().text;
            string name = "";
            if (duration >= durationConstant && name.Equals("Hide"))
            {
                Dictionary<string, NotificationsStorage> orderedNotifications = storageEditor.removeFromStorage(id, sourceName);
                sceneEditor.rebuildScene(orderedNotifications);
            }   
            if (duration >= durationConstant && name.Equals("MarkAsRead"))
            {
                Dictionary<string, NotificationsStorage> orderedNotifications = storageEditor.removeFromStorage(id, sourceName);
                sceneEditor.rebuildScene(orderedNotifications);
                //todo open application
            }
        }
    }
}
