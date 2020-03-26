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
        public int notificationsInColumnTray;
        public int notificationColumnsTray;
        public static string silentGroupKey = "_silent_";
        public float distanceFromCamera;
        public float angle;
        //for actions
        public float waitForActionToBeAcceptedPeriod;
        //for around
        public float X;
        public float Y;
        public float Z;
        //for wave
        public float speed;
        public float duration;
        public float leftX;
        public float rightX;
        public float waveY;
    }
}