using System;
using JetBrains.Annotations;
using Object = UnityEngine.Object;

namespace Assets.Resources.Scripts.General
{
    public static class Loop
    {
        private static int count;

        public static void PreventEndlessLoop(this Object obj, string message = " no message", int maxLoopCount = 1000)
        {
            count++;

            if (count > maxLoopCount)
            {
                throw new Exception("endless loop " + message);
            }
        }

        public static void FinishLoop([UsedImplicitly]this Object obj)
        {
            count = 0;
        }
    }
}