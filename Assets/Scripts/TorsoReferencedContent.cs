using UnityEngine;

public class TorsoReferencedContent : MonoBehaviour
{
    [Tooltip("The camera is needed to emulate a torso reference frame")]
    public GameObject Camera;

    [Tooltip("The distance from the camera that this object should be placed")]
    public float DistanceFromCamera = 0.75f;

    public Transform Track;

    [Tooltip("If checked, makes objecta move smoothly")]
    public bool SimulateInertia = false;

    [Tooltip("The speed at which this object changes its position, if the inertia effect is enabled")]
    public float PositionLerpSpeed = 5f;

    [Tooltip("The speed at which this object changes its rotation, if the inertia effect is enabled")]
    public float RotationLerpSpeed = 5f;

    public float Pitch = 10f;

    public float Yaw = 0f;

    public int MotionNum = 5;

    public float TimeThreshold = 0.25f;

    public float DistanceThreshold = 0.2f;

    void OnEnable()
    {
        // Disable the script if there is no camera
        if (Camera == null)
        {
            Debug.LogError("Error: TorsoReferencedContent.Camera is not set. Disabling the script.");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        Vector3 vec = getVectorFromCameraToObject();
        Vector3 posTo = Camera.transform.position + vec;
        Quaternion rotTo = Quaternion.LookRotation(transform.position - Camera.transform.position);

        if (SimulateInertia)
        {
            float posSpeed = Time.deltaTime * PositionLerpSpeed;
            transform.position = Vector3.SlerpUnclamped(transform.position, posTo, posSpeed);

            float rotSpeed = Time.deltaTime * RotationLerpSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotTo, rotSpeed);
        }
        else
        {
            transform.position = posTo;
            transform.rotation = rotTo;
        }
    }

    private Vector3 getVectorFromCameraToObject()
    {
        Quaternion rotation = Quaternion.Euler(Pitch,
            Camera.transform.rotation.eulerAngles.y + Yaw,
            Camera.transform.rotation.eulerAngles.z);
        return rotation * (Vector3.forward * DistanceFromCamera);
    }
}
