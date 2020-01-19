using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    internal class Global
    {
        public static Dictionary<string, NotificationsStorage> notifications = new Dictionary<string, NotificationsStorage>();
        public static GameObject prefabToCreate;
        public static int notificationsInColumn;
        public static int notificationColumns;
        public static string silentGroupKey = "_silent_";
    }
}