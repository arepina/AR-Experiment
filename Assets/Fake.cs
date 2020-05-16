using Logic;
using UnityEngine;

public class Fake : MonoBehaviour
{
    [Tooltip("The camera is needed to emulate a torso reference frame")]
    public GameObject Camera;

    [Tooltip("The distance from the camera that this object should be placed")]
    private float DistanceFromCamera = 10f;

    [Tooltip("Angle to the horizon")]
    public float AngleToTheHorizon = 8f;

    [Tooltip("Angle when tray should be shown")]
    public float TrayShowAngle = 12f;

    private Transform oldCameraPosition;

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
        Quaternion rotTo = Quaternion.LookRotation(transform.position - Camera.transform.position);     
        Vector3 posTo = Camera.transform.position + Camera.transform.forward * DistanceFromCamera;
        transform.rotation = rotTo;
        transform.position = posTo;
       
    }
}
