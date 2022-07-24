using System;
using Assets.Resources.Scripts.General;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.UI
{
    public class MyKeyboard : MyMono
    {
        [SerializeField] private string showAnimName,
                                        hideAnimName;
        [UsedImplicitly]
        private void OnEnable()
        {
            Anim.Play(showAnimName);
        }

        public void HideKeyboard()
        {
            Anim.Play(hideAnimName);
        }

        [UsedImplicitly]
        private void OnAppear()
        {
            foreach (Transform tr in Tr)
            {
                tr.GetComponent<Collider2D>().enabled = true;
            }
        }

        [UsedImplicitly]
        private void OnDisappear()
        {
            Destroy(Go);
        }
    }
}
