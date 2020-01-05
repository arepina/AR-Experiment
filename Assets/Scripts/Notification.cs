namespace Logic
{
    public class Notification
    {
        private string sourceImage;
        private string sourceName;
        private string author;
        private string text;
        private string header;
        private string timestamp;

        public Notification(string sourceImage, string sourceName, string author, string text, string header, string timestamp)
        {
            this.sourceImage = sourceImage;
            this.sourceName = sourceName;
            this.author = author;
            this.text = text;
            this.header = header;
            this.timestamp = timestamp;
        }

        public override string ToString()
        {
            return "sourceImage: " + sourceImage +
                    " sourceName: " + sourceName +
                    " author: " + author +
                    " text: " + text +
                    " header: " + header + " time: " + timestamp;
        }

        public string SourceImage
        {
            get
            {
                return sourceImage;
            }
        }

        public string SourceName
        {
            get
            {
                return sourceName;
            }
        }

        public string Author
        {
            get
            {
                return author;
            }
        }

        public string Text
        {
            get
            {
                return text;
            }
        }

        public string Header
        {
            get
            {
                return header;
            }
        }

        public string Timestamp
        {
            get
            {
                return timestamp;
            }
        }
    }
}
