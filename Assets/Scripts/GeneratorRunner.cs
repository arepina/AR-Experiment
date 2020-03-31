using System.Collections;
using Lgic;
using Logic;
using UnityEngine;

public class GeneratorRunner : MonoBehaviour
{
    private NotificationsGenerator notificationsGenerator = new NotificationsGenerator();
    private TrialDataStorage trialDataStorage = new TrialDataStorage();
    private Logger myLogger = new Logger(new LogHandler());
    public bool isRunning;
    private uint atWhichToGenerateHaveToActNotification = 0;
    private int notificationIndex;
    private int alreadyCorrect = 0;

    public void Start()
    {
        isRunning = true;
        atWhichToGenerateHaveToActNotification = FindObjectOfType<ExperimentData>().notificationsNumber / FindObjectOfType<ExperimentData>().numberOfHaveToActNotifications; // 0 - correct, 1 - incorrect
        notificationIndex = 0;
        myLogger.Log("Started");
    }

    public void Stop()
    {
        isRunning = false;
        myLogger.Log("Stopped");
    }

    public void Update()
    {
        if (isRunning) StartCoroutine(Wait());
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
            notificationIndex += 1;
            yield return new WaitForSeconds(pause);
            isRunning = true;
        }
        else
        {
            trialDataStorage.NextTrialExperiment(experiment.subjectNumber, experiment.design, experiment.trialNumber,
                experiment.timeInSeconds, experiment.notificationsNumber,
                experiment.numberOfHaveToActNotifications, experiment.numberOfNonIgnoredHaveToActNotifications,
                experiment.sumOfReactionTimeToNonIgnoredHaveToActNotifications, experiment.numberOfInCorrectlyActedNotifications);
            trialDataStorage.SaveExperimentData();
            yield return new WaitForSeconds(pause);
            Stop();
        }
    }
}
