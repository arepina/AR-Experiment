using UnityEngine;

namespace Logic
{
    public class ExperimentData : MonoBehaviour
    {
        public int subjectNumber;
        internal string design = FindObjectOfType<GlobalCommon>().typeName;
        public int trialNumber;
        public int timeInSeconds = 0;
        public int notificationsNumber = 0;
        public string notificationSource;
        public string notificationAuthor;
        public int numberOfHaveToActNotifications;

        internal int numberOfNonIgnoredHaveToActNotifications = 0;
        internal float sumOfReactionTimeToNonIgnoredHaveToActNotifications = 0;

        internal int numberOfInCorrectlyActedNotifications = 0;

        public static void sendData() {
             

            //todo send data here
        }

    }
}