using UnityEngine;

namespace Logic
{
    public class Coordinates
    {
        private Vector3 position;
        private Quaternion rotation;

        public Coordinates(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }

        public Vector3 Position { get { return position; } }
        public Quaternion Rotation { get { return rotation; } }

        public override string ToString()
        {
            return "Position: " + Position.ToString() + " Rotation: " + Rotation.ToString();
        }
    }
}