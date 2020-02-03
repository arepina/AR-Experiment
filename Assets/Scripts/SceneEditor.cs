using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                case "HiddenWaves": { buildHiddenWaves(orderedNotifications); break; }
                case "AroundStickers": { buildAroundStickers(orderedNotifications); break; }
                case "AroundMobile": { buildAroundMobile(orderedNotifications); break; }
            }
        }

        public void buildInFrontOfStickers(Dictionary<string, NotificationsStorage> orderedNotifications)
        {
            clearScene();
        }

        public void buildHiddenWaves(Dictionary<string, NotificationsStorage> orderedNotifications)
        {
            clearScene();
            NotificationsStorage storage = orderedNotifications.Values.First();
            Notification notification = storage.Storage.Peek();
            if (!notification.isSilent)
            {
                Vector3 position = new Vector3(0, 1.2f, 5f);
                Quaternion rotation = Quaternion.Euler(0, 0, 90);
                GameObject prefabToCreate = Global.prefabToCreate;
                GameObject waves = Instantiate(prefabToCreate, position, rotation) as GameObject;
                waves.transform.Find("WaveL").gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", notification.Color);
                waves.transform.Find("WaveL").gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 1f);
                waves.transform.Find("WaveM").gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", notification.Color);
                waves.transform.Find("WaveM").gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 1f);
                waves.transform.Find("WaveS").gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", notification.Color);
                waves.transform.Find("WaveS").gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 1f);
                waves.transform.Find("WaveXS").gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", notification.Color);
                waves.transform.Find("WaveXS").gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 1f);
            }
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
            NotificationsStorage storage = orderedNotifications.Values.First();
            Notification notification = storage.Storage.Peek();
            bool doesHaveGroupIcon = false;
            int notificationsNumber = GameObject.FindGameObjectsWithTag("Notification").Length;
            Vector3 position = new Vector3(0, notificationsNumber, 0);
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            Vector3 scale = new Vector3(3f, 3f, 0);
            GameObject notificationObject = addMobileNotification(Global.prefabToCreate, notification, position, scale, rotation, doesHaveGroupIcon);
            GameObject scrollView = GameObject.Find("Panel");
            notificationObject.transform.SetParent(scrollView.transform);
        }

        public void buildTray(Dictionary<string, NotificationsStorage> orderedNotifications)
        {
            clearScene(); 
            List<Coordinates> coordinates = NotificationCoordinates.formTrayCoordinatesArray();
            int indexPosition = 0;
            int maxNotificationsInTray = Global.notificationsInColumn * Global.notificationColumns;
            foreach (KeyValuePair<string, NotificationsStorage> notificationGroup in orderedNotifications)
            {
                Stack<Notification> groupNotifications = notificationGroup.Value.Storage;
                for (int i = 0; i < groupNotifications.Count; i++)
                {
                    Notification notification = groupNotifications.ToArray()[i];
                    bool doesHaveGroupIcon = i == groupNotifications.Count - 1 ||
                        indexPosition % Global.notificationsInColumn == (Global.notificationsInColumn - 1);
                    if (indexPosition < maxNotificationsInTray)
                    {
                        Vector3 position = new Vector3(coordinates[indexPosition].Position.X, coordinates[indexPosition].Position.Y, coordinates[indexPosition].Position.Z);
                        Quaternion rotation = Quaternion.Euler(coordinates[indexPosition].Rotation.X, coordinates[indexPosition].Rotation.Y, coordinates[indexPosition].Rotation.Z);
                        Vector3 scale = new Vector3(1, 1, 1);
                        addMobileNotification(Global.prefabToCreate, notification, position, scale, rotation, doesHaveGroupIcon);
                        indexPosition += 1;
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
            prefabToCreate.transform.Find("Icon")
                          .GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + notification.Icon);
            GameObject notificationObject = Instantiate(prefabToCreate, position, rotation) as GameObject;
            notificationObject.transform.localScale = scale;
            notificationObject.transform.Find("GroupIcon").gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", notification.Color);
            notificationObject.transform.Find("GroupIcon").gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 1f);
            notificationObject.transform.Find("IconBackground").gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", notification.Color);
            notificationObject.transform.Find("IconBackground").gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 1f);
            return notificationObject;
        }
    }
}