using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotificationServices = UnityEngine.iOS.NotificationServices;
using NotificationType = UnityEngine.iOS.NotificationType;

public class IPhoneNotificationsReceiver : MonoBehaviour
{
    void Start()
    {
        NotificationServices.RegisterForNotifications(
            NotificationType.Alert |
            NotificationType.Badge |
            NotificationType.Sound);
    }

    void Update()
    {
        ManagePushNotifications();
    }

    private void ManagePushNotifications()
    {
        if (NotificationServices.remoteNotifications.Length != 0)
        {            
            Debug.Log("Hello");
        }
    }
}
