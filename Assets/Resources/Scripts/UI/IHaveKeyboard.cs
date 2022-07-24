using System.Collections;

namespace Assets.Resources.Scripts.UI
{
    interface IHaveKeyboard
    {
        MyKeyboard KeyboardRef { get; set; }
        string KeyboardText { get; set; }
        void OpenKeyboard();
        void SubmitInput();
        IEnumerator ShowHideKeyboard();
    }
}
