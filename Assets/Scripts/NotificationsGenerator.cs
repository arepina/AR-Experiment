using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace Logic
{

    public class NotificationsGenerator : MonoBehaviour
    {

        private System.Random random = new System.Random();
        private Dictionary<string, NotificationsStorage> notifications = new Dictionary<string, NotificationsStorage>();
        public bool isRunning;
        public int secondsRange;
        public GameObject prefabToCreate;

        public void Update()
        {
            if (isRunning)
            {
                StartCoroutine(Wait());
            }
        }

        public IEnumerator Wait()
        {
            isRunning = false;
            int pause = random.Next(1, secondsRange + 1);
            createNotification();
            yield return new WaitForSeconds(pause);
            isRunning = true;
        }

        public void Start()
        {
            isRunning = true;
        }

        public void Stop()
        {
            isRunning = false;
        }

        private void createNotification()
        {
            var sourceImage = Guid.NewGuid().ToString();
            var sourceName = ((char)((int)'a' + random.Next(0, 3))).ToString();
            var author = Guid.NewGuid().ToString();
            var text = Guid.NewGuid().ToString();
            var header = Guid.NewGuid().ToString();
            var timestamp = DateTime.Now.Ticks;
            Notification notification = new Notification("sourceImage: " + sourceImage,
                                              "sourceName: " + sourceName,
                                              "author: " + author,
                                              "text: " + text,
                                              "header: " + header,
                                              timestamp);
            Stack<Notification> sourceNotifications;
            Color sourceColor;
            try
            {
                sourceNotifications = notifications[sourceName].Storage;
                sourceColor = notifications[sourceName].SourceColor;
            }
            catch (KeyNotFoundException e){
                sourceNotifications = new Stack<Notification>();
                sourceColor = new Color(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
            }
            sourceNotifications.Push(notification);
            NotificationsStorage newNotificationsStorage = new NotificationsStorage(sourceNotifications, timestamp, sourceColor);
            notifications[sourceName] = newNotificationsStorage;
            var orderedNotifications = notifications.OrderByDescending(x => x.Value.LatestTimestamp);
            addNotificationToScene(sourceColor);
        }

        private void addNotificationToScene(Color sourceColor)
        {
            var maxNumber = 1;
            var minNumber = -1;
            Vector3 position = new Vector3((float)random.NextDouble() * (maxNumber - minNumber) + minNumber,
                (float)random.NextDouble() * (maxNumber - minNumber) + minNumber,
                1);
            prefabToCreate.transform.Find("Cube").GetComponent<Renderer>().sharedMaterial.SetColor("_SpecColor", sourceColor);
            //prefabToCreate.transform.Find("Text").GetComponent<Text>().text = position.ToString();
            Debug.Log(position);
            GameObject trayNotification = Instantiate(prefabToCreate, position, Quaternion.identity) as GameObject;
        }
    }

}
