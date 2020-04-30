﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Logic
{
    public class GeneratorRunner : MonoBehaviour
    {
        private NotificationsGenerator notificationsGenerator = new NotificationsGenerator();
        private int notificationIndex = 0;
        private int alreadyCorrect = 0;
        internal bool isRunning = false;
        private float pause = 0;

        public void Stop()
        {
            StopAllCoroutines();
            isRunning = false;
            alreadyCorrect = 0;
            notificationIndex = 0;
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
                isRunning = false;
                pause = ExperimentData.timeInSeconds / ExperimentData.notificationsNumber;
                StartCoroutine(Runner());
            }
        }

        private IEnumerator Runner()
        {
            Debug.Log("Started" + DateTime.Now);
            HashSet<int> numbers = new HashSet<int>();
            for (int k = 0; k < ExperimentData.trialsNumber; k++)
            {
                Debug.Log("Trial: " + (k + 1));
                for (int i = 0; i < ExperimentData.notificationsNumber; ++i)
                {
                    if (!numbers.Contains(i))
                    {
                        numbers.Add(i);
                        Debug.Log(i);
                        Generator();
                        yield return new WaitForSeconds(pause);
                    }
                }
                SaveTrialData();
                FindObjectOfType<Storage>().removeAllFromStorage();
                if (k == ExperimentData.trialsNumber - 1) {
                    yield return new WaitForSeconds(0);
                }
                else {
                    yield return new WaitForSeconds(GlobalCommon.pauseBetweenTrials);
                }
            }
            Debug.Log("Stoped" + DateTime.Now);
            Stop();
        }

        private void Generator()
        {
            int atWhichToGenerateHaveToActNotification = ExperimentData.notificationsNumber / ExperimentData.numberOfHaveToActNotifications;
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
