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

        public string getLogString(string status, string reactionTime, string design, string timestamp, bool isCorrect)
        {
            return string.Format("subjectNumber: {0}, design: {1}, trialNumber: {2}, creationTime: {3}, isCorrect: {4}, status: {5}, reactionTime: {6}",
                                                                                subjectNumber, design, transform, timestamp, isCorrect, status, reactionTime);
        }
    }
}