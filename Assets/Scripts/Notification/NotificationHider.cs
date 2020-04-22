using System.Collections;
using Logic;
using TMPro;
using UnityEngine;

public class NotificationHider : MonoBehaviour
{
    public float hideTimeOfTheNotificationAfterArrival;
    public GameObject id;

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
        string sourceName = transform.Find("Source").GetComponent<TextMeshPro>().text;
        string tag = "MarkAsRead";
        Notification n = FindObjectOfType<Storage>().getFromStorage(id.GetComponent<TextMeshPro>().text, sourceName);
        if (n.isSilent)
        {
            sourceName = GlobalCommon.silentGroupKey;
        }
        FindObjectOfType<Storage>().removeFromStorage(id.GetComponent<TextMeshPro>().text, sourceName, tag);
        Destroy(gameObject);
    }
}
