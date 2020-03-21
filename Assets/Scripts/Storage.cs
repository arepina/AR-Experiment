using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Logic
{
    public class Storage : MonoBehaviour
    {
        private Dictionary<string, NotificationsStorage> orderedNotifications = new Dictionary<string, NotificationsStorage>();

        internal Dictionary<string, NotificationsStorage> getStorage()
        {
            return orderedNotifications;
        }

        internal void addToStorage(Notification notification)
        {
            Stack<Notification> sourceNotifications = new Stack<Notification>();
            string sourceName = notification.SourceName;
            if (notification.isSilent) sourceName = Global.silentGroupKey;
            if (Global.notifications.ContainsKey(sourceName)) sourceNotifications = Global.notifications[sourceName].Storage;
            sourceNotifications.Push(notification);
            NotificationsStorage newNotificationsStorage = new NotificationsStorage(sourceNotifications, notification.Timestamp);
            Global.notifications[sourceName] = newNotificationsStorage;
            orderedNotifications = createOrderedStorage(sourceName);
        }

        internal void removeFromStorage(string id, string sourceName)
        {
            NotificationsStorage newStorage = Global.notifications[sourceName];
            Stack<Notification> newNotificationsStorage = new Stack<Notification>();
            foreach (Notification notification in newStorage.Storage)
            {
                if (!notification.Id.Equals(id))
                {
                    newNotificationsStorage.Push(notification);
                }
            }
            newStorage.Storage = newNotificationsStorage;
            Global.notifications[sourceName] = newStorage;
            orderedNotifications = createOrderedStorage(sourceName);
        }

        internal void removeAllFromStorage(string sourceName)
        {
            Global.notifications.Remove(sourceName);
            sourceName = null;
            orderedNotifications = createOrderedStorage(sourceName);
        }

        private Dictionary<string, NotificationsStorage> createOrderedStorage(string sourceName)
        {
            NotificationsStorage silentGroup = null;
            if (Global.notifications.ContainsKey(Global.silentGroupKey))
            {
                silentGroup = Global.notifications[Global.silentGroupKey];
                Global.notifications.Remove(Global.silentGroupKey);
            }
            Dictionary<string, NotificationsStorage> orderedNotifications = Global.notifications.OrderByDescending(x => x.Value.LatestTimestamp)
                                                                                         .ToDictionary(d => d.Key, d => d.Value);
            if (silentGroup != null || sourceName == Global.silentGroupKey)
            {
                orderedNotifications.Add(Global.silentGroupKey, silentGroup); // silent are always the last
                Global.notifications.Add(Global.silentGroupKey, silentGroup);
            }
            return orderedNotifications;
        }
    }
}
