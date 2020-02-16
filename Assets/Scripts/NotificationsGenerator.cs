using System;
using UnityEngine;

namespace Logic
{
    public class NotificationsGenerator
    {
        private System.Random random = new System.Random();
        private Logger myLogger = new Logger(new LogHandler());
        private const int sourcesNumber = 4;

        public Notification getNotification()
        {
            int sourceIndex = random.Next(0, sourcesNumber);
            long timestamp = DateTime.Now.Ticks;
            bool isSilent = false;//random.Next(0, 2) == 0;
            string id = Guid.NewGuid().ToString();
            NotificationSource notificationSource = (NotificationSource)Enum.GetValues(typeof(NotificationSource)).GetValue(sourceIndex);
            string sourceName = EnumDescription.getDescription(notificationSource);
            NotificationImage notificationImage = (NotificationImage)Enum.GetValues(typeof(NotificationImage)).GetValue(sourceIndex);
            string sourceImage = EnumDescription.getDescription(notificationImage);
			NotificationColor notificationColor = (NotificationColor)Enum.GetValues(typeof(NotificationColor)).GetValue(sourceIndex);
            Color sourceColor = EnumDescription.getColor(EnumDescription.getDescription(notificationColor));
            Array values = Enum.GetValues(typeof(NotificationAuthor));
            int authorIndex = random.Next(values.Length);
            NotificationAuthor notificationAuthor = (NotificationAuthor)values.GetValue(authorIndex);
            string author = EnumDescription.getDescription(notificationAuthor);
            values = Enum.GetValues(typeof(NotificationIcon));
            NotificationIcon notificationIcon = (NotificationIcon)values.GetValue(authorIndex);
            string icon = EnumDescription.getDescription(notificationIcon);
            string text;
            if(sourceIndex == 2 || sourceIndex == 3) // post or youtube
            {
                values = Enum.GetValues(typeof(NotificationHeader));
                NotificationHeader notificationHeader = (NotificationHeader)values.GetValue(random.Next(values.Length));
                text = EnumDescription.getDescription(notificationHeader);
            }
            else // messengers
            {
                values = Enum.GetValues(typeof(NotificationText));
                NotificationText notificationText = (NotificationText)values.GetValue(random.Next(values.Length));
                text = EnumDescription.getDescription(notificationText);
            }
            if (isSilent)
            {
                sourceColor = EnumDescription.getColor(EnumDescription.getDescription(NotificationColor.Silent));
                sourceImage = "_silent_";
            }
            Notification notification = new Notification(id, sourceImage, sourceName, author, icon, text, timestamp, isSilent, sourceColor);
            myLogger.Log(string.Format("Notification {0} was created", notification));
            return notification;
        }
    }
}