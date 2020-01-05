namespace Logic
{
    class MainClass
    {
        private static NotificationsGenerator generator;

        public void stop()
        {
            generator.Stop();
        }

        static void Main(string[] args)
        {
            generator = new NotificationsGenerator();
            generator.Start();
        }
    }
}
