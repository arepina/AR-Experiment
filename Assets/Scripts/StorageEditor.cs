using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class StorageEditor
    {
        private SceneEditor sceneEditor = new SceneEditor();

        internal void addToStorage(Notification notification)
        {
            Stack<Notification> sourceNotifications = new Stack<Notification>();
            string sourceName = notification.SourceName;
            if (notification.isSilent) sourceName = Global.silentGroupKey;
            if (Global.notifications.ContainsKey(sourceName)) sourceNotifications = Global.notifications[sourceName].Storage;
            sourceNotifications.Push(notification);
            NotificationsStorage newNotificationsStorage = new NotificationsStorage(sourceNotifications, notification.Timestamp);
            Global.notifications[sourceName] = newNotificationsStorage;
            Dictionary<string, NotificationsStorage> orderedNotifications = createOrderedStorage(sourceName);
            if (Global.isTrayOpened) sceneEditor.buildTray(orderedNotifications);
            else sceneEditor.rebuildScene(orderedNotifications);
        }

        internal void closeTray()
        {
            Global.isTrayOpened = false;
            sceneEditor.buildTray(new Dictionary<string, NotificationsStorage>());            
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
            Dictionary<string, NotificationsStorage> orderedNotifications = createOrderedStorage(sourceName);
            if (Global.isTrayOpened) sceneEditor.buildTray(orderedNotifications);
            else sceneEditor.rebuildScene(orderedNotifications);
        }

        internal void removeAllFromStorage(string sourceName)
        {
            Global.notifications.Remove(sourceName);
            sourceName = null;
            Dictionary<string, NotificationsStorage> orderedNotifications = createOrderedStorage(sourceName);
            if (Global.isTrayOpened) sceneEditor.buildTray(orderedNotifications);
            else sceneEditor.rebuildScene(orderedNotifications);
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
