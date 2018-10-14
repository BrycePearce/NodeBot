using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
namespace NodeBot.src.Helpers
{
    public class DataStorage
    {
        private static Dictionary<string, string> pairs = new Dictionary<string, string>();
        public static void AddPairToStorage(string key, string value)
        {
            pairs.Add(key, value);
            SaveData();
        }

        public static int GetPairsCount()
        {
            return pairs.Count;
        }

        public static bool HasKey(string key)
        {
            if (pairs.ContainsKey(key))
            {
                return true;
            }
            else return false;
        }

        public static string GetValueFromKey(string key)
        {
            return pairs[key];
        }
        // constructor
        static DataStorage()
        {
            // Load data. Create file if it doesn't exist.
            if (!ValidateStorageFile("DataStorage.json")) return;
            string json = File.ReadAllText("DataStorage.json");
            pairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public static void SaveData()
        {
            // Save data
            string json = JsonConvert.SerializeObject(pairs);
            File.WriteAllText("DataStorage.json", json);
        }

        private static bool ValidateStorageFile(string file)
        {
            // if file doesn't exist, create it
            if (!File.Exists(file))
            {
                File.WriteAllText(file, "");
                SaveData();
                return false;
            }
            return true;
        }
    }
}
