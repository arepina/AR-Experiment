using System.Collections;
using Logic;
using UnityEngine;

public class GeneratorRunner : MonoBehaviour
{
    private NotificationsGenerator notificationsGenerator = new NotificationsGenerator();
    private Logger myLogger = new Logger(new LogHandler());
    public int notificationsToGenerateNumber;
    public int experimentDurationInSeconds;
    public bool isRunning;

    public void Start()
    {
        isRunning = true;
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
        int pause = experimentDurationInSeconds / notificationsToGenerateNumber;
        if (notificationsToGenerateNumber > 0)
        {
            Notification notification = notificationsGenerator.getNotification();
            var storage = FindObjectOfType<Storage>();
            storage.addToStorage(notification);
            EventManager.Broadcast(EVENT.NotificationCreated);
        }
        yield return new WaitForSeconds(pause);
        isRunning = true;
    }
}
