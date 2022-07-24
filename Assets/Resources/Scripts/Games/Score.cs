using Assets.Resources.Scripts.General;
using JetBrains.Annotations;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Games
{
    public class Score : MyMono
    {
        private float val;
        private Text txt;

        protected float Val
        {
            get
            {
                return val;
            }
            set
            {
                val = value;
                Display();
            }
        }

        [UsedImplicitly]
        protected virtual void Start()
        {
            txt = GetComponent<Text>();
        }

        public float Get()
        {
            return Val;
        }

        public void Add(float score)
        {
            Val += score;
        }

        public void Display()
        {
            txt.text = ((int)val).ToString();
        }
    }
}
