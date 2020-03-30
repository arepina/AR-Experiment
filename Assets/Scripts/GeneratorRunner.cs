using System.Collections;
using Logic;
using UnityEngine;

public class GeneratorRunner : MonoBehaviour
{
    private NotificationsGenerator notificationsGenerator = new NotificationsGenerator();
    private Logger myLogger = new Logger(new LogHandler());
    public bool isRunning;
    private int atWhichToGenerateHaveToActNotification = 0;
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
        int pause = FindObjectOfType<ExperimentData>().timeInSeconds / FindObjectOfType<ExperimentData>().notificationsNumber;        
        if (FindObjectOfType<ExperimentData>().notificationsNumber > 0 && notificationIndex < FindObjectOfType<ExperimentData>().notificationsNumber)
        {
            bool generateHaveToAct = notificationIndex % atWhichToGenerateHaveToActNotification == 0 && alreadyCorrect < FindObjectOfType<ExperimentData>().numberOfHaveToActNotifications;
            if (generateHaveToAct)
            {
                alreadyCorrect += 1;
            }
            Notification notification = notificationsGenerator.getNotification(generateHaveToAct);
            var storage = FindObjectOfType<Storage>();
            storage.addToStorage(notification);
            EventManager.Broadcast(EVENT.NotificationCreated);
            notificationIndex += 1;
        }
        yield return new WaitForSeconds(pause);
        isRunning = true;
    }
}
