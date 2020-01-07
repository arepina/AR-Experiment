namespace Logic
{
    public class Notification
    {
        private string sourceImage;
        private string sourceName;
        private string author;
        private string text;
        private string header;
        private long timestamp;
        private bool silent;

        public Notification(string sourceImage, string sourceName, string author, string text, string header, long timestamp, bool silent)
        {
            this.sourceImage = sourceImage;
            this.sourceName = sourceName;
            this.author = author;
            this.text = text;
            this.header = header;
            this.timestamp = timestamp;
            this.silent = silent;
        }

        public override string ToString()
        {
            return "sourceImage: " + sourceImage +
                    " sourceName: " + sourceName +
                    " author: " + author +
                    " text: " + text +
                    " header: " + header +
                    " time: " + timestamp +
                    " isSilent: " + silent;
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

        public long Timestamp
        {
            get
            {
                return timestamp;
            }
        }

        public bool isSilent
        {
            get
            {
                return silent;
            }
        }
    }
}
