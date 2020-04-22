﻿using System.Collections;
using Logic;
using UnityEngine;

public class GeneratorRunner : MonoBehaviour
{
    private NotificationsGenerator notificationsGenerator = new NotificationsGenerator();
    public bool isRunning;
    private uint atWhichToGenerateHaveToActNotification = 0;
    private int notificationIndex;
    private int alreadyCorrect = 0;

    public void Start()
    {
        if(FindObjectOfType<ExperimentData>().numberOfHaveToActNotifications > FindObjectOfType<ExperimentData>().notificationsNumber)
        {
            throw new System.Exception("Illegal arguments: numberOfHaveToActNotifications should be <= then notificationsNumber");
        }
        atWhichToGenerateHaveToActNotification = FindObjectOfType<ExperimentData>().notificationsNumber / FindObjectOfType<ExperimentData>().numberOfHaveToActNotifications; 
        notificationIndex = 0;
        Debug.Log("Started");
    }

    public void Stop()
    {
        isRunning = false;
        Debug.Log("Stopped");
    }

    public void Update()
    {
        if (isRunning)
        {
            StartCoroutine(Wait());
        }
    }

    public IEnumerator Wait()
    {
        isRunning = false;
        ExperimentData experiment = FindObjectOfType<ExperimentData>();
        uint pause = experiment.timeInSeconds / experiment.notificationsNumber;        
        if (experiment.notificationsNumber > 0 && notificationIndex < experiment.notificationsNumber)
        {
            bool generateHaveToAct = notificationIndex % atWhichToGenerateHaveToActNotification == 0 && alreadyCorrect < experiment.numberOfHaveToActNotifications;
            if (generateHaveToAct)
            {
                alreadyCorrect += 1;
            }
            Notification notification = notificationsGenerator.getNotification(generateHaveToAct);
            var storage = FindObjectOfType<Storage>();
            storage.addToStorage(notification);
            EventManager.Broadcast(EVENT.NotificationCreated);
            saveLogData(notification);
            notificationIndex += 1;
            yield return new WaitForSeconds(pause);
            isRunning = true;
        }
        else
        {
            saveTrialData(experiment);
            yield return new WaitForSeconds(pause);
            Stop();
        }
    }

    private void saveLogData(Notification notification)
    {
        string logInfo = notification.ToString(FindObjectOfType<ExperimentData>(), FindObjectOfType<GlobalCommon>().typeName, "CREATED", "-");
        CSVSaver.saveToFile(logInfo);
    }

    private void saveTrialData(ExperimentData experiment)
    {
        FindObjectOfType<TrialDataStorage>().NextTrialExperiment(experiment.subjectNumber, FindObjectOfType<GlobalCommon>().typeName, experiment.trialNumber,
                experiment.timeInSeconds, experiment.notificationsNumber,
                experiment.numberOfHaveToActNotifications, experiment.numberOfNonIgnoredHaveToActNotifications,
                experiment.sumOfReactionTimeToNonIgnoredHaveToActNotifications, experiment.numberOfInCorrectlyActedNotifications);
        FindObjectOfType<TrialDataStorage>().SaveExperimentData();
    }
}
