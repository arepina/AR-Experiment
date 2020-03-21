using UnityEngine;

namespace Logic
{
    public class Global : MonoBehaviour
    {
        public GameObject notification;
        public GameObject trayNotification;
        public string typeName;
        public int notificationsInColumn;
        public int notificationColumns;
        public static string silentGroupKey = "_silent_";
        public Triple aroundCoordinatesCenter;
        public float distanceFromCamera;
        public float angle;
    }
}