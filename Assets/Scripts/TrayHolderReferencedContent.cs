using UnityEngine;

public class TrayHolderReferencedContent : MonoBehaviour
{
    [Tooltip("The camera is needed to emulate a torso reference frame")]
    public GameObject Camera;

    [Tooltip("The distance from the camera that this object should be placed")]
    public float DistanceFromCamera = 10f;

    [Tooltip("If checked, makes objecta move smoothly")]
    public bool SimulateInertia = false;

    [Tooltip("The speed at which this object changes its position, if the inertia effect is enabled")]
    public float LerpSpeed = 5f;

    [Tooltip("Height of the holder lower which the hodler's height will not be changed")]
    public float DownBarier = -1f;

    [Tooltip("Height of the holder upper which the hodler's height will not be changed")]
    public float UpBarier = 1f;

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
        float alpha = -Camera.transform.rotation.eulerAngles.y + 90;
        if(alpha > 360)
        {
            alpha = alpha - 360;
        }
        float a = Mathf.Pow(transform.position.x - Camera.transform.position.x, 2);
        float c = Mathf.Pow(transform.position.z - Camera.transform.position.z, 2);
        float r = Mathf.Sqrt(a + c);
        float cos = Mathf.Cos(alpha * Mathf.Deg2Rad);
        float sin = Mathf.Sin(alpha * Mathf.Deg2Rad);
        if (alpha == 90.0 || alpha == 270.0) {
            cos = 0;
        }
        if (alpha == 0 || alpha == 180.0){
            sin = 0;
        }

        float x2 = Camera.transform.position.x + r * cos;
        float y2 = Camera.transform.position.y;
        float z2 = Camera.transform.position.z + r * sin;
        if (y2 < DownBarier)
        {
            y2 = DownBarier;
        }
        if (y2 > UpBarier)
        {
            y2 = UpBarier;
        }
        Vector3 posTo = new Vector3(x2, y2, z2);

        if (SimulateInertia)
        {
            float posSpeed = Time.deltaTime * LerpSpeed;
            transform.position = Vector3.SlerpUnclamped(transform.position, posTo, posSpeed);
        }
        else
        {
            transform.position = posTo;
        }
    }
}
