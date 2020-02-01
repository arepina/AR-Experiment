using System;
using System.Collections.Generic;

namespace Logic
{
    [Serializable]
    public class TrialData
    {
        public uint SubjectNumber;
        public uint Design;
        public uint TrialNumber;
        public float Time; // In seconds
        public uint NotificationsNumber; 
        public uint CorrectAnswers;
        public uint IncorrectAnswers;
        public string Note;

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
            formFields.Add("entry.123982960", CorrectAnswers.ToString());
            formFields.Add("entry.94201860", IncorrectAnswers.ToString());
            if (Note != null) formFields.Add("entry.839337160", Note);
            return formFields;
        }
    }
}