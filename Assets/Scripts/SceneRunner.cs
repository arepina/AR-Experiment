using System;
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
            //foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            //{
            //    if (Input.GetKey(kcode))
            //    {
            //        Debug.Log("KeyCode down: " + kcode);
            //        myLogger.Log("KeyCode down: " + kcode);
            //    }
            //}
            if (Input.GetKeyDown(KeyCode.O))
            {
                if (notificationsHolder != null)
                {
                    notificationsHolder.SetActive(!notificationsHolder.activeSelf);
                }
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
