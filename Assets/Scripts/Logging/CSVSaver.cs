using System;
using System.IO;
using UnityEngine;

namespace Logic
{
    public class CSVSaver : MonoBehaviour
    {
        private static string path = "logData.csv";

        public static void saveToFile(string logRow)
        {
            string row = logRow + Environment.NewLine;
            if (!File.Exists(path))
            {
                File.WriteAllText(path, row, System.Text.Encoding.UTF8);
            }
            File.AppendAllText(path, row, System.Text.Encoding.UTF8);
        }
    }
}