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

        public void rebuildScene(Dictionary<string, NotificationsStorage> orderedNotifications)
        {
            clearScene(); // todo change to reposition with animation
            List<Coordinates> coordinates = NotificationCoordinates.formCoordinatesArray();
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
                        addNotification(Global.prefabToCreate, notification, coordinates, indexPosition, doesHaveGroupIcon);
                        indexPosition += 1;
                    }
                }
            }
        }

        private void addNotification(GameObject prefabToCreate, Notification notification, List<Coordinates> coordinates, int indexPosition, bool doesHaveGroupIcon)
        {
            Vector3 position;
            Quaternion rotation;
            if (doesHaveGroupIcon)
            {
                prefabToCreate.transform.Find("GroupIcon").localScale = new Vector3(0.3f, 0.2f, 0.2f);
                prefabToCreate.transform.Find("GroupIcon")
                              .transform.Find("Icon")
                              .GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + notification.SourceImage);
            }
            else
            {
                prefabToCreate.transform.Find("GroupIcon").localScale = new Vector3(0, 0, 0);
            }
            //todo fix change color
            //GameObject cube = prefabToCreate.transform.Find("Cube").gameObject;
            //cube.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", notification.Color);
            prefabToCreate.transform.Find("Text").GetComponent<TextMeshPro>().text = notification.Text;
            prefabToCreate.transform.Find("Author").GetComponent<TextMeshPro>().text = notification.Author;
            prefabToCreate.transform.Find("Source").GetComponent<TextMeshPro>().text = notification.SourceName;
            prefabToCreate.transform.Find("Header").GetComponent<TextMeshPro>().text = notification.header;
            prefabToCreate.transform.Find("Id").GetComponent<TextMeshPro>().text = notification.Id;
            prefabToCreate.transform.Find("Timestamp").GetComponent<TextMeshPro>().text = new DateTime(notification.Timestamp).ToString();
            prefabToCreate.transform.Find("Icon")
                          .GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + notification.Icon);
            //todo how to show hide and mark as read
            //prefabToCreate.transform.Find("Hide").gameObject.SetActive(false);
            //prefabToCreate.transform.Find("MarkAsRead").gameObject.SetActive(false);
            position = new Vector3(coordinates[indexPosition].Position.X, coordinates[indexPosition].Position.Y, coordinates[indexPosition].Position.Z);
            rotation = Quaternion.Euler(coordinates[indexPosition].Rotation.X, coordinates[indexPosition].Rotation.Y, coordinates[indexPosition].Rotation.Z);
            _ = Instantiate(prefabToCreate, position, rotation) as GameObject;
        }
    }
}