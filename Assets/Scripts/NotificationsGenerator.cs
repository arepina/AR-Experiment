using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
        private string silentGroupKey = "_silent_";

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
            Notification notification = createNotification();
            Dictionary<string, NotificationsStorage> orderedNotifications = addToStorage(notification);
            addNotificationToScene(orderedNotifications);
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

        private Notification createNotification()
        {
            string sourceImage = Guid.NewGuid().ToString();
            string sourceName = ((char)('a' + random.Next(0, 3))).ToString();
            string author = Guid.NewGuid().ToString();
            string text = Guid.NewGuid().ToString();
            string header = Guid.NewGuid().ToString();
            long timestamp = DateTime.Now.Ticks;
            Notification notification = new Notification("sourceImage: " + sourceImage,
                                              "sourceName: " + sourceName,
                                              "author: " + author,
                                              "text: " + text,
                                              "header: " + header,
                                              timestamp, random.Next(0, 2) == 0);
            
            return notification;
        }

        private Dictionary<string, NotificationsStorage> addToStorage(Notification notification)
        {
            Stack<Notification> sourceNotifications;
            Color sourceColor;
            Image sourceIcon;
            string sourceName = notification.SourceName;
            if (notification.isSilent)
            {
                sourceName = silentGroupKey;
                //todo choose sourceColor and sourceIcon
                notification.header = "Silent: " + notification.header;
            }
            if (notifications.ContainsKey(sourceName))
            {
                sourceNotifications = notifications[sourceName].Storage;
                sourceColor = notifications[sourceName].SourceColor;
                sourceIcon = notifications[sourceName].SourceIcon;
            }
            else
            {
                sourceNotifications = new Stack<Notification>();
                sourceColor = UnityEngine.Random.ColorHSV((float)random.NextDouble(),
                                                          (float)random.NextDouble(),
                                                          (float)random.NextDouble(),
                                                          (float)random.NextDouble(),
                                                          (float)random.NextDouble(),
                                                          (float)random.NextDouble());
                sourceIcon = null;
                //todo set sourceIcon here
            }
            sourceNotifications.Push(notification);
            NotificationsStorage newNotificationsStorage = new NotificationsStorage(sourceNotifications,
                                                                                    notification.Timestamp,
                                                                                    sourceColor,
                                                                                    sourceIcon);
            notifications[sourceName] = newNotificationsStorage;
            NotificationsStorage silentGroup = null;
            if (notifications.ContainsKey(silentGroupKey))
            {
                silentGroup = notifications[silentGroupKey];
                notifications.Remove(silentGroupKey);
            }
            Dictionary<string, NotificationsStorage> orderedNotifications = notifications.OrderByDescending(x => x.Value.LatestTimestamp)
                                                                                         .ToDictionary(d => d.Key, d => d.Value);
            if (silentGroup != null || sourceName == silentGroupKey)
            {
                orderedNotifications.Add(silentGroupKey, silentGroup); // silent are always the last
                notifications.Add(silentGroupKey, silentGroup);
            }
            return orderedNotifications;
        }

        private void clearScene()
        {
            GameObject[] notificationsObjects = GameObject.FindGameObjectsWithTag("Notification");
            foreach (GameObject notification in notificationsObjects)
            {
                Destroy(notification);
            }
        }

        private List<Coordinates> formCoordinatesArray()
        {
            List<Coordinates> coordinates = new List<Coordinates>();
            coordinates.Add(new Coordinates(new Triple(-0.21f, 0.341f, 1.3f), new Triple(180, 90, 180))); // center 
            coordinates.Add(new Coordinates(new Triple(-0.21f, 0, 1.3f), new Triple(180, 90, 180))); // center 
            coordinates.Add(new Coordinates(new Triple(-0.21f, -0.337f, 1.3f), new Triple(180, 90, 180))); // center 
            coordinates.Add(new Coordinates(new Triple(-0.21f, -0.676f, 1.3f), new Triple(180, 90, 180))); // center 

            coordinates.Add(new Coordinates(new Triple(-1.1f, 0.341f, 0.9f), new Triple(180, 45, 180))); // left
            coordinates.Add(new Coordinates(new Triple(-1.1f, 0, 0.9f), new Triple(180, 45, 180))); // left
            coordinates.Add(new Coordinates(new Triple(-1.1f, -0.337f, 0.9f), new Triple(180, 45, 180))); // left
            coordinates.Add(new Coordinates(new Triple(-1.1f, -0.676f, 0.9f), new Triple(180, 45, 180))); // left

            coordinates.Add(new Coordinates(new Triple(0.7f, 0.341f, 0.9f), new Triple(180, 135, 180))); // right
            coordinates.Add(new Coordinates(new Triple(0.7f, 0, 0.9f), new Triple(180, 135, 180))); // right
            coordinates.Add(new Coordinates(new Triple(0.7f, -0.337f, 0.9f), new Triple(180, 135, 180))); // right
            coordinates.Add(new Coordinates(new Triple(0.7f, -0.676f, 0.9f), new Triple(180, 135, 180))); // right

            coordinates.Add(new Coordinates(new Triple(-1.5f, 0.341f, 0), new Triple(180, 0, 180))); // left
            coordinates.Add(new Coordinates(new Triple(-1.5f, 0, 0), new Triple(180, 0, 180))); // left
            coordinates.Add(new Coordinates(new Triple(-1.5f, -0.337f, 0), new Triple(180, 0, 180))); // left
            coordinates.Add(new Coordinates(new Triple(-1.5f, -0.676f, 0), new Triple(180, 0, 180))); // left

            coordinates.Add(new Coordinates(new Triple(1.1f, 0.341f, 0), new Triple(180, 180, 180))); // right
            coordinates.Add(new Coordinates(new Triple(1.1f, 0, 0), new Triple(180, 180, 180))); // right
            coordinates.Add(new Coordinates(new Triple(1.1f, -0.337f, 0), new Triple(180, 180, 180))); // right
            coordinates.Add(new Coordinates(new Triple(1.1f, -0.676f, 0), new Triple(180, 180, 180))); // right

            coordinates.Add(new Coordinates(new Triple(-1.1f, 0.341f, -0.9f), new Triple(180, 315, 180))); // left
            coordinates.Add(new Coordinates(new Triple(-1.1f, 0, -0.9f), new Triple(180, 315, 180))); // left
            coordinates.Add(new Coordinates(new Triple(-1.1f, -0.337f, -0.9f), new Triple(180, 315, 180))); // left
            coordinates.Add(new Coordinates(new Triple(-1.1f, -0.676f, -0.9f), new Triple(180, 315, 180))); // left

            coordinates.Add(new Coordinates(new Triple(0.7f, 0.341f, -0.9f), new Triple(180, 225, 180))); // right
            coordinates.Add(new Coordinates(new Triple(0.7f, 0, -0.9f), new Triple(180, 225, 180))); // right
            coordinates.Add(new Coordinates(new Triple(0.7f, -0.337f, -0.9f), new Triple(180, 225, 180))); // right
            coordinates.Add(new Coordinates(new Triple(0.7f, -0.676f, -0.9f), new Triple(180, 225, 180))); // right

            coordinates.Add(new Coordinates(new Triple(-0.21f, 0.341f, -1.3f), new Triple(180, 270, 180))); // opposite
            coordinates.Add(new Coordinates(new Triple(-0.21f, 0, -1.3f), new Triple(180, 270, 180))); // opposite
            coordinates.Add(new Coordinates(new Triple(-0.21f, -0.337f, -1.3f), new Triple(180, 270, 180))); // opposite
            coordinates.Add(new Coordinates(new Triple(-0.21f, -0.676f, -1.3f), new Triple(180, 270, 180))); // opposite
            return coordinates;
        }

        private void addNotificationToScene(Dictionary<string, NotificationsStorage> orderedNotifications)
        {
            Debug.Log("____________________________________");
            clearScene();
            List<Coordinates> coordinates = formCoordinatesArray();
            int maxNotificationsInTray = 4 * 8;
            int indexPosition = 0;
            foreach (KeyValuePair<string, NotificationsStorage> notificationGroup in orderedNotifications)
            {
                string groupName = notificationGroup.Key;
                Stack<Notification> groupNotifications = notificationGroup.Value.Storage;
                Color groupColor = notificationGroup.Value.SourceColor;
                Image groupIcon = notificationGroup.Value.SourceIcon;
                bool isFirstInGroup = true;
                foreach (Notification notification in groupNotifications)
                {
                    if (indexPosition < maxNotificationsInTray) // can display only 32
                    {
                        Vector3 position;
                        Quaternion rotation;
                        if (isFirstInGroup)
                        {
                            prefabToCreate.transform.Find("GroupIcon").localScale = new Vector3(0.06f, 0.1f, 0.1f);
                            //todo set the GroupIcon
                            //icon.transform.Find("Icon").GetComponent<Renderer>().sharedMaterial = groupIcon;
                            isFirstInGroup = false;
                        }
                        else
                        {
                            prefabToCreate.transform.Find("GroupIcon").localScale = new Vector3(0, 0, 0);
                        }
                        Renderer render = prefabToCreate.transform.Find("Cube").GetComponent<Renderer>();
                        render.sharedMaterial.color = groupColor;
                        prefabToCreate.transform.Find("Text").GetComponent<TextMeshPro>().text = notification.Text;
                        prefabToCreate.transform.Find("Author").GetComponent<TextMeshPro>().text = notification.Author;
                        prefabToCreate.transform.Find("Source").GetComponent<TextMeshPro>().text = notification.SourceName;
                        prefabToCreate.transform.Find("Header").GetComponent<TextMeshPro>().text = notification.header;
                        prefabToCreate.transform.Find("Timestamp").GetComponent<TextMeshPro>().text = new DateTime(notification.Timestamp).ToString();
                        //todo set the SourceImage
                        //prefabToCreate.transform.Find("Icon").GetComponent<Renderer>().sharedMaterial = notification.SourceImage; 
                        position = new Vector3(coordinates[indexPosition].Position.X, coordinates[indexPosition].Position.Y, coordinates[indexPosition].Position.Z);
                        rotation = Quaternion.Euler(coordinates[indexPosition].Rotation.X, coordinates[indexPosition].Rotation.Y, coordinates[indexPosition].Rotation.Z);
                        indexPosition = indexPosition + 1;
                        Debug.Log(groupColor);
                        GameObject trayNotification = Instantiate(prefabToCreate, position, rotation) as GameObject;
                    }
                }
            }
        }
    }
}
