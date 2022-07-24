using System;
using System.Linq;
using Database = Soomla.KeyValueStorage;

namespace Assets.Resources.Scripts.General
{
    public static class GameStorage
    {
        public const string Separator = "~~~|||~~~";
        public const string AllKeysKey = "allGameStorageKeys";

        public static string GetValue(string key, string def = "")
        {
            string result = Database.GetValue(key);
            if (String.IsNullOrEmpty(result))
                result = def;

            return result;
        }

        public static int GetValue(string key, int def = 0)
        {
            return Convert.ToInt32(GetValue(key, Convert.ToString(def)));
        }

        public static float GetValue(string key, float def = 0f)
        {
            return (float)Convert.ToDouble(GetValue(key, Convert.ToString(def)));
        }

        public static bool GetValue(string key, bool def = false)
        {
            return Convert.ToBoolean(GetValue(key, Convert.ToString(def)));
        }

        public static bool HasKey(string key)
        {
            var val = GetValue(key, null);
            return val != null;
        }

        private static void AddToAllKeys(string key)
        {
            var allKeysValue = GetValue(AllKeysKey, "");

            if (string.IsNullOrEmpty(allKeysValue))
            {
                allKeysValue = key;
            }
            else
            {
                allKeysValue += Separator;
                allKeysValue += key;
            }

            SetValue(AllKeysKey, allKeysValue);
        }

        public static void SetValue(string key, string val)
        {
            if (!HasKey(key) && key != AllKeysKey)
            {
                AddToAllKeys(key);
            }

            Database.SetValue(key, val);
        }

        public static void SetValue(string key, int val)
        {
            SetValue(key, Convert.ToString(val));
        }

        public static void SetValue(string key, float val)
        {
            SetValue(key, Convert.ToString(val));
        }

        public static void SetValue(string key, bool val)
        {
            SetValue(key, Convert.ToString(val));
        }

        public static void DeleteKeyValue(string key)
        {
            Database.DeleteKeyValue(key);
            if(key != AllKeysKey) RemoveFromAllkeys(key);
        }

        private static void RemoveFromAllkeys(string key)
        {
            var allKeysNotSeparated = GetValue(AllKeysKey, "");
            string[] separators = { Separator };

            var allKeysSeparated = allKeysNotSeparated.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            allKeysNotSeparated = string.Empty;

            foreach (string k in allKeysSeparated.Where(k => k != key))
            {
                if(allKeysNotSeparated != string.Empty)
                    allKeysNotSeparated += Separator;

                allKeysNotSeparated += k;
            }

            if (allKeysNotSeparated != string.Empty)
            {
                SetValue(AllKeysKey, allKeysNotSeparated);
            }
            else
            {
                Database.DeleteKeyValue(AllKeysKey);
            }
        }
    }
}
