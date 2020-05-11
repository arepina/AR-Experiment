using Logic;
using UnityEngine;

public class WavesHolderReferencedContent : MonoBehaviour
{   
    [Tooltip("The camera is needed to emulate a torso reference frame")]
    public GameObject Camera;

    [Tooltip("Angle when tray should be shown")]
    public float TrayShowAngle = 0.02f;

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
        if (Camera.transform.position.y >= TrayShowAngle)
        {
            EventManager.Broadcast(EVENT.ShowTray);
            return;
        }
    }
}