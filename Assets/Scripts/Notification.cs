using UnityEngine;

namespace Logic
{
    public class Notification
    {
        private string sourceImage;
        private string sourceName;
        private string author;
        private string icon;
        private string text;
        private long timestamp;
        private bool silent;
        private Color color;
        private string id;
        private bool isCorrect;

        public Notification(string id, string sourceImage, string sourceName, string author, string icon, string text, long timestamp, bool silent, Color color, bool isCorrect) { 
            this.sourceImage = sourceImage;
            this.sourceName = sourceName;
            this.author = author;
            this.text = text;
            this.timestamp = timestamp;
            this.silent = silent;
            this.color = color;
            this.icon = icon;
            this.id = id;
            this.isCorrect = isCorrect;
        }

        public override string ToString()
        {
            return string.Format("id: {0}, sourceImage: {1}, sourceName: {2}, author: {3}, icon: {4}, text : {5}, timestamp: {6}, silent: {7}, color: {8}",
                                                                              id, sourceImage, sourceName, author, icon, text, timestamp, silent, color);
        }

        public string getLogString(ExperimentData experiment, string reactionTime)
        {
            return string.Format("subjectNumber: {0}, design: {1}, trialNumber: {2}, creationTime: {3}, isCorrect: {4}, reactionTime: {5}",
                                                                                experiment.subjectNumber, experiment.design, experiment.transform, timestamp, isCorrect, reactionTime);
        }

        public string Id
        {
            get
            {
                return id;
            }
        }

        public string SourceImage
        {
            get
            {
                return sourceImage;
            }
        }

        public string Icon
        {
            get
            {
                return icon;
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

        public Color Color
        {
            get
            {
                return color;
            }
        }

        public string Text
        {
            get
            {
                return text;
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