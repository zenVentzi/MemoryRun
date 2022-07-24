using System;

namespace Assets.Resources.Scripts.General
{
    public static class GamePlayerPrefs
    {
        public static string GetString(string key, string def = "")
        {
            return GameStorage.GetValue(key, def);
        }

        public static DateTime GetTime(string key, DateTime def = default(DateTime))
        {
            if (GameStorage.GetValue(key + "Year", -1) == -1)
                return def;

            var year = GameStorage.GetValue(key + "Year", -1);
            var month = GameStorage.GetValue(key + "Month", -1);
            var day = GameStorage.GetValue(key + "Day", -1);
            var hour = GameStorage.GetValue(key + "Hour", -1);
            var minute = GameStorage.GetValue(key + "Minute", -1);
            var second = GameStorage.GetValue(key + "Second", -1);

            return new DateTime(year, month, day, hour, minute, second);
        }

        public static int GetInt(string key, int def = 0)
        {
            return GameStorage.GetValue(key, def);
        }

        public static float GetFloat(string key, float def = 0f)
        {
            return GameStorage.GetValue(key, def);
        }

        public static bool GetBool(string key, bool def = false)
        {
            return GameStorage.GetValue(key, def);
        }

        public static bool HasKey(string key)
        {
            return GameStorage.HasKey(key);
        }

        public static void SetString(string key, string val)
        {
            GameStorage.SetValue(key, val);
        }

        public static void SetTime(string key, DateTime val)
        {
            GameStorage.SetValue(key + "Year", val.Year);
            GameStorage.SetValue(key + "Month", val.Month);
            GameStorage.SetValue(key + "Day", val.Day);
            GameStorage.SetValue(key + "Hour", val.Hour);
            GameStorage.SetValue(key + "Minute", val.Minute);
            GameStorage.SetValue(key + "Second", val.Second);
        }

        public static void SetInt(string key, int val)
        {
            GameStorage.SetValue(key, val);
        }

        public static void SetFloat(string key, float val)
        {
            GameStorage.SetValue(key, val);
        }

        public static void SetBool(string key, bool val)
        {
            GameStorage.SetValue(key, val);
        }

        private static void DeleteTime(string key)
        {
            GameStorage.DeleteKeyValue(key + "Year");
            GameStorage.DeleteKeyValue(key + "Month");
            GameStorage.DeleteKeyValue(key + "Day");
            GameStorage.DeleteKeyValue(key + "Hour");
            GameStorage.DeleteKeyValue(key + "Minute");
            GameStorage.DeleteKeyValue(key + "Second");
        }

        public static void DeleteKey(string key)
        {
            var isTime = GetTime(key) != default(DateTime);

            if (isTime)
            {
                DeleteTime(key);
            }
            else
            {
                GameStorage.DeleteKeyValue(key);                
            }
        }

        public static void DeleteAll()
        {
            var allKeysNotSeparated = GetString(GameStorage.AllKeysKey);
            string[] separators = { GameStorage.Separator };

            var allKeysSeparated = allKeysNotSeparated.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (var key in allKeysSeparated)
            {
                DeleteKey(key);
            }

            DeleteKey(GameStorage.AllKeysKey);
        }
    }
}
