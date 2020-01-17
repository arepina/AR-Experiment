using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace Logic
{

    public class NotificationsRunner : MonoBehaviour
    {
        private Dictionary<string, NotificationsStorage> notifications = new Dictionary<string, NotificationsStorage>();
        private NotificationsGenerator notificationsGenerator = new NotificationsGenerator();
        private System.Random random = new System.Random();
        public bool isRunning;
        public int secondsRange;
        public int notificationsInColumn;
        public int notificationColumns;
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
            Notification notification = notificationsGenerator.getNotification();
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

        private Dictionary<string, NotificationsStorage> addToStorage(Notification notification)
        {
            Stack<Notification> sourceNotifications = new Stack<Notification>();
            string sourceName = notification.SourceName;
            if (notification.isSilent)
            {
                sourceName = silentGroupKey;
            }
            if (notifications.ContainsKey(sourceName))
            {
                sourceNotifications = notifications[sourceName].Storage;
            }
            sourceNotifications.Push(notification);
            NotificationsStorage newNotificationsStorage = new NotificationsStorage(sourceNotifications,
                                                                                    notification.Timestamp);
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

        private void addNotificationToScene(Dictionary<string, NotificationsStorage> orderedNotifications)
        {
            clearScene(); // todo change to reposition with animation
            List<Coordinates> coordinates = NotificationCoordinates.formCoordinatesArray();
            int maxNotificationsInTray = notificationsInColumn * notificationColumns;
            int indexPosition = 0;
            foreach (KeyValuePair<string, NotificationsStorage> notificationGroup in orderedNotifications)
            {
                string groupName = notificationGroup.Key;
                Stack<Notification> groupNotifications = notificationGroup.Value.Storage;
                bool isFirstInGroup = true;
                foreach (Notification notification in groupNotifications)
                {
                    if (indexPosition < maxNotificationsInTray)
                    {
                        Vector3 position;
                        Quaternion rotation;
                        if (isFirstInGroup)
                        {
                            prefabToCreate.transform.Find("GroupIcon").localScale = new Vector3(0.3f, 0.2f, 0.2f);
                            prefabToCreate.transform.Find("GroupIcon")
                                          .transform.Find("Icon")
                                          .GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(notification.SourceImage);
                            isFirstInGroup = false;
                        }
                        else
                        {
                            prefabToCreate.transform.Find("GroupIcon").localScale = new Vector3(0, 0, 0);
                        }
                        Renderer render = prefabToCreate.transform.Find("Cube").GetComponent<Renderer>();
                        render.sharedMaterial.color = notification.Color;
                        prefabToCreate.transform.Find("Text").GetComponent<TextMeshPro>().text = notification.Text;
                        prefabToCreate.transform.Find("Author").GetComponent<TextMeshPro>().text = notification.Author;
                        prefabToCreate.transform.Find("Source").GetComponent<TextMeshPro>().text = notification.SourceName;
                        prefabToCreate.transform.Find("Header").GetComponent<TextMeshPro>().text = notification.header;
                        prefabToCreate.transform.Find("Timestamp").GetComponent<TextMeshPro>().text = new DateTime(notification.Timestamp).ToString();
                        prefabToCreate.transform.Find("Icon")
                                      .GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(notification.SourceImage);
                        position = new Vector3(coordinates[indexPosition].Position.X, coordinates[indexPosition].Position.Y, coordinates[indexPosition].Position.Z);
                        rotation = Quaternion.Euler(coordinates[indexPosition].Rotation.X, coordinates[indexPosition].Rotation.Y, coordinates[indexPosition].Rotation.Z);
                        indexPosition = indexPosition + 1;
                        GameObject trayNotification = Instantiate(prefabToCreate, position, rotation) as GameObject;
                    }
                }
            }
        }
    }
}
