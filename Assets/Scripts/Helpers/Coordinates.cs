namespace Logic
{
    public class Coordinates
    {
        private Triple position;
        private Triple rotation;

        public Coordinates(Triple position, Triple rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }

        public Triple Position { get { return position; } }
        public Triple Rotation { get { return rotation; } }
    }
}