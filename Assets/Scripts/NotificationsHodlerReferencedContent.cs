using Logic;
using UnityEngine;

public class NotificationsHodlerReferencedContent : MonoBehaviour
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

    void Start()
    {
        Vector3 posTo = Camera.transform.position + Camera.transform.forward * DistanceFromCamera;
        Quaternion rotTo = Quaternion.LookRotation(transform.position - Camera.transform.position);
        transform.rotation = rotTo;
        transform.position = posTo;
    }

    void Update()
    {
        Debug.Log("Ang: " + Camera.transform.rotation.eulerAngles.x);
        Quaternion rotTo = Quaternion.LookRotation(transform.position - Camera.transform.position);
        if (Camera.transform.rotation.eulerAngles.x > 180 && Mathf.Abs(Camera.transform.rotation.eulerAngles.x - 360) >= TrayShowAngle)
        {
            EventManager.Broadcast(EVENT.ShowTray);
            return;
        }

        if (transform.childCount == 0)
        {
            oldCameraPosition = Camera.transform;
            Vector3 posTo = Camera.transform.position + Camera.transform.forward * DistanceFromCamera;
            if (Camera.transform.rotation.eulerAngles.x > 180 && Mathf.Abs(Camera.transform.rotation.eulerAngles.x - 360) > AngleToTheHorizon)
            {
                posTo.y = DistanceFromCamera * Mathf.Tan(Mathf.Deg2Rad * AngleToTheHorizon);
            }
            transform.rotation = rotTo;
            transform.position = posTo;
        }
        else
        {
            //Vector3 posTo = transform.position;
            //Debug.Log(Camera.transform.rotation.eulerAngles.x);
            //if (Camera.transform.rotation.eulerAngles.x > 180 && Mathf.Abs(Camera.transform.rotation.eulerAngles.x - 360) > AngleToTheHorizon)
            //{
            //    posTo.y = DistanceFromCamera * Mathf.Tan(Mathf.Deg2Rad * AngleToTheHorizon);
            //    transform.position = posTo;
            //}
            //else
            //{
            //    float newAngle = Camera.transform.rotation.eulerAngles.x;
            //    posTo.y = DistanceFromCamera * Mathf.Tan(Mathf.Deg2Rad * newAngle);
            //    transform.position = posTo;
            //    rotTo = Quaternion.LookRotation(transform.position - oldCameraPosition.position);
            //    transform.rotation = rotTo;
            //}
        }
    }
}
