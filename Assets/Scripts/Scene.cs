using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Logic
{
    public class Scene : MonoBehaviour
    {
        private Color markAsReadColor = new Color32(0, 0, 194, 255);
        private Color hideColor = new Color32(255, 36, 0, 255);
        public GameObject trayHolder;
        public GameObject notificationsHolder;

        public delegate GameObject Generator(GameObject prefabToCreate, Notification notification,
                                            Vector3 position, Vector3 scale, Quaternion rotation,
                                            bool doesHaveGroupIcon);

        public delegate List<Coordinates> Coordinate();        

        private void clearScene()
        {
            GameObject[] notificationsObjects = GameObject.FindGameObjectsWithTag("Notification");
            foreach (GameObject notification in notificationsObjects)
            {
                Destroy(notification);
            }
            foreach (Transform childTransform in trayHolder.transform)
            {
                Destroy(childTransform.gameObject);
            }
        }

        public void rebuildScene()
        {
            switch (FindObjectOfType<Global>().typeName)
            {
                case "InFrontOfStickers": { buildInFrontOf(addStickerNotification, NotificationCoordinates.formInFrontOfStickerCoordinatesArray, NotificationCoordinates.formTrayCoordinatesArraySticker); break; }
                case "Tray": { buildTray(addMobileNotification, NotificationCoordinates.formTrayCoordinatesArrayMobile); break; }
                case "InFrontOfMobile": { buildInFrontOf(addMobileNotification, NotificationCoordinates.formInFrontOfMobileCoordinatesArray, NotificationCoordinates.formTrayCoordinatesArrayMobile); break; }
                case "HiddenWaves": { buildHiddenWaves(); break; }
                case "AroundStickers": { buildAround(addStickerNotification, NotificationCoordinates.formAroundStickerCoordinatesArray, NotificationCoordinates.formTrayCoordinatesArraySticker); break; }
                case "AroundMobile": { buildAround(addMobileNotification, NotificationCoordinates.formAroundMobileCoordinatesArray, NotificationCoordinates.formTrayCoordinatesArrayMobile); break; }
            }
        }

        public void buildHiddenWaves()
        {
            var storage = gameObject.GetComponent<Storage>();
            Dictionary<string, NotificationsStorage> orderedNotifications = storage.getStorage();
            Notification notification = orderedNotifications.Values.First().Storage.Peek();
            if (!notification.isSilent)
            {
                Vector3 position = new Vector3(-15, 18.5f, FindObjectOfType<Global>().distanceFromCamera);
                Quaternion rotation = Quaternion.Euler(0, 0, 0);
                GameObject prefabToCreate = FindObjectOfType<Global>().notification;
                GameObject wave = Instantiate(prefabToCreate, position, rotation) as GameObject;
                Color c = notification.Color;
                c.a = 0.5f;
                wave.transform.Find("Image").gameObject.GetComponent<SpriteRenderer>().material.SetColor("_Color", c);
                wave.transform.Find("Image").gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_Glossiness", 1f);
            }
        }

        public void buildAround(Generator notificationGenerator, Coordinate notificationCoordinates, Coordinate traysCoordinates)
        {
            var storage = gameObject.GetComponent<Storage>();
            Dictionary<string, NotificationsStorage> orderedNotifications = storage.getStorage();
            clearScene();
            List<Coordinates> coordinates = notificationCoordinates();
            List<Coordinates> trayCoordinates = traysCoordinates();
            int trayCoordinatesIndex = 0;
            int maxNotificationsInTray = FindObjectOfType<Global>().notificationsInColumnTray * FindObjectOfType<Global>().notificationColumnsTray;
            int groupIndex = 0;
            foreach (KeyValuePair<string, NotificationsStorage> notificationGroup in orderedNotifications)
            {
                Stack<Notification> groupNotifications = notificationGroup.Value.Storage;
                int usualCoordinatesIndex = groupIndex * FindObjectOfType<Global>().notificationsInColumn;
                for (int i = 0; i < groupNotifications.Count; i++)
                {
                    Notification notification = groupNotifications.ToArray()[i];
                    if (usualCoordinatesIndex < maxNotificationsInTray) // tray case
                    {
                        bool doesHaveGroupIconTray = i == groupNotifications.Count - 1 || trayCoordinatesIndex == FindObjectOfType<Global>().notificationsInColumnTray - 1;
                        Vector3 position = new Vector3(trayCoordinates[trayCoordinatesIndex].Position.X, trayCoordinates[trayCoordinatesIndex].Position.Y, trayCoordinates[trayCoordinatesIndex].Position.Z);
                        Quaternion rotation = Quaternion.Euler(trayCoordinates[trayCoordinatesIndex].Rotation.X, trayCoordinates[trayCoordinatesIndex].Rotation.Y, trayCoordinates[trayCoordinatesIndex].Rotation.Z);
                        Vector3 scale = new Vector3(1, 1, 1);
                        GameObject trayN = notificationGenerator(FindObjectOfType<Global>().trayNotification,
                                              notification,
                                              position,
                                              scale,
                                              rotation,
                                              doesHaveGroupIconTray);
                        trayN.transform.parent = trayHolder.transform;
                        trayCoordinatesIndex += 1;
                    }
                    if (i < FindObjectOfType<Global>().notificationsInColumn) // usual case
                    {
                        bool doesHaveGroupIcon = i == 0;
                        Vector3 position = new Vector3(coordinates[usualCoordinatesIndex].Position.X, coordinates[usualCoordinatesIndex].Position.Y, coordinates[usualCoordinatesIndex].Position.Z);
                        Quaternion rotation = Quaternion.Euler(90, 0, 0); //todo
                        Vector3 scale = new Vector3(1, 1, 1);
                        GameObject n = notificationGenerator(FindObjectOfType<Global>().notification,
                                              notification,
                                              position,
                                              scale,
                                              rotation,
                                              doesHaveGroupIcon);
                        GameObject trayN = Instantiate(n);
                        n.transform.parent = notificationsHolder.transform;
                        trayN.transform.parent = trayHolder.transform;
                        usualCoordinatesIndex += 1;
                    }
                }
                groupIndex += 1;
            }
        }

        public void buildInFrontOf(Generator notificationGenerator, Coordinate notificationCoordinates, Coordinate traysCoordinates)
        {
            var storage = gameObject.GetComponent<Storage>();
            Dictionary<string, NotificationsStorage> orderedNotifications = storage.getStorage();
            clearScene();
            List<Coordinates> coordinates = notificationCoordinates();
            List<Coordinates> trayCoordinates = traysCoordinates();
            int usualCoordinatesIndex = 0;
            int trayCoordinatesIndex = 0;
            int maxNotifications = FindObjectOfType<Global>().notificationsInColumn * FindObjectOfType<Global>().notificationColumns;
            int maxNotificationsInTray = FindObjectOfType<Global>().notificationsInColumnTray * FindObjectOfType<Global>().notificationColumnsTray;
            foreach (KeyValuePair<string, NotificationsStorage> notificationGroup in orderedNotifications)
            {
                Stack<Notification> groupNotifications = notificationGroup.Value.Storage;
                for (int i = 0; i < groupNotifications.Count; i++)
                {
                    Notification notification = groupNotifications.ToArray()[i];
                    if (usualCoordinatesIndex < maxNotificationsInTray) // tray case
                    {
                        bool doesHaveGroupIconTray = i == groupNotifications.Count - 1 || trayCoordinatesIndex == FindObjectOfType<Global>().notificationsInColumnTray - 1;
                        Vector3 position = new Vector3(trayCoordinates[trayCoordinatesIndex].Position.X, trayCoordinates[trayCoordinatesIndex].Position.Y, trayCoordinates[trayCoordinatesIndex].Position.Z);
                        Quaternion rotation = Quaternion.Euler(trayCoordinates[trayCoordinatesIndex].Rotation.X, trayCoordinates[trayCoordinatesIndex].Rotation.Y, trayCoordinates[trayCoordinatesIndex].Rotation.Z);
                        Vector3 scale = new Vector3(1, 1, 1);
                        GameObject trayN = notificationGenerator(FindObjectOfType<Global>().trayNotification,
                                              notification,
                                              position,
                                              scale,
                                              rotation,
                                              doesHaveGroupIconTray);
                        trayN.transform.parent = trayHolder.transform;
                        trayCoordinatesIndex += 1;
                    }
                    if (usualCoordinatesIndex < maxNotifications) // usual case 
                    {
                        bool doesHaveGroupIcon = i == groupNotifications.Count - 1 || usualCoordinatesIndex == FindObjectOfType<Global>().notificationsInColumn - 1;
                        Vector3 position = new Vector3(coordinates[usualCoordinatesIndex].Position.X, coordinates[usualCoordinatesIndex].Position.Y, coordinates[usualCoordinatesIndex].Position.Z);
                        Quaternion rotation = Quaternion.Euler(coordinates[usualCoordinatesIndex].Rotation.X, coordinates[usualCoordinatesIndex].Rotation.Y, coordinates[usualCoordinatesIndex].Rotation.Z);
                        Vector3 scale = new Vector3(1, 1, 1);
                        GameObject n = notificationGenerator(FindObjectOfType<Global>().notification,
                                             notification,
                                             position,
                                             scale,
                                             rotation,
                                             doesHaveGroupIcon);
                        n.transform.parent = notificationsHolder.transform;
                        usualCoordinatesIndex += 1;
                    }
                }
            }
        }

        public void buildTray(Generator notificationGenerator, Coordinate traysCoordinates)
        {
            var storage = gameObject.GetComponent<Storage>();
            Dictionary<string, NotificationsStorage> orderedNotifications = storage.getStorage();
            clearScene(); 
            List<Coordinates> coordinates = traysCoordinates();
            int indexPosition = 0;
            trayHolder.SetActive(true);
            int maxNotificationsInTray = FindObjectOfType<Global>().notificationsInColumnTray * FindObjectOfType<Global>().notificationColumnsTray;
            foreach (KeyValuePair<string, NotificationsStorage> notificationGroup in orderedNotifications)
            {
                Stack<Notification> groupNotifications = notificationGroup.Value.Storage;
                for (int i = 0; i < groupNotifications.Count; i++)
                {
                    Notification notification = groupNotifications.ToArray()[i];
                    bool doesHaveGroupIcon = i == groupNotifications.Count - 1 ||
                        indexPosition % FindObjectOfType<Global>().notificationsInColumn == (FindObjectOfType<Global>().notificationsInColumn - 1);
                    if (indexPosition < maxNotificationsInTray)
                    {
                        Vector3 position = new Vector3(coordinates[indexPosition].Position.X, coordinates[indexPosition].Position.Y, coordinates[indexPosition].Position.Z);
                        Quaternion rotation = Quaternion.Euler(coordinates[indexPosition].Rotation.X, coordinates[indexPosition].Rotation.Y, coordinates[indexPosition].Rotation.Z);
                        Vector3 scale = new Vector3(1, 1, 1);
                        GameObject trayN = notificationGenerator(FindObjectOfType<Global>().trayNotification, notification, position, scale, rotation, doesHaveGroupIcon);
                        trayN.transform.parent = trayHolder.transform;
                        indexPosition += 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private GameObject addMobileNotification(GameObject prefabToCreate, Notification notification,
                                            Vector3 position, Vector3 scale, Quaternion rotation,
                                            bool doesHaveGroupIcon)
        {            
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
            prefabToCreate.transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + notification.Icon);
            GameObject notificationObject = Instantiate(prefabToCreate, position, rotation) as GameObject;
            notificationObject.transform.localScale = scale;
            notificationObject.transform.Find("GroupIcon").gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", notification.Color);
            notificationObject.transform.Find("GroupIcon").gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 1f);
            notificationObject.transform.Find("IconBackground").gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", notification.Color);
            notificationObject.transform.Find("IconBackground").gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 1f);
            notificationObject.transform.Find("MarkAsRead").Find("Blue").GetComponent<SpriteRenderer>().material.SetColor("_Color", markAsReadColor);
            notificationObject.transform.Find("Hide").Find("Red").GetComponent<SpriteRenderer>().material.SetColor("_Color", hideColor);
            notificationObject.transform.Find("GroupIcon").Find("MarkAsReadGroup").Find("Blue").GetComponent<SpriteRenderer>().material.SetColor("_Color", markAsReadColor);
            notificationObject.transform.Find("GroupIcon").Find("HideGroup").Find("Red").GetComponent<SpriteRenderer>().material.SetColor("_Color", hideColor);
            return notificationObject;
        }

        private GameObject addStickerNotification(GameObject prefabToCreate, Notification notification,
                                           Vector3 position, Vector3 scale, Quaternion rotation,
                                           bool doesHaveGroupIcon)
        {
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
            GameObject notificationObject = Instantiate(prefabToCreate, position, rotation) as GameObject;
            notificationObject.transform.localScale = scale;
            notificationObject.transform.Find("IconBackground").gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", notification.Color);
            notificationObject.transform.Find("IconBackground").gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 1f);
            notificationObject.transform.Find("MarkAsRead").Find("Blue").GetComponent<SpriteRenderer>().material.SetColor("_Color", markAsReadColor);
            notificationObject.transform.Find("Hide").Find("Red").GetComponent<SpriteRenderer>().material.SetColor("_Color", hideColor);
            notificationObject.transform.Find("Cube").Find("Box").GetComponent<SpriteRenderer>().material.SetColor("_Color", notification.Color);
            return notificationObject;
        }
    }
}