using UnityEngine;

namespace Logic
{
    public class ExperimentData : MonoBehaviour
    {
        public int subjectNumber;
        private string design = "";
        public int trialNumber;
        private float time = 0;
        private int notificationsNumber = 0;
        private float selectedCorrectAnswersPercentage = 0;
        private float reactionTimeForCorrectAnswers = 0;
        private float lostCorrectAnswersPercentage = 0;
        private float selectedIncorrectAnswersPercentage = 0;
        public string note;
        private int correctAnswers = 0;
        private int wrongAnswers = 0;

        public void sendData() {
            design = FindObjectOfType<GlobalCommon>().typeName;
            time = FindObjectOfType<GeneratorRunner>().experimentDurationInSeconds;
            notificationsNumber = FindObjectOfType<GeneratorRunner>().notificationsToGenerateNumber;
            selectedCorrectAnswersPercentage = correctAnswers / FindObjectOfType<GeneratorRunner>().notificationsToGenerateNumber * 100;
            reactionTimeForCorrectAnswers = 0; // todo
            lostCorrectAnswersPercentage = 0; // todo
            selectedIncorrectAnswersPercentage = wrongAnswers / FindObjectOfType<GeneratorRunner>().notificationsToGenerateNumber * 100;
        }

    }
}