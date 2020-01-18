using System;
using UnityEngine;

namespace Logic {
    public class ReticleEventTrigger : MonoBehaviour
    {
        private long startTime;
        private long durationConstant = 3;
        private StorageEditor storageEditor = new StorageEditor();

        public void OnPointerEnter()
        {
            startTime = DateTime.Now.Ticks;
        }

        public void OnPointerExit(GameObject notification)
        {
            double duration = TimeSpan.FromTicks(DateTime.Now.Ticks - startTime).TotalSeconds;
            string name = "";
            if (duration >= durationConstant && name.Equals("Hide"))
            {
                //todo
                Notification toRemove = null;
                storageEditor.removeFromStorage(toRemove);
                //todo remove notification from scene 

            }   
            if (duration >= durationConstant && name.Equals("MarkAsRead"))
            {
                //todo remove notification from scene
            }
        }
    }
}
