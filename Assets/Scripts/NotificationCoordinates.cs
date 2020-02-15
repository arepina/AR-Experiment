using System.Collections.Generic;

namespace Logic
{
    public class NotificationCoordinates
    {
        public static List<Coordinates> formInFrontOfMobileCoordinatesArray()
        {
            List<Coordinates> coordinates = new List<Coordinates>();
            coordinates.Add(new Coordinates(new Triple(0f, 1.1f, 6f), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 0f, 6f), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, -1.1f, 6f), new Triple(0, 0, 0)));
            return coordinates;
        }

        public static List<Coordinates> formInFrontOfStickerCoordinatesArray()
        {
            List<Coordinates> coordinates = new List<Coordinates>();
            coordinates.Add(new Coordinates(new Triple(0f, 1.6f, 6f), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 0f, 6f), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, -1.6f, 6f), new Triple(0, 0, 0)));
            return coordinates;
        }

        public static List<Coordinates> formTrayCoordinatesArray()
        {
            List<Coordinates> coordinates = new List<Coordinates>();
            coordinates.Add(new Coordinates(new Triple(0f, -2.2f, 6f), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, -1.1f, 6f), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 0f, 6f), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 1.1f, 6f), new Triple(0, 0, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 2.2f, 6f), new Triple(0, 0, 0)));

            coordinates.Add(new Coordinates(new Triple(-3.5f, -2.2f, 4f), new Triple(0, 315, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, -1.1f, 4f), new Triple(0, 315, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, 0f, 4f), new Triple(0, 315, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, 1.1f, 4f), new Triple(0, 315, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, 2.2f, 4f), new Triple(0, 315, 0)));

            coordinates.Add(new Coordinates(new Triple(3.5f, -2.2f, 4f), new Triple(0, 45, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, -1.1f, 4f), new Triple(0, 45, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, 0f, 4f), new Triple(0, 45, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, 1.1f, 4f), new Triple(0, 45, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, 2.2f, 4f), new Triple(0, 45, 0)));

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

            coordinates.Add(new Coordinates(new Triple(-3.5f, -2.2f, -4f), new Triple(0, 225, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, -1.1f, -4f), new Triple(0, 225, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, 0f, -4f), new Triple(0, 225, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, 1.1f, -4f), new Triple(0, 225, 0)));
            coordinates.Add(new Coordinates(new Triple(-3.5f, 2.2f, -4f), new Triple(0, 225, 0)));

            coordinates.Add(new Coordinates(new Triple(3.5f, -2.2f, -4f), new Triple(0, 135, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, -1.1f, -4f), new Triple(0, 135, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, 0f, -4f), new Triple(0, 135, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, 1.1f, -4f), new Triple(0, 135, 0)));
            coordinates.Add(new Coordinates(new Triple(3.5f, 2.2f, -4f), new Triple(0, 135, 0)));

            coordinates.Add(new Coordinates(new Triple(0f, -2.2f, -6f), new Triple(0, 180, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, -1.1f, -6f), new Triple(0, 180, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 0f, -6f), new Triple(0, 180, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 1.1f, -6f), new Triple(0, 180, 0)));
            coordinates.Add(new Coordinates(new Triple(0f, 2.2f, -6f), new Triple(0, 180, 0)));
            return coordinates;
        }
    }
}