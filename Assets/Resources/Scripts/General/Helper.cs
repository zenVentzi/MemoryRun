using System;
using System.Reflection;
using Object = UnityEngine.Object;

namespace Assets.Resources.Scripts
{
    public static class Helper
    {
        public static void ClearConsole(this Object obj)
        {
            // This simply does "LogEntries.Clear()" the long way:
            Type logEntries = Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
            MethodInfo clearMethod = logEntries.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);
            clearMethod.Invoke(null, null);
        }

        public static string GetFormattedTime(int time, bool hasHours = false)
        {
            if (!hasHours)
            {
                var mins = time / 60;
                var secs = time % 60;

                var formattedTime = mins < 10 ?
                    string.Format(secs < 10 ? "0{0}:0{1}" : "0{0}:{1}", mins, secs) :
                    string.Format(secs < 10 ? "{0}:0{1}" : "{0}:{1}", mins, secs);

                return formattedTime;
            }
            else
            {
                var hours = time / 3600;
                var mins = (time / 60) % 60;
                var secs = time % 60;

                var hoursStr = "0" + hours;
                var minsStr = string.Format(mins < 10 ? "0{0}" : "{0}", mins);
                var secsStr = string.Format(secs < 10 ? "0{0}" : "{0}", secs);

                return string.Format("{0}:{1}:{2}", hoursStr, minsStr, secsStr);
            }
        }
    }
}