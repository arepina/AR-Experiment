using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

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
            var sourceName = ((char)('a' + random.Next(0, 3))).ToString();
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
            catch (KeyNotFoundException e)
            {
                sourceNotifications = new Stack<Notification>();
                sourceColor = new Color(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
            }
            sourceNotifications.Push(notification);
            NotificationsStorage newNotificationsStorage = new NotificationsStorage(sourceNotifications, timestamp, sourceColor);
            notifications[sourceName] = newNotificationsStorage;
            Dictionary<string, NotificationsStorage> orderedNotifications = notifications.OrderByDescending(x => x.Value.LatestTimestamp)
                                                                                         .ToDictionary(d => d.Key, d => d.Value);

            addNotificationToScene(orderedNotifications);
        }

        private void clearScene()
        {
            var notificationsObjects = GameObject.FindGameObjectsWithTag("Notification");
            foreach (GameObject notification in notificationsObjects)
            {
                Destroy(notification);
            }
        }

        private List<Coordinates> formCoordinatesArray()
        {
            List<Coordinates> coordinates = new List<Coordinates>();
            coordinates.Add(new Coordinates(new Triple(-0.21f, 0, 1.3f), new Triple(180, 90, 180))); // center
            coordinates.Add(new Coordinates(new Triple(-1.1f, 0, 0.9f), new Triple(180, 45, 180))); // left
            coordinates.Add(new Coordinates(new Triple(0.7f, 0, 0.9f), new Triple(180, 135, 180))); // right
            coordinates.Add(new Coordinates(new Triple(-1.5f, 0, 0), new Triple(180, 0, 180))); // left
            coordinates.Add(new Coordinates(new Triple(1.1f, 0, 0), new Triple(180, 180, 180))); // right
            coordinates.Add(new Coordinates(new Triple(-1.1f, 0, -0.9f), new Triple(180, 315, 180))); // left
            coordinates.Add(new Coordinates(new Triple(0.7f, 0, -0.9f), new Triple(180, 225, 180))); // right
            coordinates.Add(new Coordinates(new Triple(-0.21f, 0, -1.3f), new Triple(180, 270, 180))); // opposite
            return coordinates;
        }

        private void addNotificationToScene(Dictionary<string, NotificationsStorage> orderedNotifications)
        {
            clearScene();
            var coordinates = formCoordinatesArray();
            int maxNotificationsInTray = 8;
            int currentGroupIndex = 0;
            foreach (var notificationGroup in orderedNotifications)
            {
                string groupName = notificationGroup.Key;
                Stack<Notification> groupNotifications = notificationGroup.Value.Storage;
                Color groupColor = notificationGroup.Value.SourceColor;
                foreach (var notification in groupNotifications)
                {
                    if (currentGroupIndex < maxNotificationsInTray) // can display only 8
                    {
                        Vector3 position;
                        Quaternion rotation;
                        prefabToCreate.transform.Find("Cube").GetComponent<Renderer>().sharedMaterial.color = groupColor;
                        prefabToCreate.transform.Find("Text").GetComponent<TextMeshPro>().text = notification.Text;
                        prefabToCreate.transform.Find("Author").GetComponent<TextMeshPro>().text = notification.Author;
                        prefabToCreate.transform.Find("Source").GetComponent<TextMeshPro>().text = notification.SourceName;
                        prefabToCreate.transform.Find("Header").GetComponent<TextMeshPro>().text = notification.Header;
                        prefabToCreate.transform.Find("Timestamp").GetComponent<TextMeshPro>().text = new DateTime(notification.Timestamp).ToString();
                        //todo set the Image
                        //prefabToCreate.transform.Find("Icon").GetComponent<Renderer>().sharedMaterial = notification.SourceImage;                        
                        int i = currentGroupIndex;
                        position = new Vector3(coordinates[i].Position.X, coordinates[i].Position.Y, coordinates[i].Position.Z);
                        rotation = Quaternion.Euler(coordinates[i].Rotation.X, coordinates[i].Rotation.Y, coordinates[i].Rotation.Z);
                        currentGroupIndex++;
                        GameObject trayNotification = Instantiate(prefabToCreate, position, rotation) as GameObject;
                    }
                }
            }
        }
    }
}
