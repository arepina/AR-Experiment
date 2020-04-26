using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Logic
{
    public class GeneratorRunner : MonoBehaviour
    {
        private NotificationsGenerator notificationsGenerator = new NotificationsGenerator();
        private int notificationIndex = 0;
        private int alreadyCorrect = 0;
        internal static bool isRunning = false;
        private float pause = 0;

        public void Stop()
        {
            StopAllCoroutines();
            isRunning = false;
            ReturnToMainMenu();
        }

        private void ReturnToMainMenu()
        {
            Scene mainMenuScene = SceneManager.GetSceneByName("MainMenu");
            if (mainMenuScene.isLoaded)
                SceneManager.SetActiveScene(mainMenuScene);
            else
                SceneManager.LoadScene("MainMenu");
            SceneManager.UnloadSceneAsync(GlobalCommon.currentTypeName);
        }

        public void Update()
        {
            if (isRunning)
            {
                pause = ExperimentData.timeInSeconds / ExperimentData.notificationsNumber;
                StartCoroutine(Generator()); //todo fix the coroutines
            }
        }

        private IEnumerator Generator()
        {
        	isRunning = false;
            Debug.Log(DateTime.Now);
            int atWhichToGenerateHaveToActNotification = ExperimentData.notificationsNumber / ExperimentData.numberOfHaveToActNotifications;
            if (ExperimentData.notificationsNumber > 0 && notificationIndex < ExperimentData.notificationsNumber)
            {
                bool generateHaveToAct = notificationIndex % atWhichToGenerateHaveToActNotification == 0 && alreadyCorrect < ExperimentData.numberOfHaveToActNotifications;
                if (generateHaveToAct)
                {
                    alreadyCorrect += 1;
                }
                Notification notification = notificationsGenerator.getNotification(generateHaveToAct);
                var storage = FindObjectOfType<Storage>();
                storage.addToStorage(notification);
                EventManager.Broadcast(EVENT.NotificationCreated);
                SaveLogData(notification);
                notificationIndex += 1;
                yield return new WaitForSeconds(pause);
                isRunning = true;
            }
            else
            {
                SaveTrialData();
                yield return new WaitForSeconds(pause);
                Stop();
            }
        }

        private void SaveLogData(Notification notification)
        {
            string logInfo = notification.ToString(GlobalCommon.currentTypeName, "CREATED", "-");
            CSVSaver.saveToFile(logInfo);
        }

        private void SaveTrialData()
        {
            FindObjectOfType<TrialDataStorage>().NextTrialExperiment(ExperimentData.subjectNumber, GlobalCommon.currentTypeName, ExperimentData.trialsNumber,
                    ExperimentData.timeInSeconds, ExperimentData.notificationsNumber,
                    ExperimentData.numberOfHaveToActNotifications, ExperimentData.numberOfNonIgnoredHaveToActNotifications,
                    ExperimentData.sumOfReactionTimeToNonIgnoredHaveToActNotifications, ExperimentData.numberOfInCorrectlyActedNotifications);
            FindObjectOfType<TrialDataStorage>().SaveExperimentData();
        }
    }
}
