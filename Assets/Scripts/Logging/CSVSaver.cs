using System;
using System.IO;
using UnityEngine;

namespace Logic
{
    public class CSVSaver : MonoBehaviour
    {
        private static string path = "logData.csv";
        private static string iosPath = Application.persistentDataPath + "logData.csv";

        public static void saveToFile(string logRow)
        {
            string row = logRow + Environment.NewLine;
            #if UNITY_EDITOR
            try
            {
                if (!File.Exists(path))
                {
                    File.WriteAllText(path, row, System.Text.Encoding.UTF8);
                }
                File.AppendAllText(path, row, System.Text.Encoding.UTF8);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            #else
            try
            {
                if (!File.Exists(iosPath))
                {
                    File.WriteAllText(iosPath, row, System.Text.Encoding.UTF8);
                }
                File.AppendAllText(iosPath, row, System.Text.Encoding.UTF8);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            #endif
        }
    }
}