using UnityEngine;

namespace Logic
{
    public class ExperimentData : MonoBehaviour
    {
        public uint subjectNumber;
        public uint trialNumber;
        public uint timeInSeconds;
        public uint notificationsNumber;
        public string notificationSource;
        public string notificationAuthor;
        public uint numberOfHaveToActNotifications;

        internal uint numberOfNonIgnoredHaveToActNotifications;
        internal float sumOfReactionTimeToNonIgnoredHaveToActNotifications;

        internal uint numberOfInCorrectlyActedNotifications;
    }
}