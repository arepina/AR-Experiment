using Logic;
using UnityEngine;

public class NotificationsHodlerReferencedContent : MonoBehaviour
{
    [Tooltip("The camera is needed to emulate a torso reference frame")]
    public GameObject Camera;

    [Tooltip("The distance from the camera that this object should be placed")]
    public float DistanceFromCamera = 10f;

    [Tooltip("If checked, makes objecta move smoothly")]
    public bool SimulateInertia = false;

    [Tooltip("The speed at which this object changes its position, if the inertia effect is enabled")]
    public float LerpSpeed = 5f;

    [Tooltip("Angle to the horizon")]
    public float AngleToTheHorizon = -0.03f;

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
        if (posTo.y < AngleToTheHorizon)
        {
            posTo.y = AngleToTheHorizon;
        }
        if (posTo.y >= TrayShowAngle)
        {
            EventManager.Broadcast(EVENT.ShowTray);
            return;
        }

        Quaternion rotTo = Quaternion.LookRotation(transform.position - Camera.transform.position);

        if (SimulateInertia)
        {
            float posSpeed = Time.deltaTime * LerpSpeed;
            transform.position = Vector3.SlerpUnclamped(transform.position, posTo, posSpeed);
            float rotSpeed = Time.deltaTime * LerpSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotTo, rotSpeed);
        }
        else
        {
            transform.position = posTo;
            transform.rotation = rotTo;
        }
    }
}
