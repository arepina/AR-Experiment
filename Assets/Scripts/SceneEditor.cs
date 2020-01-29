using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Logic
{
    public class SceneEditor : MonoBehaviour
    {
        private void clearScene()
        {
            GameObject[] notificationsObjects = GameObject.FindGameObjectsWithTag("Notification");
            foreach (GameObject notification in notificationsObjects)
            {
                Destroy(notification);
            }
        }

        public void rebuildScene(string type, Dictionary<string, NotificationsStorage> orderedNotifications)
        {
            switch (type)
            {
                case "InFrontOfStickers": { buildInFrontOfStickers(orderedNotifications); break; }
                case "Tray": { buildTray(orderedNotifications); break; }
                case "InFrontOfMobile": { buildInFrontOfMobile(orderedNotifications); break; }
                case "HiddenWaves": { buildHiddenWaves(); break; }
                case "AroundStickers": { buildAroundStickers(orderedNotifications); break; }
                case "AroundMobile": { buildAroundMobile(orderedNotifications); break; }
            }
        }

        public void buildInFrontOfStickers(Dictionary<string, NotificationsStorage> orderedNotifications)
        {
            clearScene();
        }

        public void buildHiddenWaves()
        {
            clearScene();
        }

        public void buildAroundStickers(Dictionary<string, NotificationsStorage> orderedNotifications)
        {
            clearScene();
        }

        public void buildAroundMobile(Dictionary<string, NotificationsStorage> orderedNotifications)
        {
            clearScene();
        }

        public void buildInFrontOfMobile(Dictionary<string, NotificationsStorage> orderedNotifications)
        {
            clearScene();
            List<Coordinates> coordinates = NotificationCoordinates.formInFrontOfMobileCoordinatesArray();
            int indexPosition = 0;
            int maxNotificationsInTray = Global.notificationsInColumn * Global.notificationColumns;
            //todo change view of notifications and add scroll
            foreach (KeyValuePair<string, NotificationsStorage> notificationGroup in orderedNotifications)
            {
                string groupName = notificationGroup.Key;
                Stack<Notification> groupNotifications = notificationGroup.Value.Storage;
                for (int i = 0; i < groupNotifications.Count; i++)
                {
                    Notification notification = groupNotifications.ToArray()[i];
                    bool doesHaveGroupIcon = i == groupNotifications.Count - 1 ||
                        indexPosition % Global.notificationsInColumn == (Global.notificationsInColumn - 1);
                    if (indexPosition < maxNotificationsInTray)
                    {
                        addMobileNotification(Global.prefabToCreate, notification, coordinates, indexPosition, doesHaveGroupIcon);
                        indexPosition += 1;
                    }
                }
            }
        }

        public void buildTray(Dictionary<string, NotificationsStorage> orderedNotifications)
        {
            clearScene(); 
            List<Coordinates> coordinates = NotificationCoordinates.formTrayCoordinatesArray();
            int indexPosition = 0;
            int maxNotificationsInTray = Global.notificationsInColumn * Global.notificationColumns;
            foreach (KeyValuePair<string, NotificationsStorage> notificationGroup in orderedNotifications)
            {
                string groupName = notificationGroup.Key;
                Stack<Notification> groupNotifications = notificationGroup.Value.Storage;
                for (int i = 0; i < groupNotifications.Count; i++)
                {
                    Notification notification = groupNotifications.ToArray()[i];
                    bool doesHaveGroupIcon = i == groupNotifications.Count - 1 ||
                        indexPosition % Global.notificationsInColumn == (Global.notificationsInColumn - 1);
                    if (indexPosition < maxNotificationsInTray)
                    {
                        addMobileNotification(Global.prefabToCreate, notification, coordinates, indexPosition, doesHaveGroupIcon);
                        indexPosition += 1;
                    }
                }
            }
        }

        private void addMobileNotification(GameObject prefabToCreate, Notification notification, List<Coordinates> coordinates, int indexPosition, bool doesHaveGroupIcon)
        {
            Vector3 position;
            Quaternion rotation;
            if (doesHaveGroupIcon)
            {
                prefabToCreate.transform.Find("GroupIcon").localScale = new Vector3(0.5f, 0.05f, 0.5f);
                prefabToCreate.transform.Find("GroupIcon")
                              .transform.Find("Icon")
                              .GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + notification.SourceImage);
            }
            else
            {
                prefabToCreate.transform.Find("GroupIcon").localScale = new Vector3(0, 0, 0);
            }
            prefabToCreate.transform.Find("Text").GetComponent<TextMeshPro>().text = notification.Text;
            prefabToCreate.transform.Find("Author").GetComponent<TextMeshPro>().text = notification.Author;
            prefabToCreate.transform.Find("Source").GetComponent<TextMeshPro>().text = notification.SourceName;
            prefabToCreate.transform.Find("Id").GetComponent<TextMeshPro>().text = notification.Id;
            DateTime currentTime = DateTime.Now;
            double minutes = currentTime.Subtract(new DateTime(notification.Timestamp)).TotalMinutes;
            double seconds = currentTime.Subtract(new DateTime(notification.Timestamp)).TotalSeconds;
            prefabToCreate.transform.Find("Timestamp").GetComponent<TextMeshPro>().text = minutes < 1 ? seconds < 1 ? "Just now" :
                                                                                                                      string.Format("{0:00}s ago", seconds) :
                                                                                                        string.Format("{0:00}m ago", minutes);
            prefabToCreate.transform.Find("Icon")
                          .GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + notification.Icon);
            position = new Vector3(coordinates[indexPosition].Position.X, coordinates[indexPosition].Position.Y, coordinates[indexPosition].Position.Z);
            rotation = Quaternion.Euler(coordinates[indexPosition].Rotation.X, coordinates[indexPosition].Rotation.Y, coordinates[indexPosition].Rotation.Z);
            GameObject notificationObject = Instantiate(prefabToCreate, position, rotation) as GameObject;
            notificationObject.transform.Find("GroupIcon").gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", notification.Color);
            notificationObject.transform.Find("GroupIcon").gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 1f);
            notificationObject.transform.Find("IconBackground").gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", notification.Color);
            notificationObject.transform.Find("IconBackground").gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 1f);
        }
    }
}