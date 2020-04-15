﻿using UnityEngine;

namespace Logic
{
    public class GlobalCommon : MonoBehaviour
    {
        public GameObject notification;
        public GameObject trayNotification;
        public string typeName;
        public int notificationsInColumn;
        public int notificationColumns;
        public int notificationsInColumnTray;
        public int notificationColumnsTray;
        public static string silentGroupKey = "_silent_";
        public float angle;
        public float waitForActionToBeAcceptedPeriod;
    }
}