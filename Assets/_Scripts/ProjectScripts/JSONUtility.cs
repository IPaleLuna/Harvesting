using System.IO;
using UnityEngine;

namespace PaleLuna.JSONUtility
{
    public static class JSONUtility
    {
        public static void WriteToFile<T>(string path, T objectToWrite)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(objectToWrite);
            File.WriteAllText(path, json);
        }

        public static bool TryReadFromFile<T>(string path, out T objectToRead)
        {
            bool fileExist = File.Exists(path);
            
            if (fileExist)
            {
                string json = File.ReadAllText(path);
                objectToRead = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                objectToRead = default(T);
            }
            
            return fileExist;
        }
        
    }
}

