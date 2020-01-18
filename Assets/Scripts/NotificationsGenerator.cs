using System;
using UnityEngine;

namespace Logic
{
    public class NotificationsGenerator
    {
        private System.Random random = new System.Random();
        private const int sourcesNumber = 4;

        public Notification getNotification()
        {
            int sourceIndex = random.Next(0, sourcesNumber);
            long timestamp = DateTime.Now.Ticks;
            bool isSilent = random.Next(0, 2) == 0;
            string id = Guid.NewGuid().ToString();
            NotificationSource notificationSource = (NotificationSource)Enum.GetValues(typeof(NotificationSource)).GetValue(sourceIndex);
            string sourceName = EnumDescription.getDescription(notificationSource);
            NotificationImage notificationImage = (NotificationImage)Enum.GetValues(typeof(NotificationImage)).GetValue(sourceIndex);
            string sourceImage = EnumDescription.getDescription(notificationImage);
			NotificationColor notificationColor = (NotificationColor)Enum.GetValues(typeof(NotificationColor)).GetValue(sourceIndex);
            Color sourceColor = EnumDescription.getColor(EnumDescription.getDescription(notificationColor));
            Array values = Enum.GetValues(typeof(NotificationAuthor));
            NotificationAuthor notificationAuthor = (NotificationAuthor)values.GetValue(random.Next(values.Length));
            string author = EnumDescription.getDescription(notificationAuthor);
            values = Enum.GetValues(typeof(NotificationText));
            NotificationText notificationText = (NotificationText)values.GetValue(random.Next(values.Length));
            string text = EnumDescription.getDescription(notificationText);
            values = Enum.GetValues(typeof(NotificationIcon));
            NotificationIcon notificationIcon = (NotificationIcon)values.GetValue(random.Next(values.Length));
            string icon = EnumDescription.getDescription(notificationIcon);
            values = Enum.GetValues(typeof(NotificationHeader));
            NotificationHeader notificationHeader = (NotificationHeader)values.GetValue(random.Next(values.Length));
            string header = EnumDescription.getDescription(notificationHeader);
            if (isSilent)
            {
                sourceColor = EnumDescription.getColor(EnumDescription.getDescription(NotificationColor.Silent));
                header = "Silent: " + header;
            }
            Notification notification = new Notification(id, sourceImage, sourceName, author, icon, text, header, timestamp, isSilent, sourceColor);            
            return notification;
        }
    }
}