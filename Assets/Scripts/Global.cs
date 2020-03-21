using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    internal class Global
    {
        public static Dictionary<string, NotificationsStorage> notifications = new Dictionary<string, NotificationsStorage>();
        public static GameObject prefabToCreate;
        public static string typeName;
        public static int notificationsInColumn;
        public static int notificationColumns;
        public static string silentGroupKey = "_silent_";
        public static bool isTrayOpened = false;
        public static Triple aroundCoordinatesCenter;
        public static float distanceFromCamera;
        public static float angle;
    }
}