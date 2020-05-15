using Logic;
using UnityEngine;

public class WavesHolderReferencedContent : MonoBehaviour
{   
    [Tooltip("The camera is needed to emulate a torso reference frame")]
    public GameObject Camera;

    [Tooltip("Angle when tray should be shown")]
    public float TrayShowAngle = 12f;

    void OnEnable()
    {
        if (Camera == null)
        {
            Debug.LogError("Error: Camera is not set. Disabling the script.");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        if (Camera.transform.rotation.eulerAngles.x > 180 && Mathf.Abs(Camera.transform.rotation.eulerAngles.x - 360) >= TrayShowAngle)
        {
            EventManager.Broadcast(EVENT.ShowTray);
            return;
        }

        transform.position = Camera.transform.position + Camera.transform.forward * transform.position.z;
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.transform.position);
    }
}