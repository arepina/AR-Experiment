namespace Logic
{
    public class Triple
    {
        private float x;
        private float y;
        private float z;

        public Triple(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float X { get { return x; } }
        public float Y { get { return y; } }
        public float Z { get { return z; } }

        public override string ToString()
        {
            return X + " " + Y + " " + Z;
        }
    }
}
