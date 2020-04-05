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
            switch (FindObjectOfType<GlobalCommon>().typeName)
            {
                case "InFrontOfStickers": { buildInFrontOf(addStickerNotification, NotificationCoordinates.formInFrontOfStickerCoordinatesArray, NotificationCoordinates.formTrayCoordinatesArraySticker); break; }
                case "InFrontOfMobile": { buildInFrontOf(addMobileNotification, NotificationCoordinates.formInFrontOfMobileCoordinatesArray, NotificationCoordinates.formTrayCoordinatesArrayMobile); break; }
                case "HiddenWaves": { buildHiddenWaves(addMobileNotification, NotificationCoordinates.formTrayCoordinatesArrayMobile); break; }
                case "AroundStickers": { buildAround(addStickerNotification, NotificationCoordinates.formAroundStickerCoordinatesArray, NotificationCoordinates.formTrayCoordinatesArraySticker); break; }
                case "AroundMobile": { buildAround(addMobileNotification, NotificationCoordinates.formAroundMobileCoordinatesArray, NotificationCoordinates.formTrayCoordinatesArrayMobile); break; }
            }
        }

        public void buildHiddenWaves(Generator notificationGenerator, Coordinate traysCoordinates)
        {
            var storage = gameObject.GetComponent<Storage>();
            Dictionary<string, NotificationsStorage> orderedNotifications = storage.getStorage();
            Notification n = orderedNotifications.Values.First().Storage.Peek();
            if (!n.isSilent && !trayHolder.activeSelf)
            {
                Vector3 position = new Vector3(FindObjectOfType<GlobalWave>().leftXWave, FindObjectOfType<GlobalWave>().YWave, FindObjectOfType<GlobalCommon>().distanceFromCamera);
                Quaternion rotation = Quaternion.Euler(0, 0, 0);
                GameObject prefabToCreate = FindObjectOfType<GlobalCommon>().notification;
                GameObject wave = Instantiate(prefabToCreate, position, rotation) as GameObject;
                Color c = n.Color;
                c.a = 0.5f;
                wave.transform.Find("Image").gameObject.GetComponent<SpriteRenderer>().material.SetColor("_Color", c);
                wave.transform.Find("Image").gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_Glossiness", 1f);
            }
            if(trayHolder.activeSelf)
            {
                clearScene();
                List<Coordinates> coordinates = traysCoordinates();
                int indexPosition = 0;
                int maxNotificationsInTray = FindObjectOfType<GlobalCommon>().notificationsInColumnTray * FindObjectOfType<GlobalCommon>().notificationColumnsTray;
                foreach (KeyValuePair<string, NotificationsStorage> notificationGroup in orderedNotifications)
                {
                    Stack<Notification> groupNotifications = notificationGroup.Value.Storage;
                    for (int i = 0; i < groupNotifications.Count; i++)
                    {
                        Notification notification = groupNotifications.ToArray()[i];
                        bool doesHaveGroupIcon = i == groupNotifications.Count - 1 ||
                            indexPosition % FindObjectOfType<GlobalCommon>().notificationsInColumn == (FindObjectOfType<GlobalCommon>().notificationsInColumn - 1);
                        if (indexPosition < maxNotificationsInTray)
                        {
                            Vector3 position = new Vector3(coordinates[indexPosition].Position.x, coordinates[indexPosition].Position.y, coordinates[indexPosition].Position.z);
                            Quaternion rotation = Quaternion.Euler(coordinates[indexPosition].Rotation.x, coordinates[indexPosition].Rotation.y, coordinates[indexPosition].Rotation.z);
                            Vector3 scale = new Vector3(1, 1, 1);
                            GameObject trayN = notificationGenerator(FindObjectOfType<GlobalCommon>().trayNotification, notification, position, scale, rotation, doesHaveGroupIcon);
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
        }

        public void buildAround(Generator notificationGenerator, Coordinate notificationCoordinates, Coordinate traysCoordinates)
        {
            var storage = gameObject.GetComponent<Storage>();
            Dictionary<string, NotificationsStorage> orderedNotifications = storage.getStorage();
            clearScene();
            List<Coordinates> coordinates = notificationCoordinates();
            List<Coordinates> trayCoordinates = traysCoordinates();
            int trayCoordinatesIndex = 0;
            int maxNotificationsInTray = FindObjectOfType<GlobalCommon>().notificationsInColumnTray * FindObjectOfType<GlobalCommon>().notificationColumnsTray;
            int groupIndex = 0;
            foreach (KeyValuePair<string, NotificationsStorage> notificationGroup in orderedNotifications)
            {
                Stack<Notification> groupNotifications = notificationGroup.Value.Storage;
                int usualCoordinatesIndex = groupIndex * FindObjectOfType<GlobalCommon>().notificationsInColumn;
                for (int i = 0; i < groupNotifications.Count; i++)
                {
                    Notification notification = groupNotifications.ToArray()[i];
                    if (usualCoordinatesIndex < maxNotificationsInTray) // tray case
                    {
                        bool doesHaveGroupIconTray = i == groupNotifications.Count - 1 || trayCoordinatesIndex == FindObjectOfType<GlobalCommon>().notificationsInColumnTray - 1;
                        Vector3 position = new Vector3(trayCoordinates[trayCoordinatesIndex].Position.x, trayCoordinates[trayCoordinatesIndex].Position.y, trayCoordinates[trayCoordinatesIndex].Position.z);
                        Quaternion rotation = Quaternion.Euler(trayCoordinates[trayCoordinatesIndex].Rotation.x, trayCoordinates[trayCoordinatesIndex].Rotation.y, trayCoordinates[trayCoordinatesIndex].Rotation.z);
                        Vector3 scale = new Vector3(1, 1, 1);
                        GameObject trayN = notificationGenerator(FindObjectOfType<GlobalCommon>().trayNotification,
                                              notification,
                                              position,
                                              scale,
                                              rotation,
                                              doesHaveGroupIconTray);
                        trayN.transform.parent = trayHolder.transform;
                        trayCoordinatesIndex += 1;
                    }
                    if (i < FindObjectOfType<GlobalCommon>().notificationsInColumn && !trayHolder.activeSelf) // usual case
                    {
                        bool doesHaveGroupIcon = i == 0;
                        Debug.Log(coordinates[usualCoordinatesIndex].Position);
                        Vector3 position = new Vector3(coordinates[usualCoordinatesIndex].Position.x, coordinates[usualCoordinatesIndex].Position.y, coordinates[usualCoordinatesIndex].Position.z);
                        Quaternion rotation = Quaternion.Euler(coordinates[usualCoordinatesIndex].Rotation.x, coordinates[usualCoordinatesIndex].Rotation.y, coordinates[usualCoordinatesIndex].Rotation.z);
                        Vector3 scale = new Vector3(1, 1, 1);
                        GameObject n = notificationGenerator(FindObjectOfType<GlobalCommon>().notification,
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
            int maxNotifications = FindObjectOfType<GlobalCommon>().notificationsInColumn * FindObjectOfType<GlobalCommon>().notificationColumns;
            int maxNotificationsInTray = FindObjectOfType<GlobalCommon>().notificationsInColumnTray * FindObjectOfType<GlobalCommon>().notificationColumnsTray;
            foreach (KeyValuePair<string, NotificationsStorage> notificationGroup in orderedNotifications)
            {
                Stack<Notification> groupNotifications = notificationGroup.Value.Storage;
                for (int i = 0; i < groupNotifications.Count; i++)
                {
                    Notification notification = groupNotifications.ToArray()[i];
                    if (usualCoordinatesIndex < maxNotificationsInTray) // tray case
                    {
                        bool doesHaveGroupIconTray = i == groupNotifications.Count - 1 || trayCoordinatesIndex == FindObjectOfType<GlobalCommon>().notificationsInColumnTray - 1;
                        Vector3 position = new Vector3(trayCoordinates[trayCoordinatesIndex].Position.x, trayCoordinates[trayCoordinatesIndex].Position.y, trayCoordinates[trayCoordinatesIndex].Position.z);
                        Quaternion rotation = Quaternion.Euler(trayCoordinates[trayCoordinatesIndex].Rotation.x, trayCoordinates[trayCoordinatesIndex].Rotation.y, trayCoordinates[trayCoordinatesIndex].Rotation.z);
                        Vector3 scale = new Vector3(1, 1, 1);
                        GameObject trayN = notificationGenerator(FindObjectOfType<GlobalCommon>().trayNotification,
                                              notification,
                                              position,
                                              scale,
                                              rotation,
                                              doesHaveGroupIconTray);
                        trayN.transform.parent = trayHolder.transform;
                        trayCoordinatesIndex += 1;
                    }
                    if (usualCoordinatesIndex < maxNotifications && !trayHolder.activeSelf) // usual case 
                    {
                        bool doesHaveGroupIcon = i == groupNotifications.Count - 1 || usualCoordinatesIndex == FindObjectOfType<GlobalCommon>().notificationsInColumn - 1;
                        Vector3 position = new Vector3(coordinates[usualCoordinatesIndex].Position.x, coordinates[usualCoordinatesIndex].Position.y, coordinates[usualCoordinatesIndex].Position.z);
                        Quaternion rotation = Quaternion.Euler(coordinates[usualCoordinatesIndex].Rotation.x, coordinates[usualCoordinatesIndex].Rotation.y, coordinates[usualCoordinatesIndex].Rotation.z);
                        Vector3 scale = new Vector3(1, 1, 1);
                        GameObject n = notificationGenerator(FindObjectOfType<GlobalCommon>().notification,
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

        private GameObject addMobileNotification(GameObject prefabToCreate, Notification notification,
                                            Vector3 position, Vector3 scale, Quaternion rotation,
                                            bool doesHaveGroupIcon)
        {
            GameObject notificationObject = Instantiate(prefabToCreate, position, rotation) as GameObject;
            if (doesHaveGroupIcon)
            {
                notificationObject.transform.Find("GroupIcon").localScale = new Vector3(0.5f, 0.05f, 0.5f);
                notificationObject.transform.Find("GroupIcon")
                              .transform.Find("Icon")
                              .GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + notification.SourceImage);
            }
            else
            {
                notificationObject.transform.Find("GroupIcon").localScale = new Vector3(0, 0, 0);
            }
            notificationObject.transform.Find("Text").GetComponent<TextMeshPro>().text = notification.Text;
            notificationObject.transform.Find("Author").GetComponent<TextMeshPro>().text = notification.Author;
            notificationObject.transform.Find("Source").GetComponent<TextMeshPro>().text = notification.SourceName;
            notificationObject.transform.Find("Id").GetComponent<TextMeshPro>().text = notification.Id;
            DateTime currentTime = DateTime.Now;
            double minutes = currentTime.Subtract(new DateTime(notification.Timestamp)).TotalMinutes;
            double seconds = currentTime.Subtract(new DateTime(notification.Timestamp)).TotalSeconds;
            notificationObject.transform.Find("Timestamp").GetComponent<TextMeshPro>().text = minutes < 1 ? seconds < 1 ? "Just now" :
                                                                                                                      string.Format("{0:00}s ago", seconds) :
                                                                                                        string.Format("{0:00}m ago", minutes);
            notificationObject.transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + notification.Icon);
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
            GameObject notificationObject = Instantiate(prefabToCreate, position, rotation) as GameObject;
            Debug.Log(notificationObject.transform.Find("Text"));
            notificationObject.transform.Find("Text").GetComponent<TextMeshPro>().text = notification.Text;
            notificationObject.transform.Find("Author").GetComponent<TextMeshPro>().text = notification.Author;
            notificationObject.transform.Find("Source").GetComponent<TextMeshPro>().text = notification.SourceName;
            notificationObject.transform.Find("Id").GetComponent<TextMeshPro>().text = notification.Id;
            DateTime currentTime = DateTime.Now;
            double minutes = currentTime.Subtract(new DateTime(notification.Timestamp)).TotalMinutes;
            double seconds = currentTime.Subtract(new DateTime(notification.Timestamp)).TotalSeconds;
            notificationObject.transform.Find("Timestamp").GetComponent<TextMeshPro>().text = minutes < 1 ? seconds < 1 ? "Just now" :
                                                                                                                      string.Format("{0:00}s ago", seconds) :
                                                                                                        string.Format("{0:00}m ago", minutes);
            notificationObject.transform.Find("Icon")
                          .GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + notification.Icon);
            notificationObject.transform.localScale = scale;
            notificationObject.transform.Find("IconBackground").gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", notification.Color);
            notificationObject.transform.Find("IconBackground").gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 1f);
            notificationObject.transform.Find("MarkAsRead").Find("Blue").GetComponent<SpriteRenderer>().material.SetColor("_Color", markAsReadColor);
            notificationObject.transform.Find("Hide").Find("Red").GetComponent<SpriteRenderer>().material.SetColor("_Color", hideColor);
            notificationObject.transform.Find("Box").GetComponent<SpriteRenderer>().material.SetColor("_Color", notification.Color);
            return notificationObject;
        }
    }
}