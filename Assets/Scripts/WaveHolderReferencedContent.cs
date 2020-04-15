
using Logic;
using UnityEngine;

public class WaveHolderReferencedContent : MonoBehaviour
{
    [Tooltip("The camera is needed to emulate a torso reference frame")]
    public GameObject Camera;

    [Tooltip("The distance from the camera that this object should be placed")]
    public float DistanceFromCamera = 10f;

    [Tooltip("Angle when tray should be shown")]
    public float TrayShowAngle = 3f;

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
        Vector3 posTo = Camera.transform.position + Camera.transform.forward * DistanceFromCamera;
        if (posTo.y >= TrayShowAngle)
        {
            EventManager.Broadcast(EVENT.ShowTray);
            return;
        }

        Quaternion rotTo = Quaternion.LookRotation(transform.position - Camera.transform.position);
   
        transform.position = posTo;
        transform.rotation = rotTo;
    }
}
