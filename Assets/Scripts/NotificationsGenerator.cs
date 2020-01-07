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
                                              timestamp, false);
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
            Dictionary<string, NotificationsStorage> orderedNotifications = notifications.OrderByDescending(x => x.Value.LatestTimestamp)
                                                                                         .ToDictionary(d => d.Key, d => d.Value);
            //addNotificationToScene(orderedNotifications);
        }

        private void addNotificationToScene(Dictionary<string, NotificationsStorage> orderedNotifications)
        {
            //todo remove prev tray

            //todo create postions and rotations arrays

            //center position -0.21 0 1.3
            //      rotation 180 90 180 

            //left  position -1.1 0 0.9
            //      rotation 180 45 180

            //left position -1.5 0 0
            //      rotation 180 0 180 

            //left position -1.1 0 -0.9
            //      rotation 180 315 180

            //opposite the center position -0.21 0 -1.3 (the furtherst one)
            //      rotation 180 270 180 

            //right  position 0.7 0 0.9
            //      rotation 180 135 180

            //right  position 1.1 0 0
            //      rotation 180 180 180

            //right  position 0.7 0 -0.9
            //      rotation 180 225 180
            int maxNotificationsInTray = 8;
            int currentGroupIndex = 0;
            Vector3 leftestPosition = new Vector3();
            Vector3 rightestPosition = new Vector3();
            Quaternion leftestRotation = new Quaternion();
            Quaternion rightestRotation = new Quaternion();
            foreach (var notificationGroup in orderedNotifications)
            {
                string groupName = notificationGroup.Key;
                Stack<Notification> groupNotifications = notificationGroup.Value.Storage;
                Color groupColor = notificationGroup.Value.SourceColor;
                foreach(var notification in groupNotifications)
                {
                    if (currentGroupIndex < maxNotificationsInTray) // can display only 8
                    {
                        Vector3 position;
                        Quaternion rotation;
                        prefabToCreate.transform.Find("Cube").GetComponent<Renderer>().sharedMaterial.color = groupColor;
                        prefabToCreate.transform.Find("Text").GetComponent<Text>().text = notification.Text;
                        prefabToCreate.transform.Find("Author").GetComponent<Text>().text = notification.Author;
                        prefabToCreate.transform.Find("Source").GetComponent<Text>().text = notification.SourceName;
                        prefabToCreate.transform.Find("Header").GetComponent<Text>().text = notification.Header;
                        prefabToCreate.transform.Find("Timestamp").GetComponent<Text>().text = new DateTime(notification.Timestamp).ToString();
                        //todo set the Image
                        //prefabToCreate.transform.Find("Icon").GetComponent<Renderer>().sharedMaterial = notification.SourceImage;
                        if (currentGroupIndex == 0) // the center
                        {
                            position = new Vector3(-0.21f, 0, 1.3f);
                            rotation = new Quaternion(180, 90, 180, 0);
                        }
                        else if (currentGroupIndex % 2 != 0) // go left
                        {
                            position = leftestPosition;
                            rotation = leftestRotation;
                            //todo create postions and rotations arrays
                            //leftestPosition =
                            //leftestRotation = 
                        }
                        else // go right
                        {
                            position = rightestPosition;
                            rotation = rightestRotation;
                            //todo create postions and rotations arrays
                            //rightestPosition =
                            //rightestRotation =
                        }
                        currentGroupIndex++;
                        GameObject trayNotification = Instantiate(prefabToCreate, position, rotation) as GameObject;
                    }
                }
            }
        }
    }

}
