using UnityEngine;

public class ARPositioner : MonoBehaviour
{
    public GameObject GameObjectToPlace;
    public float maxRayDistance = 30.0f;
    public LayerMask collisionLayer = 1 << 10;  //ARKitPlane layer

    void Update()
    {
        Debug.Log("OB" + GameObjectToPlace.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRayDistance, collisionLayer))
        {
            GameObjectToPlace.transform.position = hit.point;
            Debug.Log(string.Format("x:{0:0.######} y:{1:0.######} z:{2:0.######}", GameObjectToPlace.transform.position.x, GameObjectToPlace.transform.position.y, GameObjectToPlace.transform.position.z));

            //and the rotation from the transform of the plane collider
            GameObjectToPlace.transform.rotation = hit.transform.rotation;

        }
    }
}
