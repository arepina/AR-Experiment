using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Logic
{
    public class Scene : MonoBehaviour
    {
        private Color markAsReadColor = new Color32(0, 0, 194, 255);
        private Color hideColor = new Color32(255, 36, 0, 255);
        public Material red;
        public Material blue;
        public Material yellow;
        public Material green;
        public Material grey;
        public GameObject trayHolder;
        public GameObject notificationsHolder;

        public delegate GameObject Generator(GameObject prefabToCreate, Notification notification,
                                            Vector3 position, Vector3 scale, Quaternion rotation,
                                            bool doesHaveGroupIcon);

        public delegate List<Coordinates> Coordinate();

        public void Start()
        {
            EventManager.AddHandler(EVENT.NotificationCreated, rebuildScene);
            EventManager.AddHandler(EVENT.ShowTray, showTray);
            EventManager.AddHandler(EVENT.HideTray, hideTray);
        }

        private void showTray()
        {
            notificationsHolder.SetActive(false);
            trayHolder.SetActive(true);
            if (FindObjectOfType<GlobalCommon>().typeName == "HidenWaves")
            {
                rebuildScene();
            }
        }

        private void hideTray()
        {
            Vector3 trayPosBefore = trayHolder.transform.position;
            trayPosBefore.y = 10;
            trayHolder.transform.position = trayPosBefore;
            trayHolder.SetActive(false);
            notificationsHolder.SetActive(true);
        }

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
            int columnIndex = 1;
            int notificationsInColumn = 0;
            if (!n.isSilent && !trayHolder.activeSelf)
            {
                GameObject prefabToCreate = FindObjectOfType<GlobalCommon>().notification;
                GameObject wave = Instantiate(prefabToCreate) as GameObject;
                Color c = n.Color;
                c.a = 0.5f;
                if (n.SourceName == "YouTube") wave.GetComponents<Image>()[0].material = red;
                if (n.SourceName == "Telegram") wave.GetComponents<Image>()[0].material = blue;
                if (n.SourceName == "Яндекс.Почта") wave.GetComponents<Image>()[0].material = yellow;
                if (n.SourceName == "WhatsApp") wave.GetComponents<Image>()[0].material = green;
                if (n.SourceName == GlobalCommon.silentGroupKey) wave.GetComponents<Image>()[0].material = grey;
                wave.GetComponents<Image>()[0].material.SetFloat("_Glossiness", 1f);
                wave.transform.parent = notificationsHolder.transform;
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
                        bool doesHaveGroupIconTray = i == groupNotifications.Count - 1 || indexPosition == columnIndex * FindObjectOfType<GlobalCommon>().notificationsInColumnTray - 1;
                        if (indexPosition < maxNotificationsInTray)
                        {
                            Vector3 position = coordinates[indexPosition].Position;
                            Quaternion rotation = Quaternion.Euler(coordinates[indexPosition].Rotation.x, coordinates[indexPosition].Rotation.y, coordinates[indexPosition].Rotation.z);
                            Vector3 scale = coordinates[indexPosition].Scale;
                            GameObject trayN = notificationGenerator(FindObjectOfType<GlobalCommon>().trayNotification, notification, position, scale, rotation, doesHaveGroupIconTray);
                            trayN.transform.parent = trayHolder.transform;
                            trayN.transform.localPosition = position;
                            trayN.transform.localRotation = rotation;
                            indexPosition += 1;
                            notificationsInColumn += 1;
                            if (notificationsInColumn == FindObjectOfType<GlobalCommon>().notificationsInColumnTray)
                            {
                                notificationsInColumn = 0;
                                columnIndex += 1;
                            }
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
            int columnIndex = 1;
            int notificationsInColumn = 0;
            foreach (KeyValuePair<string, NotificationsStorage> notificationGroup in orderedNotifications)
            {
                Stack<Notification> groupNotifications = notificationGroup.Value.Storage;
                int usualCoordinatesIndex = groupIndex * FindObjectOfType<GlobalCommon>().notificationsInColumn;
                for (int i = 0; i < groupNotifications.Count; i++)
                {                   
                    Notification notification = groupNotifications.ToArray()[i];
                    if (trayCoordinatesIndex < maxNotificationsInTray) // tray case
                    {
                        bool doesHaveGroupIconTray = i == groupNotifications.Count - 1 || trayCoordinatesIndex == columnIndex * FindObjectOfType<GlobalCommon>().notificationsInColumnTray - 1;
                        Vector3 position = trayCoordinates[trayCoordinatesIndex].Position;
                        Quaternion rotation = Quaternion.Euler(trayCoordinates[trayCoordinatesIndex].Rotation.x, trayCoordinates[trayCoordinatesIndex].Rotation.y, trayCoordinates[trayCoordinatesIndex].Rotation.z);
                        Vector3 scale = trayCoordinates[trayCoordinatesIndex].Scale;
                        GameObject trayN = notificationGenerator(FindObjectOfType<GlobalCommon>().trayNotification,
                                              notification,
                                              position,
                                              scale,
                                              rotation,
                                              doesHaveGroupIconTray);
                        trayN.transform.parent = trayHolder.transform;
                        trayN.transform.localPosition = position;
                        trayN.transform.localRotation = rotation;
                        trayCoordinatesIndex += 1;
                        notificationsInColumn += 1;
                        if (notificationsInColumn == FindObjectOfType<GlobalCommon>().notificationsInColumnTray)
                        {
                            notificationsInColumn = 0;
                            columnIndex += 1;
                        }
                    }
                    if (i < FindObjectOfType<GlobalCommon>().notificationsInColumn && !trayHolder.activeSelf) // usual case
                    {
                        bool doesHaveGroupIcon = i == 0;
                        Vector3 position = coordinates[usualCoordinatesIndex].Position;
                        Quaternion rotation = Quaternion.Euler(coordinates[usualCoordinatesIndex].Rotation.x, coordinates[usualCoordinatesIndex].Rotation.y, coordinates[usualCoordinatesIndex].Rotation.z);
                        Vector3 scale = coordinates[usualCoordinatesIndex].Scale;
                        GameObject n = notificationGenerator(FindObjectOfType<GlobalCommon>().notification,
                                              notification,
                                              position,
                                              scale,
                                              rotation,
                                              doesHaveGroupIcon);
                        GameObject trayN = Instantiate(n);
                        if (!notification.isMarkedAsRead)
                        {
                            n.transform.parent = notificationsHolder.transform;
                            n.transform.localPosition = position;
                            n.transform.localRotation = rotation;
                        }
                        else
                        {
                            Destroy(n);
                        }
                        trayN.transform.parent = trayHolder.transform;
                        trayN.transform.localPosition = position;
                        trayN.transform.localRotation = rotation;
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
            int columnIndex = 1;
            int notificationsInColumn = 0;
            int maxNotifications = FindObjectOfType<GlobalCommon>().notificationsInColumn * FindObjectOfType<GlobalCommon>().notificationColumns;
            int maxNotificationsInTray = FindObjectOfType<GlobalCommon>().notificationsInColumnTray * FindObjectOfType<GlobalCommon>().notificationColumnsTray;
            foreach (KeyValuePair<string, NotificationsStorage> notificationGroup in orderedNotifications)
            {
                Stack<Notification> groupNotifications = notificationGroup.Value.Storage;
                for (int i = 0; i < groupNotifications.Count; i++)
                {
                    Notification notification = groupNotifications.ToArray()[i];
                    if (trayCoordinatesIndex < maxNotificationsInTray) // tray case
                    {
                        bool doesHaveGroupIconTray = i == groupNotifications.Count - 1 || trayCoordinatesIndex == columnIndex * FindObjectOfType<GlobalCommon>().notificationsInColumnTray - 1;
                        Vector3 position = trayCoordinates[trayCoordinatesIndex].Position;
                        Quaternion rotation = Quaternion.Euler(trayCoordinates[trayCoordinatesIndex].Rotation.x, trayCoordinates[trayCoordinatesIndex].Rotation.y, trayCoordinates[trayCoordinatesIndex].Rotation.z);
                        Vector3 scale = trayCoordinates[trayCoordinatesIndex].Scale;
                        GameObject trayN = notificationGenerator(FindObjectOfType<GlobalCommon>().trayNotification,
                                              notification,
                                              position,
                                              scale,
                                              rotation,
                                              doesHaveGroupIconTray);
                        trayN.transform.parent = trayHolder.transform;
                        trayN.transform.localPosition = position;
                        trayN.transform.localRotation = rotation;
                        trayCoordinatesIndex += 1;
                        notificationsInColumn += 1;
                        if (notificationsInColumn == FindObjectOfType<GlobalCommon>().notificationsInColumnTray)
                        {
                            notificationsInColumn = 0;
                            columnIndex += 1;
                        }

                    }
                    if (usualCoordinatesIndex < maxNotifications && !trayHolder.activeSelf) // usual case 
                    {
                        bool doesHaveGroupIcon = i == groupNotifications.Count - 1 || usualCoordinatesIndex == FindObjectOfType<GlobalCommon>().notificationsInColumn - 1;
                        Vector3 position = coordinates[usualCoordinatesIndex].Position;
                        Quaternion rotation = Quaternion.Euler(coordinates[usualCoordinatesIndex].Rotation.x, coordinates[usualCoordinatesIndex].Rotation.y, coordinates[usualCoordinatesIndex].Rotation.z);
                        Vector3 scale = coordinates[usualCoordinatesIndex].Scale;
                        GameObject n = notificationGenerator(FindObjectOfType<GlobalCommon>().notification,
                                             notification,
                                             position,
                                             scale,
                                             rotation,
                                             doesHaveGroupIcon);
                        if (!notification.isMarkedAsRead)
                        {
                            n.transform.parent = notificationsHolder.transform;
                            n.transform.localPosition = position;
                            n.transform.localRotation = rotation;
                        }
                        else
                        {
                            Destroy(n);
                        }                        
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
                notificationObject.GetComponentsInChildren<MeshRenderer>()[10].gameObject.transform.localScale = new Vector3(0.5f, 0.05f, 0.5f);
                notificationObject.GetComponentsInChildren<SpriteRenderer>()[2].sprite = Resources.Load<Sprite>("Sprites/" + notification.SourceImage);
            }
            else
            {
                notificationObject.GetComponentsInChildren<MeshRenderer>()[10].gameObject.transform.localScale = new Vector3(0, 0, 0);
            }
            notificationObject.GetComponentsInChildren<TextMeshPro>()[0].text = notification.Text;
            notificationObject.GetComponentsInChildren<TextMeshPro>()[1].text = notification.Author;
            notificationObject.GetComponentsInChildren<TextMeshPro>()[2].text = notification.SourceName;
            notificationObject.GetComponentsInChildren<TextMeshPro>()[4].text = notification.Id;
            DateTime currentTime = DateTime.Now;
            double minutes = currentTime.Subtract(new DateTime(notification.Timestamp)).TotalMinutes;
            double seconds = currentTime.Subtract(new DateTime(notification.Timestamp)).TotalSeconds;
            notificationObject.GetComponentsInChildren<TextMeshPro>()[3].text = minutes < 1 ? seconds < 1 ? "Just now" :
                                                                                                                      string.Format("{0:00}s ago", seconds) :
                                                                                                        string.Format("{0:00}m ago", minutes);
            notificationObject.GetComponentsInChildren<SpriteRenderer>()[1].sprite = Resources.Load<Sprite>("Sprites/" + notification.Icon);
            notificationObject.transform.localScale = scale;
            notificationObject.GetComponentsInChildren<MeshRenderer>()[10].material.SetColor("_Color", notification.Color);
            notificationObject.GetComponentsInChildren<MeshRenderer>()[10].material.SetFloat("_Glossiness", 1f);
            notificationObject.GetComponentsInChildren<SpriteRenderer>(true)[8].material.SetColor("_Color", markAsReadColor);
            notificationObject.GetComponentsInChildren<SpriteRenderer>(true)[10].material.SetColor("_Color", hideColor);
            notificationObject.GetComponentsInChildren<SpriteRenderer>(true)[6].material.SetColor("_Color", markAsReadColor);
            notificationObject.GetComponentsInChildren<SpriteRenderer>(true)[4].material.SetColor("_Color", hideColor);
            return notificationObject;
        }

        private GameObject addStickerNotification(GameObject prefabToCreate, Notification notification,
                                           Vector3 position, Vector3 scale, Quaternion rotation,
                                           bool doesHaveGroupIcon)
        {
            GameObject notificationObject = Instantiate(prefabToCreate, position, rotation) as GameObject;
            notificationObject.GetComponentsInChildren<TextMeshPro>()[0].text = notification.Text;
            notificationObject.GetComponentsInChildren<TextMeshPro>()[1].text = notification.Author;
            notificationObject.GetComponentsInChildren<TextMeshPro>()[4].text = notification.SourceName;
            notificationObject.GetComponentsInChildren<TextMeshPro>()[3].text = notification.Id;
            DateTime currentTime = DateTime.Now;
            double minutes = currentTime.Subtract(new DateTime(notification.Timestamp)).TotalMinutes;
            double seconds = currentTime.Subtract(new DateTime(notification.Timestamp)).TotalSeconds;
            notificationObject.GetComponentsInChildren<TextMeshPro>()[2].text = minutes < 1 ? seconds < 1 ? "Just now" :
                                                                                                                      string.Format("{0:00}s ago", seconds) :
                                                                                                        string.Format("{0:00}m ago", minutes);
            notificationObject.GetComponentsInChildren<SpriteRenderer>()[0].sprite = Resources.Load<Sprite>("Sprites/" + notification.Icon);
            notificationObject.transform.localScale = scale;
            notificationObject.GetComponentsInChildren<MeshRenderer>()[9].material.SetColor("_Color", notification.Color);
            notificationObject.GetComponentsInChildren<MeshRenderer>()[9].material.SetFloat("_Glossiness", 1f);
            notificationObject.GetComponentsInChildren<SpriteRenderer>()[1].material.SetColor("_Color", notification.Color);
            notificationObject.GetComponentsInChildren<SpriteRenderer>(true)[3].material.SetColor("_Color", markAsReadColor);
            notificationObject.GetComponentsInChildren<SpriteRenderer>(true)[5].material.SetColor("_Color", hideColor);
            return notificationObject;
        }
    }
}