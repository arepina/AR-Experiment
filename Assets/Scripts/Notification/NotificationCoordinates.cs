using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    public class NotificationCoordinates : MonoBehaviour
    {
        public static List<Coordinates> formInFrontOfMobileCoordinatesArray()
        {
            float distanceFromCamera = FindObjectOfType<GlobalCommon>().distanceFromCamera;
            List<Coordinates> coordinates = new List<Coordinates>();
            coordinates.Add(new Coordinates(new Vector3(0f, -1.1f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, 0f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, 1.1f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));
            return coordinates;
        }

        public static List<Coordinates> formInFrontOfStickerCoordinatesArray()
        {
            float distanceFromCamera = FindObjectOfType<GlobalCommon>().distanceFromCamera;
            List<Coordinates> coordinates = new List<Coordinates>();
            coordinates.Add(new Coordinates(new Vector3(0f, -1.6f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, 0f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));
            coordinates.Add(new Coordinates(new Vector3(0f, 1.6f, distanceFromCamera), new Quaternion(0, 0, 0, 0)));
            return coordinates;
        }

        public static List<Coordinates> formAroundMobileCoordinatesArray()
        {
            float distanceFromCamera = FindObjectOfType<GlobalCommon>().distanceFromCamera;
            GlobalAround globalAround = FindObjectOfType<GlobalAround>();
            Vector3 center = globalAround.position;
            Quaternion rotation = globalAround.rotation;
            List<Coordinates> coordinates = new List<Coordinates>();
            float xDist = -2.4f;
            float yDist = -1.2f;
            coordinates.Add(new Coordinates(new Vector3(center.x, center.y, center.z + distanceFromCamera), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x, center.y, center.z + distanceFromCamera), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x, center.y, center.z + distanceFromCamera), rotation));

            coordinates.Add(new Coordinates(new Vector3(center.x + xDist, center.y + yDist, center.z + distanceFromCamera), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist, center.y + yDist, center.z + distanceFromCamera), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist, center.y + yDist, center.z + distanceFromCamera), rotation));

            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 2, center.y + 2 * yDist, center.z + distanceFromCamera), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 2, center.y + 2 * yDist, center.z + distanceFromCamera), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 2, center.y + 2 * yDist, center.z + distanceFromCamera), rotation));

            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 3, center.y + 3 * yDist, center.z + distanceFromCamera), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 3, center.y + 3 * yDist, center.z + distanceFromCamera), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 3, center.y + 3 * yDist, center.z + distanceFromCamera), rotation));

            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 4, center.y + 4 * yDist, center.z + distanceFromCamera), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 4, center.y + 4 * yDist, center.z + distanceFromCamera), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 4, center.y + 4 * yDist, center.z + distanceFromCamera), rotation));
            return coordinates;
        }

        public static List<Coordinates> formAroundStickerCoordinatesArray()
        {
            float distanceFromCamera = FindObjectOfType<GlobalCommon>().distanceFromCamera;
            GlobalAround globalAround = FindObjectOfType<GlobalAround>();
            Vector3 center = globalAround.position;
            Quaternion rotation = globalAround.rotation;
            List<Coordinates> coordinates = new List<Coordinates>();
            float xDist = -1.7f;
            float yDist = -1.2f;
            coordinates.Add(new Coordinates(new Vector3(center.x, center.y, center.z + distanceFromCamera), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x, center.y, center.z + distanceFromCamera), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x, center.y, center.z + distanceFromCamera), rotation));

            coordinates.Add(new Coordinates(new Vector3(center.x + xDist, center.y + yDist, center.z + distanceFromCamera), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist, center.y + yDist, center.z + distanceFromCamera), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist, center.y + yDist, center.z + distanceFromCamera), rotation));

            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 2, center.y + 2 * yDist, center.z + distanceFromCamera), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 2, center.y + 2 * yDist, center.z + distanceFromCamera), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 2, center.y + 2 * yDist, center.z + distanceFromCamera), rotation));

            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 3, center.y + 3 * yDist, center.z + distanceFromCamera), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 3, center.y + 3 * yDist, center.z + distanceFromCamera), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 3, center.y + 3 * yDist, center.z + distanceFromCamera), rotation));

            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 4, center.y + 4 * yDist, center.z + distanceFromCamera), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 4, center.y + 4 * yDist, center.z + distanceFromCamera), rotation));
            coordinates.Add(new Coordinates(new Vector3(center.x + xDist * 4, center.y + 4 * yDist, center.z + distanceFromCamera), rotation));
            return coordinates;
        }

        public static List<Coordinates> formTrayCoordinatesArrayMobile()
        {
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