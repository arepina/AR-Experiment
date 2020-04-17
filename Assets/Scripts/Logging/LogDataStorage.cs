using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Logic
{
    public class LogDataStorage : MonoBehaviour
    {
        [Serializable]
        private struct SerializableWrapper
        {
            public SerializableWrapper(Queue<LogData> allLogsData)
            {
                AllLogsData = allLogsData.ToArray();
            }

            public LogData[] AllLogsData;
        }

        private Queue<LogData> _storedLogData;
        private LogData _currentLogData;

        private const string FILE_NAME = "/AllLogData.json";

        void Awake()
        {
            try
            {
                StreamReader reader = new StreamReader(Application.persistentDataPath + FILE_NAME, System.Text.Encoding.UTF8);
                string json = reader.ReadToEnd();
                if (json.Length > 0)
                {
                    SerializableWrapper data = JsonUtility.FromJson<SerializableWrapper>(json);
                    _storedLogData = new Queue<LogData>(data.AllLogsData);
                }
                else
                    _storedLogData = new Queue<LogData>();

            }
            catch (Exception e)
            {
                Debug.LogException(e);
                _storedLogData = new Queue<LogData>();
            }
        }

        void OnDestroy()
        {
            if (IsThereUnsavedData())
                SaveEverythingToLocalStorage();
        }

        public LogData GetCurrectLogData()
        {
            return _currentLogData;
        }

        public bool IsThereUnsavedData()
        {
            if(_storedLogData != null)
                return _storedLogData.Count > 0;
            return false;
        }

        public void NextLog(string Notification)
        {
            // Fool proffing
            if (_currentLogData != null)
                _storedLogData.Enqueue(_currentLogData);

            _currentLogData = new LogData();
            _currentLogData.Notification = Notification;
        }

        public void SaveLogData()
        {
            if (_currentLogData != null)
            {
                if(_storedLogData == null)
                {
                    _storedLogData = new Queue<LogData>();
                }
                _storedLogData.Enqueue(_currentLogData);
                _currentLogData = null;
            }

            if (IsThereUnsavedData())
                StartCoroutine(TryToSaveToGoogleSheets());
        }
       
        public IEnumerator TryToSaveToGoogleSheets()
        {
            LogData earliestData = _storedLogData.Peek();

            using (UnityWebRequest www = UnityWebRequest.Post(LogData.GetFormURI(), earliestData.GetFormFields()))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.LogError(www.error);
                    SaveEverythingToLocalStorage();
                }
                else
                {
                    // Yepp, we will do this one by one
                    _storedLogData.Dequeue();
                    if (IsThereUnsavedData())
                        StartCoroutine(TryToSaveToGoogleSheets());
                    else
                        ClearLocalStorage();
                }
            }
        }

        private void SaveEverythingToLocalStorage()
        {
            try
            {
                // We store all the data to a disk but do not clear the _storedLogData. The letter is quite important
                StreamWriter writer = new StreamWriter(Application.persistentDataPath + FILE_NAME, false, System.Text.Encoding.UTF8);
                writer.Write(JsonUtility.ToJson(new SerializableWrapper(_storedLogData)));
                writer.Close();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private void ClearLocalStorage()
        {
            try
            {
                StreamWriter writer = new StreamWriter(Application.persistentDataPath + FILE_NAME, false, System.Text.Encoding.UTF8);
                writer.Close();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}