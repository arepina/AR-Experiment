using Logic;
using UnityEngine;

public class NotificationsPositioner : MonoBehaviour
{
    public void Start()
    {
        EventManager.AddHandler(EVENT.SceneRebuild, UpdateNotifications);
    }

    void UpdateNotifications()
    {
        foreach (Transform child in transform)
        {
            Vector3 newPos = child.position;
            newPos.x = 0;
            newPos.z = FindObjectOfType<NotificationsHodlerReferencedContent>().DistanceFromCamera;
            child.position = newPos;
            child.rotation = new Quaternion(0,0,0,0);
            Debug.Log(child.position);
        }
    }
}
