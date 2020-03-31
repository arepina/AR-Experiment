using UnityEngine;

namespace Logic
{
    public class ExperimentData : MonoBehaviour
    {
        public uint subjectNumber;
        internal string design = FindObjectOfType<GlobalCommon>().typeName;
        public uint trialNumber;
        public uint timeInSeconds = 0;
        public uint notificationsNumber = 0;
        public string notificationSource;
        public string notificationAuthor;
        public uint numberOfHaveToActNotifications;

        internal uint numberOfNonIgnoredHaveToActNotifications = 0;
        internal float sumOfReactionTimeToNonIgnoredHaveToActNotifications = 0;

        internal uint numberOfInCorrectlyActedNotifications = 0;
    }
}