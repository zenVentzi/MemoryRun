using UnityEngine;

namespace Assets.Resources.Scripts.Games.Timings
{
    public class TimeElapse : AbstractTime
    {
        protected override void UpdateTiming()
        {
            TimeProp += Time.deltaTime;
        }
    }
}