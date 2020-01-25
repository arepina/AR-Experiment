﻿using UnityEngine;

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

        public Notification(string id, string sourceImage, string sourceName, string author, string icon, string text, long timestamp, bool silent, Color color)
        {
            this.sourceImage = sourceImage;
            this.sourceName = sourceName;
            this.author = author;
            this.text = text;
            this.timestamp = timestamp;
            this.silent = silent;
            this.color = color;
            this.icon = icon;
            this.id = id;
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