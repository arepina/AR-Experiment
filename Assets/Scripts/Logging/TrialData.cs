using System;
using System.Collections.Generic;

namespace Logic
{
    [Serializable]
    public class TrialData
    {
        public uint SubjectNumber;
        public string Design;
        public uint TrialNumber;
        public float Time; // In seconds
        public uint NotificationsNumber; 
        public uint NumberOfHaveToActNotifications;
        public uint NumberOfNonIgnoredHaveToActNotifications;
        public float SumOfReactionTimeToNonIgnoredHaveToActNotifications;
        public uint NumberOfInCorrectlyActedNotifications;

        // Instructions were taken from here: https://youtu.be/z9b5aRfrz7M
        private static readonly string _formURI = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSeMgJ_QKYVj_roNo8w9kyQv465foOt-ePB5z_4rV-srn6TxwA/formResponse";

        public static string GetFormURI()
        {
            return _formURI;
        }

        public Dictionary<string, string> GetFormFields()
        {
            Dictionary<string, string> formFields = new Dictionary<string, string>();
            formFields.Add("entry.2005620554", SubjectNumber.ToString());
            formFields.Add("entry.1630268306", Design.ToString());
            formFields.Add("entry.1276049454", TrialNumber.ToString());
            formFields.Add("entry.388397209", Time.ToString());
            formFields.Add("entry.1289494810", NotificationsNumber.ToString());
            formFields.Add("entry.123982960", NumberOfHaveToActNotifications.ToString());
            formFields.Add("entry.1215856783", NumberOfNonIgnoredHaveToActNotifications.ToString());
            formFields.Add("entry.1734028322", SumOfReactionTimeToNonIgnoredHaveToActNotifications.ToString());
            formFields.Add("entry.588295555", NumberOfInCorrectlyActedNotifications.ToString());
            return formFields;
        }
    }
}