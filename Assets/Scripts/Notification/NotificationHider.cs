using System.Collections;
using Logic;
using TMPro;
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
        string id = transform.Find("Id").GetComponent<TextMeshPro>().text;
        string sourceName = transform.Find("Source").GetComponent<TextMeshPro>().text;
        string tag = "MarkAsRead";
        FindObjectOfType<Storage>().removeFromStorage(id, sourceName, tag);
        Destroy(gameObject);
    }
}
