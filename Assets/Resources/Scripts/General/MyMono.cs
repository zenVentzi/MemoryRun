using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.General
{
    public class MyMono : MonoBehaviour
    {
        public Transform Tr
        {
            get { return transform; }
        }

        public GameObject Go
        {
            get { return transform.gameObject; }
        }

        public Rigidbody2D RigBody
        {
            get { return transform.GetComponent<Rigidbody2D>(); }
        }

        public Collider2D Colider
        {
            get { return transform.GetComponent<Collider2D>(); }
        }

        public Text Txt
        {
            get { return transform.GetComponent<Text>(); }
        }

        public Animation Anim
        {
            get { return transform.GetComponent<Animation>(); }
        }

        public AudioSource Audioo
        {
            get { return transform.GetComponent<AudioSource>(); }
        }

        public SpriteRenderer SpriteRend
        {
            get
            {
                return transform.GetComponent<SpriteRenderer>();
            }
        }
    }
}