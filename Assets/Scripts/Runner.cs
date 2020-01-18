using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace Logic
{
    public class Runner : MonoBehaviour
    {
        private NotificationsGenerator notificationsGenerator = new NotificationsGenerator();
        private StorageEditor storageEditor = new StorageEditor();
        private SceneEditor sceneEditor = new SceneEditor();
        private System.Random random = new System.Random();
        public GameObject prefabToCreate;
        public bool isRunning;
        public int secondsRange;
        public int notificationsInColumn;
        public int notificationColumns;
        private int count = 0;

        public void Update()
        {
            //if (isRunning)
            //{
            //    StartCoroutine(Wait());
            //}
            Wait();
        }

        //public IEnumerator Wait()
        void Wait()
        {
            if (count <= 7)
            {

                Application.OpenURL("tg://");
                //isRunning = false;
                //int pause = random.Next(1, secondsRange + 1);
                Global.maxNotificationsInTray = notificationsInColumn * notificationColumns;
                Global.prefabToCreate = prefabToCreate;
                Notification notification = notificationsGenerator.getNotification();
                Dictionary<string, NotificationsStorage> orderedNotifications = storageEditor.addToStorage(notification);
                sceneEditor.rebuildScene(orderedNotifications);
                count++;
                //yield return new WaitForSeconds(pause);
                //isRunning = true;
            }
        }

        public void Start()
        {
            isRunning = true;
        }

        public void Stop()
        {
            isRunning = false;
        }   
    }
}
