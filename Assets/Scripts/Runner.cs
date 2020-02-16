using System.Collections;
using UnityEngine;

namespace Logic
{
    public class Runner : MonoBehaviour
    {
        private NotificationsGenerator notificationsGenerator = new NotificationsGenerator();
        private StorageEditor storageEditor = new StorageEditor();
        private System.Random random = new System.Random();
        private Logger myLogger = new Logger(new LogHandler());
        public GameObject prefabToCreate;
        public GameObject trayPrefab;
        public bool isRunning;
        public int startRange;
        public int endRange;
        public int notificationsInColumn;
        public int notificationColumns;
        public string typeName;

        public void Update()
        {
            //if (Input.GetMouseButtonDown(0) && !Global.isTrayOpened)
            //{
            //    prefabToCreate = trayPrefab;
            //    Global.isTrayOpened = true;
            //}
            if (isRunning) StartCoroutine(Wait());
        }

        public IEnumerator Wait()
        {
            isRunning = false;
            int pause = random.Next(startRange, endRange + 1);
            Global.notificationColumns = notificationColumns;
            Global.notificationsInColumn = notificationsInColumn;
            Global.typeName = typeName;
            Global.prefabToCreate = prefabToCreate;
            Notification notification = notificationsGenerator.getNotification();
            storageEditor.addToStorage(notification);
            yield return new WaitForSeconds(pause);
            isRunning = true;
        }

        public void Start()
        {
            isRunning = true;
            myLogger.Log("Started");
        }

        public void Stop()
        {
            isRunning = false;
            myLogger.Log("Stopped");
        }   
    }
}
