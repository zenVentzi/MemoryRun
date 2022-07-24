using Assets.Resources.Scripts.Games;
using JetBrains.Annotations;

namespace Assets.Resources.Scripts.UI.Buttons
{
    public class KeyboardButton : AnimatedButton
    {
        private IHaveKeyboard gameWithKeyboard;
        private bool IsActionKey
        {
            get { return name == "ShootKey" || name == "DeleteKey"; }
        }

        [UsedImplicitly]
        private void Awake()
        {
            Colider.enabled = false;
            gameWithKeyboard = Game.GameInstance.Go.GetComponent<Game>() as IHaveKeyboard;
        }

        protected override void OnnMouseDown()
        {
            base.OnnMouseDown();

            if (!IsActionKey)
            {
                gameWithKeyboard.KeyboardText += name != "SpaceBar" ? name : " ";
            }
            else
            {
                if (name == "ShootKey")
                {
                    gameWithKeyboard.SubmitInput();
                }
                else
                {
                    if (gameWithKeyboard.KeyboardText.Length > 0)
                        gameWithKeyboard.KeyboardText =
                            gameWithKeyboard.KeyboardText.Remove(gameWithKeyboard.KeyboardText.Length - 1);
                }
            }
        }
    }
}
