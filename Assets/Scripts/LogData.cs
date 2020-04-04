using System;
using System.Collections.Generic;

namespace Logic
{
    [Serializable]
    public class LogData
    {
        public string Notification;

        // Instructions were taken from here: https://youtu.be/z9b5aRfrz7M
        private static readonly string _formURI = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSczrBh7HKVPPm1FaDm3dVtxaHN4t-TbGtYpNOEkHO2lNg8g3g/formResponse";

        public static string GetFormURI()
        {
            return _formURI;
        }

        public Dictionary<string, string> GetFormFields()
        {
            Dictionary<string, string> formFields = new Dictionary<string, string>();
            formFields.Add("entry.1072938702", Notification.ToString());
            return formFields;
        }
    }
}