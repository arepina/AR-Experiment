using UnityEngine;

namespace Logic
{
    public class SceneRunner : MonoBehaviour
    {
        private Logger myLogger = new Logger(new LogHandler());
        public GameObject prefabToCreate;
        public GameObject trayPrefab;
        public int notificationsInColumn;
        public int notificationColumns;
        public string typeName;
        public float distanceFromCamera;
        public float angle;
        //for around staff only
        public float X;
        public float Y;
        public float Z;
        public bool isRunning;

        public void Start()
        {
            EventManager.AddHandler(EVENT.NotificationCreated, UpdateScene);
            isRunning = true;
            myLogger.Log("Started");
        }

        public void Stop()
        {
            isRunning = false;
            myLogger.Log("Stopped");
        }

        public void UpdateScene()
        {
            if (isRunning)
            {
                Global.notificationColumns = notificationColumns;
                Global.notificationsInColumn = notificationsInColumn;
                Global.typeName = typeName;
                Global.prefabToCreate = prefabToCreate;
                Global.aroundCoordinatesCenter = new Triple(X, Y, Z);
                var scene = gameObject.GetComponent<Scene>();
                if (Global.isTrayOpened) scene.buildTray();
                else scene.rebuildScene();
            }
        }       
    }
}
