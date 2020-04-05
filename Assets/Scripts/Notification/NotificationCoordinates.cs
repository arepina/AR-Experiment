using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    public class NotificationCoordinates : MonoBehaviour
    {
        public static List<Coordinates> formInFrontOfMobileCoordinatesArray()
        {
            Debug.Log("formInFrontOfMobileCoordinatesArray");
            float distanceFromCamera = FindObjectOfType<GlobalCommon>().distanceFromCamera;
            List<Coordinates> coordinates = new List<Coordinates>();
            coordinates.Add(new Coordinates(new Vector3(0f, -1.1f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, 0f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, 1.1f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));
            return coordinates;
        }

        public static List<Coordinates> formInFrontOfStickerCoordinatesArray()
        {
            Debug.Log("formInFrontOfStickerCoordinatesArray");
            float distanceFromCamera = FindObjectOfType<GlobalCommon>().distanceFromCamera;
            List<Coordinates> coordinates = new List<Coordinates>();
            coordinates.Add(new Coordinates(new Vector3(0f, -1.6f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, 0f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, 1.6f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));
            return coordinates;
        }

        public static List<Coordinates> formAroundMobileCoordinatesArray()
        {
            Debug.Log("formAroundMobileCoordinatesArray");
            GlobalAround globalAround = FindObjectOfType<GlobalAround>();
            Vector3 center = globalAround.position;
            Quaternion rotation = globalAround.rotation;
            List<Coordinates> coordinates = new List<Coordinates>();
            float xDist = -2.4f;
            float zDist = -1.2f;
            coordinates.Add(new Coordinates(new Vector3(center.x, center.y, center.z), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x, center.y, center.z + zDist), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x, center.y, center.z + zDist * 2), rotation));

            coordinates.Add(new Coordinates(new Vector3(center.x + xDist, center.y, center.z), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist, center.y, center.z + zDist), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist, center.y, center.z + zDist * 2), rotation));

            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 2, center.y, center.z), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 2, center.y, center.z + zDist), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 2, center.y, center.z + zDist * 2), rotation));

            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 3, center.y, center.z), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 3, center.y, center.z + zDist), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 3, center.y, center.z + zDist * 2), rotation));

            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 4, center.y, center.z), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 4, center.y, center.z + zDist), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 4, center.y, center.z + zDist * 2), rotation));
            return coordinates;
        }

        public static List<Coordinates> formAroundStickerCoordinatesArray()
        {
            Debug.Log("formAroundStickerCoordinatesArray");
            GlobalAround globalAround = FindObjectOfType<GlobalAround>();
            Vector3 center = globalAround.position;
            Quaternion rotation = globalAround.rotation;
            List<Coordinates> coordinates = new List<Coordinates>();
            float xDist = -1.7f;
            float zDist = -2f;
            coordinates.Add(new Coordinates(new Vector3(center.x, center.y, center.z), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x, center.y, center.z + zDist), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x, center.y, center.z + zDist * 2), rotation));

            coordinates.Add(new Coordinates(new Vector3(center.x + xDist, center.y, center.z), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist, center.y, center.z + zDist), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist, center.y, center.z + zDist * 2), rotation));

            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 2, center.y, center.z), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 2, center.y, center.z + zDist), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 2, center.y, center.z + zDist * 2), rotation));

            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 3, center.y, center.z), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 3, center.y, center.z + zDist), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 3, center.y, center.z + zDist * 2), rotation));

            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 4, center.y, center.z), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 4, center.y, center.z + zDist), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 4, center.y, center.z + zDist * 2), rotation));
            return coordinates;
        }

        public static List<Coordinates> formTrayCoordinatesArrayMobile()
        {
            Debug.Log("formTrayCoordinatesArrayMobile");
            float distanceFromCamera = FindObjectOfType<GlobalCommon>().distanceFromCamera;
            List<Coordinates> coordinates = new List<Coordinates>();
            coordinates.Add(new Coordinates(new Vector3(0f, -2.2f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, -1.1f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, 0f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, 1.1f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, 2.2f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));

            coordinates.Add(new Coordinates(new Vector3(-3.5f, -2.2f, distanceFromCamera), new Quaternion(0, 315, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-3.5f, -1.1f, distanceFromCamera), new Quaternion(0, 315, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-3.5f, 0f, distanceFromCamera), new Quaternion(0, 315, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-3.5f, 1.1f, distanceFromCamera), new Quaternion(0, 315, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-3.5f, 2.2f, distanceFromCamera), new Quaternion(0, 315, 0, 0)));

            coordinates.Add(new Coordinates(new Vector3(3.5f, -2.2f, distanceFromCamera), new Quaternion(0, 45, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(3.5f, -1.1f, distanceFromCamera), new Quaternion(0, 45, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(3.5f, 0f, distanceFromCamera), new Quaternion(0, 45, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(3.5f, 1.1f, distanceFromCamera), new Quaternion(0, 45, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(3.5f, 2.2f, distanceFromCamera), new Quaternion(0, 45, 0, 0)));

            coordinates.Add(new Coordinates(new Vector3(-5f, -2.2f, 0), new Quaternion(0, 270, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-5f, -1.1f, 0), new Quaternion(0, 270, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-5f, 0f, 0), new Quaternion(0, 270, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-5f, 1.1f, 0), new Quaternion(0, 270, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-5f, 2.2f, 0), new Quaternion(0, 270, 0, 0)));

            coordinates.Add(new Coordinates(new Vector3(5f, -2.2f, 0), new Quaternion(0, 90, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(5f, -1.1f, 0), new Quaternion(0, 90, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(5f, 0f, 0), new Quaternion(0, 90, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(5f, 1.1f, 0), new Quaternion(0, 90, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(5f, 2.2f, 0), new Quaternion(0, 90, 0, 0)));

            coordinates.Add(new Coordinates(new Vector3(-3.5f, -2.2f, -distanceFromCamera), new Quaternion(0, 225, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-3.5f, -1.1f, -distanceFromCamera), new Quaternion(0, 225, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-3.5f, 0f, -distanceFromCamera), new Quaternion(0, 225, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-3.5f, 1.1f, -distanceFromCamera), new Quaternion(0, 225, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-3.5f, 2.2f, -distanceFromCamera), new Quaternion(0, 225, 0, 0)));

            coordinates.Add(new Coordinates(new Vector3(3.5f, -2.2f, -distanceFromCamera), new Quaternion(0, 135, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(3.5f, -1.1f, -distanceFromCamera), new Quaternion(0, 135, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(3.5f, 0f, -distanceFromCamera), new Quaternion(0, 135, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(3.5f, 1.1f, -distanceFromCamera), new Quaternion(0, 135, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(3.5f, 2.2f, -distanceFromCamera), new Quaternion(0, 135, 0, 0)));

            coordinates.Add(new Coordinates(new Vector3(0f, -2.2f, -distanceFromCamera), new Quaternion(0, 180, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, -1.1f, -distanceFromCamera), new Quaternion(0, 180, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, 0f, -distanceFromCamera), new Quaternion(0, 180, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, 1.1f, -distanceFromCamera), new Quaternion(0, 180, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, 2.2f, -distanceFromCamera), new Quaternion(0, 180, 0, 0)));
            return coordinates;
        }

        public static List<Coordinates> formTrayCoordinatesArraySticker()
        {
            Debug.Log("formTrayCoordinatesArraySticker");
            float distanceFromCamera = FindObjectOfType<GlobalCommon>().distanceFromCamera;
            List<Coordinates> coordinates = new List<Coordinates>();
            coordinates.Add(new Coordinates(new Vector3(0f, -3.2f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, -1.6f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, 0f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, 1.6f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, 3.2f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));

            coordinates.Add(new Coordinates(new Vector3(-3.5f, -3.2f, distanceFromCamera), new Quaternion(0, 315, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-3.5f, -1.6f, distanceFromCamera), new Quaternion(0, 315, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-3.5f, 0f, distanceFromCamera), new Quaternion(0, 315, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-3.5f, 1.6f, distanceFromCamera), new Quaternion(0, 315, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-3.5f, 3.2f, distanceFromCamera), new Quaternion(0, 315, 0, 0)));

            coordinates.Add(new Coordinates(new Vector3(3.5f, -3.2f, distanceFromCamera), new Quaternion(0, 45, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(3.5f, -1.6f, distanceFromCamera), new Quaternion(0, 45, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(3.5f, 0f, distanceFromCamera), new Quaternion(0, 45, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(3.5f, 1.6f, distanceFromCamera), new Quaternion(0, 45, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(3.5f, 3.2f, distanceFromCamera), new Quaternion(0, 45, 0, 0)));

            coordinates.Add(new Coordinates(new Vector3(-5f, -3.2f, 0), new Quaternion(0, 270, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-5f, -1.6f, 0), new Quaternion(0, 270, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-5f, 0f, 0), new Quaternion(0, 270, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-5f, 1.6f, 0), new Quaternion(0, 270, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-5f, 3.2f, 0), new Quaternion(0, 270, 0, 0)));

            coordinates.Add(new Coordinates(new Vector3(5f, -3.2f, 0), new Quaternion(0, 90, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(5f, -1.6f, 0), new Quaternion(0, 90, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(5f, 0f, 0), new Quaternion(0, 90, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(5f, 1.6f, 0), new Quaternion(0, 90, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(5f, 3.2f, 0), new Quaternion(0, 90, 0, 0)));

            coordinates.Add(new Coordinates(new Vector3(-3.5f, -3.2f, -distanceFromCamera), new Quaternion(0, 225, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-3.5f, -1.6f, -distanceFromCamera), new Quaternion(0, 225, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-3.5f, 0f, -distanceFromCamera), new Quaternion(0, 225, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-3.5f, 1.6f, -distanceFromCamera), new Quaternion(0, 225, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(-3.5f, 3.2f, -distanceFromCamera), new Quaternion(0, 225, 0, 0)));

            coordinates.Add(new Coordinates(new Vector3(3.5f, -3.2f, -distanceFromCamera), new Quaternion(0, 135, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(3.5f, -1.6f, -distanceFromCamera), new Quaternion(0, 135, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(3.5f, 0f, -distanceFromCamera), new Quaternion(0, 135, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(3.5f, 1.6f, -distanceFromCamera), new Quaternion(0, 135, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(3.5f, 3.2f, -distanceFromCamera), new Quaternion(0, 135, 0, 0)));

            coordinates.Add(new Coordinates(new Vector3(0f, -3.2f, -distanceFromCamera), new Quaternion(0, 180, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, -1.6f, -distanceFromCamera), new Quaternion(0, 180, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, 0f, -distanceFromCamera), new Quaternion(0, 180, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, 1.6f, -distanceFromCamera), new Quaternion(0, 180, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, 3.2f, -distanceFromCamera), new Quaternion(0, 180, 0, 0)));
            return coordinates;
        }
    }
}