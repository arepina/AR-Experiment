using UnityEngine;

namespace Logic
{
    public class SceneRunner : MonoBehaviour
    {
        private Logger myLogger = new Logger(new LogHandler());
        public bool isRunning;
        public GameObject trayHolder;
        public GameObject notificationsHolder;

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

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.A) && !FindObjectOfType<Global>().typeName.Equals("Tray"))
            {
                notificationsHolder.SetActive(!notificationsHolder.activeSelf);
                trayHolder.SetActive(!trayHolder.activeSelf);
                FindObjectOfType<Scene>().rebuildScene();
            }
        }

        public void UpdateScene()
        {
            if (isRunning)
            {
                FindObjectOfType<Scene>().rebuildScene();
            }
        }       
    }
}
