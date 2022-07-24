using System.Collections;
using Assets.Resources.Scripts.General;
using UnityEngine;

namespace Assets.Resources.Scripts
{
    public class TestScript : MyMono
    {
        private float updateInterval = 0.5f;
 
        private float accum = 0.0f;
        private int frames = 0;
        private float timeleft;

        private void Start()
        {
            timeleft = updateInterval;
        }

        private void Update()
        {
            timeleft -= Time.deltaTime;
            accum += Time.timeScale / Time.deltaTime;
            ++frames;

            if (timeleft <= 0.0)
            {
                Txt.text = "" + (accum / frames).ToString("f2");
                timeleft = updateInterval;
                accum = 0.0f;
                frames = 0;
            }
        }
    }
}
