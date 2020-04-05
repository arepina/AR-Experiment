﻿using System.Collections.Generic;
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
            coordinates.Add(new Coordinates(new Triple(0f, -1.1f, distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 0f, distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 1.1f, distanceFromCamera), new Triple(0, 0, 0)));
            return coordinates;
        }

        public static List<Coordinates> formInFrontOfStickerCoordinatesArray()
        {
            Debug.Log("formInFrontOfStickerCoordinatesArray");
            float distanceFromCamera = FindObjectOfType<GlobalCommon>().distanceFromCamera;
            List<Coordinates> coordinates = new List<Coordinates>();
            coordinates.Add(new Coordinates(new Triple(0f, -1.6f, distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 0f, distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 1.6f, distanceFromCamera), new Triple(0, 0, 0)));
            return coordinates;
        }

        public static List<Coordinates> formAroundMobileCoordinatesArray()
        {
            Debug.Log("formAroundMobileCoordinatesArray");
            GlobalAround globalAround = FindObjectOfType<GlobalAround>();
            Triple center = new Triple(globalAround.XAroundOnly, globalAround.YAroundOnly, globalAround.ZAroundOnly);
            GlobalCommon global = FindObjectOfType<GlobalCommon>();
            float distanceFromCamera = global.distanceFromCamera;
            List<Coordinates> coordinates = new List<Coordinates>();
            float xDist = -2.4f;
            float zDist = -1.2f;
            coordinates.Add(new Coordinates(new Triple(center.X, center.Y, center.Z + distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(center.X, center.Y, center.Z + distanceFromCamera + zDist), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(center.X, center.Y, center.Z + distanceFromCamera + zDist * 2), new Triple(0, 0, 0)));

            coordinates.Add(new Coordinates(new Triple(center.X + xDist, center.Y, center.Z + distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(center.X + xDist, center.Y, center.Z + distanceFromCamera + zDist), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(center.X + xDist, center.Y, center.Z + distanceFromCamera + zDist * 2), new Triple(0, 0, 0)));

            coordinates.Add(new Coordinates(new Triple(center.X + xDist * 2, center.Y, center.Z + distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(center.X + xDist * 2, center.Y, center.Z + distanceFromCamera + zDist), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(center.X + xDist * 2, center.Y, center.Z + distanceFromCamera + zDist * 2), new Triple(0, 0, 0)));

            coordinates.Add(new Coordinates(new Triple(center.X + xDist * 3, center.Y, center.Z + distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(center.X + xDist * 3, center.Y, center.Z + distanceFromCamera + zDist), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(center.X + xDist * 3, center.Y, center.Z + distanceFromCamera + zDist * 2), new Triple(0, 0, 0)));

            coordinates.Add(new Coordinates(new Triple(center.X + xDist * 4, center.Y, center.Z + distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(center.X + xDist * 4, center.Y, center.Z + distanceFromCamera + zDist), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(center.X + xDist * 4, center.Y, center.Z + distanceFromCamera + zDist * 2), new Triple(0, 0, 0)));
            return coordinates;
        }

        public static List<Coordinates> formAroundStickerCoordinatesArray()
        {
            Debug.Log("formAroundStickerCoordinatesArray");
            GlobalAround globalAround = FindObjectOfType<GlobalAround>();
            Triple center = new Triple(globalAround.XAroundOnly, globalAround.YAroundOnly, globalAround.ZAroundOnly);
            GlobalCommon global = FindObjectOfType<GlobalCommon>();
            float distanceFromCamera = global.distanceFromCamera;
            List<Coordinates> coordinates = new List<Coordinates>();
            float xDist = -1.7f;
            float zDist = -2f;
            coordinates.Add(new Coordinates(new Triple(center.X, center.Y, center.Z + distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(center.X, center.Y, center.Z + distanceFromCamera + zDist), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(center.X, center.Y, center.Z + distanceFromCamera + zDist * 2), new Triple(0, 0, 0)));

            coordinates.Add(new Coordinates(new Triple(center.X + xDist, center.Y, center.Z + distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(center.X + xDist, center.Y, center.Z + distanceFromCamera + zDist), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(center.X + xDist, center.Y, center.Z + distanceFromCamera + zDist * 2), new Triple(0, 0, 0)));

            coordinates.Add(new Coordinates(new Triple(center.X + xDist * 2, center.Y, center.Z + distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(center.X + xDist * 2, center.Y, center.Z + distanceFromCamera + zDist), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(center.X + xDist * 2, center.Y, center.Z + distanceFromCamera + zDist * 2), new Triple(0, 0, 0)));

            coordinates.Add(new Coordinates(new Triple(center.X + xDist * 3, center.Y, center.Z + distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(center.X + xDist * 3, center.Y, center.Z + distanceFromCamera + zDist), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(center.X + xDist * 3, center.Y, center.Z + distanceFromCamera + zDist * 2), new Triple(0, 0, 0)));

            coordinates.Add(new Coordinates(new Triple(center.X + xDist * 4, center.Y, center.Z + distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(center.X + xDist * 4, center.Y, center.Z + distanceFromCamera + zDist), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(center.X + xDist * 4, center.Y, center.Z + distanceFromCamera + zDist * 2), new Triple(0, 0, 0)));
            return coordinates;
        }

        public static List<Coordinates> formTrayCoordinatesArrayMobile()
        {
            Debug.Log("formTrayCoordinatesArrayMobile");
            float distanceFromCamera = FindObjectOfType<GlobalCommon>().distanceFromCamera;
            List<Coordinates> coordinates = new List<Coordinates>();
            coordinates.Add(new Coordinates(new Triple(0f, -2.2f, distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, -1.1f, distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 0f, distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 1.1f, distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 2.2f, distanceFromCamera), new Triple(0, 0, 0)));

            coordinates.Add(new Coordinates(new Triple(-3.5f, -2.2f, distanceFromCamera), new Triple(0, 315, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, -1.1f, distanceFromCamera), new Triple(0, 315, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, 0f, distanceFromCamera), new Triple(0, 315, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, 1.1f, distanceFromCamera), new Triple(0, 315, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, 2.2f, distanceFromCamera), new Triple(0, 315, 0)));

            coordinates.Add(new Coordinates(new Triple(3.5f, -2.2f, distanceFromCamera), new Triple(0, 45, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, -1.1f, distanceFromCamera), new Triple(0, 45, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, 0f, distanceFromCamera), new Triple(0, 45, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, 1.1f, distanceFromCamera), new Triple(0, 45, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, 2.2f, distanceFromCamera), new Triple(0, 45, 0)));

            coordinates.Add(new Coordinates(new Triple(-5f, -2.2f, 0), new Triple(0, 270, 0)));
            coordinates.Add(new Coordinates(new Triple(-5f, -1.1f, 0), new Triple(0, 270, 0)));
            coordinates.Add(new Coordinates(new Triple(-5f, 0f, 0), new Triple(0, 270, 0)));
            coordinates.Add(new Coordinates(new Triple(-5f, 1.1f, 0), new Triple(0, 270, 0)));
            coordinates.Add(new Coordinates(new Triple(-5f, 2.2f, 0), new Triple(0, 270, 0)));

            coordinates.Add(new Coordinates(new Triple(5f, -2.2f, 0), new Triple(0, 90, 0)));
            coordinates.Add(new Coordinates(new Triple(5f, -1.1f, 0), new Triple(0, 90, 0)));
            coordinates.Add(new Coordinates(new Triple(5f, 0f, 0), new Triple(0, 90, 0)));
            coordinates.Add(new Coordinates(new Triple(5f, 1.1f, 0), new Triple(0, 90, 0)));
            coordinates.Add(new Coordinates(new Triple(5f, 2.2f, 0), new Triple(0, 90, 0)));

            coordinates.Add(new Coordinates(new Triple(-3.5f, -2.2f, -distanceFromCamera), new Triple(0, 225, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, -1.1f, -distanceFromCamera), new Triple(0, 225, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, 0f, -distanceFromCamera), new Triple(0, 225, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, 1.1f, -distanceFromCamera), new Triple(0, 225, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, 2.2f, -distanceFromCamera), new Triple(0, 225, 0)));

            coordinates.Add(new Coordinates(new Triple(3.5f, -2.2f, -distanceFromCamera), new Triple(0, 135, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, -1.1f, -distanceFromCamera), new Triple(0, 135, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, 0f, -distanceFromCamera), new Triple(0, 135, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, 1.1f, -distanceFromCamera), new Triple(0, 135, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, 2.2f, -distanceFromCamera), new Triple(0, 135, 0)));

            coordinates.Add(new Coordinates(new Triple(0f, -2.2f, -distanceFromCamera), new Triple(0, 180, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, -1.1f, -distanceFromCamera), new Triple(0, 180, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 0f, -distanceFromCamera), new Triple(0, 180, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 1.1f, -distanceFromCamera), new Triple(0, 180, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 2.2f, -distanceFromCamera), new Triple(0, 180, 0)));
            return coordinates;
        }

        public static List<Coordinates> formTrayCoordinatesArraySticker()
        {
            Debug.Log("formTrayCoordinatesArraySticker");
            float distanceFromCamera = FindObjectOfType<GlobalCommon>().distanceFromCamera;
            List<Coordinates> coordinates = new List<Coordinates>();
            coordinates.Add(new Coordinates(new Triple(0f, -3.2f, distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, -1.6f, distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 0f, distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 1.6f, distanceFromCamera), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 3.2f, distanceFromCamera), new Triple(0, 0, 0)));

            coordinates.Add(new Coordinates(new Triple(-3.5f, -3.2f, distanceFromCamera), new Triple(0, 315, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, -1.6f, distanceFromCamera), new Triple(0, 315, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, 0f, distanceFromCamera), new Triple(0, 315, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, 1.6f, distanceFromCamera), new Triple(0, 315, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, 3.2f, distanceFromCamera), new Triple(0, 315, 0)));

            coordinates.Add(new Coordinates(new Triple(3.5f, -3.2f, distanceFromCamera), new Triple(0, 45, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, -1.6f, distanceFromCamera), new Triple(0, 45, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, 0f, distanceFromCamera), new Triple(0, 45, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, 1.6f, distanceFromCamera), new Triple(0, 45, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, 3.2f, distanceFromCamera), new Triple(0, 45, 0)));

            coordinates.Add(new Coordinates(new Triple(-5f, -3.2f, 0), new Triple(0, 270, 0)));
            coordinates.Add(new Coordinates(new Triple(-5f, -1.6f, 0), new Triple(0, 270, 0)));
            coordinates.Add(new Coordinates(new Triple(-5f, 0f, 0), new Triple(0, 270, 0)));
            coordinates.Add(new Coordinates(new Triple(-5f, 1.6f, 0), new Triple(0, 270, 0)));
            coordinates.Add(new Coordinates(new Triple(-5f, 3.2f, 0), new Triple(0, 270, 0)));

            coordinates.Add(new Coordinates(new Triple(5f, -3.2f, 0), new Triple(0, 90, 0)));
            coordinates.Add(new Coordinates(new Triple(5f, -1.6f, 0), new Triple(0, 90, 0)));
            coordinates.Add(new Coordinates(new Triple(5f, 0f, 0), new Triple(0, 90, 0)));
            coordinates.Add(new Coordinates(new Triple(5f, 1.6f, 0), new Triple(0, 90, 0)));
            coordinates.Add(new Coordinates(new Triple(5f, 3.2f, 0), new Triple(0, 90, 0)));

            coordinates.Add(new Coordinates(new Triple(-3.5f, -3.2f, -distanceFromCamera), new Triple(0, 225, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, -1.6f, -distanceFromCamera), new Triple(0, 225, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, 0f, -distanceFromCamera), new Triple(0, 225, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, 1.6f, -distanceFromCamera), new Triple(0, 225, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, 3.2f, -distanceFromCamera), new Triple(0, 225, 0)));

            coordinates.Add(new Coordinates(new Triple(3.5f, -3.2f, -distanceFromCamera), new Triple(0, 135, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, -1.6f, -distanceFromCamera), new Triple(0, 135, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, 0f, -distanceFromCamera), new Triple(0, 135, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, 1.6f, -distanceFromCamera), new Triple(0, 135, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, 3.2f, -distanceFromCamera), new Triple(0, 135, 0)));

            coordinates.Add(new Coordinates(new Triple(0f, -3.2f, -distanceFromCamera), new Triple(0, 180, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, -1.6f, -distanceFromCamera), new Triple(0, 180, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 0f, -distanceFromCamera), new Triple(0, 180, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 1.6f, -distanceFromCamera), new Triple(0, 180, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 3.2f, -distanceFromCamera), new Triple(0, 180, 0)));
            return coordinates;
        }
    }
}