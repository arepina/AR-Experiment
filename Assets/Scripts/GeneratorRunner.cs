using System.Collections;
using Logic;
using UnityEngine;

public class GeneratorRunner : MonoBehaviour
{
    private NotificationsGenerator notificationsGenerator = new NotificationsGenerator();
    private Logger myLogger = new Logger(new LogHandler());

    public void Start()
    {
        myLogger.Log("Started");
        StartCoroutine(Wait());
    }

    public void Stop()
    {
        myLogger.Log("Stopped");
    }

    public IEnumerator Wait()
    {
        int pause = FindObjectOfType<ExperimentData>().timeInSeconds / FindObjectOfType<ExperimentData>().notificationsNumber;
        if (FindObjectOfType<ExperimentData>().notificationsNumber > 0)
        {
            Notification notification = notificationsGenerator.getNotification();
            var storage = FindObjectOfType<Storage>();
            storage.addToStorage(notification);
            EventManager.Broadcast(EVENT.NotificationCreated);
        }
        yield return new WaitForSeconds(pause);
    }
}
