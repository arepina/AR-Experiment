using System.Collections;
using UnityEngine;

public class NotificationHider : MonoBehaviour
{
    public float hideTimeOfTheNotificationAfterArrival;

    void Start()
    {
        if (transform.parent.name != "TrayHolder")
        {
            StartCoroutine(Destroyer());
        }
    }

    IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(hideTimeOfTheNotificationAfterArrival);
        Destroy(gameObject);
    }
}
