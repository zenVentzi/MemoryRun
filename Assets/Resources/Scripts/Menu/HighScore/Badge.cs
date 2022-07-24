using UnityEngine;

namespace Assets.Resources.Scripts.Menu.HighScore
{
    public class Badge : MonoBehaviour
    {
        public static Sprite GetGame(string name)
        {
            var path = "Textures/Menu/Main/Score/Badges/Badge" + Proficiency.GetGame(name);
            return UnityEngine.Resources.Load<Sprite>(path);
        }

        public static Sprite GetGameNext(string name)
        {
            var path = "Textures/Menu/Main/Score/Badges/Badge" + Proficiency.GetGameNextLevel(name);
            return UnityEngine.Resources.Load<Sprite>(path);
        }

        public static Sprite GetSection(string name)
        {
            var path = "Textures/Menu/Main/Score/Badges/Badge" + Proficiency.GetSection(name);
            return UnityEngine.Resources.Load<Sprite>(path);
        }
    }
}