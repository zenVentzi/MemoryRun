using System.Collections.Generic;
using System.Linq;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Games.BrainZ.Logic
{
    public class WordOrderGame : BrainGame
    {
        #region variables

        #region questions
        private readonly KeyValuePair<string, List<string>>[] questAnswers =
        {
            new KeyValuePair<string, List<string>>("Parts of a word ?", new List<string> {"SY", "LLA", "BLE"}),
            new KeyValuePair<string, List<string>>(" The biggest planet in our solar system?", new List<string> {"JU", "PI", "TER"}),
            new KeyValuePair<string, List<string>>("The river with the biggest water lillies?",
                new List<string> {"A", "MA", "ZON"}),
            new KeyValuePair<string, List<string>>("An art gallery, the entrance of which is a glass pyramid?",
                new List<string> {"LOU", "VRE"}),
            new KeyValuePair<string, List<string>>("The biggest part of the human brain?",
                new List<string> {"CE", "RE", "BRUM"}),
            new KeyValuePair<string, List<string>>("Another name for your voice box is?",
                new List<string> {"LAR", "YNX"}),
            new KeyValuePair<string, List<string>>("The 7th element on the Mendeleev's periodic table?",
                new List<string> {"NI", "TRO", "GEN"}),
            new KeyValuePair<string, List<string>>("The name of the element with the chemical symbol ‘He’?",
                new List<string> {"HE", "LI", "UM"}),
            new KeyValuePair<string, List<string>>("The fear of which animal is known as ‘arachnophobia’?",
                new List<string> {"SPI", "DER"}),
            new KeyValuePair<string, List<string>>("Adidas and Volkswagen are companies from ?",
                new List<string> {"GER", "MA", "NY"}),
            new KeyValuePair<string, List<string>>("The distance from the center of a circle to its edge? ",
                new List<string> {"RA", "DI", "US"}),
            new KeyValuePair<string, List<string>>("The outermost layer of skin on the human body is?",
                new List<string> {"EP", "I", "DER", "MIS"}),
            new KeyValuePair<string, List<string>>("What substance are nails made of?",
                new List<string> {"KE", "RA", "TIN"}),
            new KeyValuePair<string, List<string>>("What is the sweet substance made by bees?",
                new List<string> {"HON", "EY"}),
            new KeyValuePair<string, List<string>>("What is the most popular food used during Halloween?",
                new List<string> {"PUMP", "KIN"}),
            new KeyValuePair<string, List<string>>("Fruit preserves made from citrus fruits, sugar and water?",
                new List<string> {"MAR", "MA", "LADE"}),
            new KeyValuePair<string, List<string>>("The scientific study of plant life is known as?",
                new List<string> {"BOT", "A", "NY"}),
            new KeyValuePair<string, List<string>>("A person who studies biology is known as a?",
                new List<string> {"BI", "OL", "O", "GIST"}),
            new KeyValuePair<string, List<string>>("The chemical element uranium was named after which planet?",
                new List<string> {"U", "RA", "NUS"}),
            new KeyValuePair<string, List<string>>("The farthest planet from the Sun in the Solar System?",
                new List<string> {"NEP", "TUNE"}),
            new KeyValuePair<string, List<string>>("The Galilean moons orbit which planet?",
                new List<string> {"NEP", "TUNE"}),
            new KeyValuePair<string, List<string>>("What food makes up all of a Giant Panda’s diet?",
                new List<string> {"BAM", "BOO"}),
            new KeyValuePair<string, List<string>>("In which continent bees are not found?",
                new List<string> {"ANT", "ARC", "TI", "CA"}),
            new KeyValuePair<string, List<string>>("Largest primate in the world?", new List<string> {"GO", "RIL", "LA"}),
            new KeyValuePair<string, List<string>>("The first element on the periodic table?",
                new List<string> {"HY", "DRO", "GEN"}),
            new KeyValuePair<string, List<string>>("What is the centre of an atom called?",
                new List<string> {"NU", "CLE", "US"}),
            new KeyValuePair<string, List<string>>("The capital city of Australia?", new List<string> {"CAN", "BER", "RA"}),
            new KeyValuePair<string, List<string>>("The capital city of Kenya?", new List<string> {"NAI", "RO", "BI"}),
            new KeyValuePair<string, List<string>>("The capital city of Venezuela?", new List<string> {"CA", "RA", "CAS"}),
            new KeyValuePair<string, List<string>>("The capital city of Canada?", new List<string> {"OT", "TA", "WA"}),
            new KeyValuePair<string, List<string>>("The capital city of United States? ",
                new List<string> {"WASH", "ING", "TON"}),
            new KeyValuePair<string, List<string>>("What state of the USA is the Grand Canyon located in?",
                new List<string> {"AR", "I", "ZO", "NA"}),
            new KeyValuePair<string, List<string>>("When gas changes into liquid it is called?",
                new List<string> {"CON", "DEN", "SA", "TION"}),
            new KeyValuePair<string, List<string>>("The nearest galaxy to our own?",
                new List<string> {"AN", "DRO", "ME", "DA"}),
            new KeyValuePair<string, List<string>>("From what can you make a battery?",
                new List<string> {"PO", "TA", "TO"}),
            new KeyValuePair<string, List<string>>("Where is the Great Victoria Desert located in?",
                new List<string> {"AUS", "TRAL", "IA"}),
            new KeyValuePair<string, List<string>>("When iron rusts, its weight...?",
                new List<string> {"IN", "CREAS", "ES"}),
            new KeyValuePair<string, List<string>>(
                "Servers are computers that provide resources to other computers connected to a",
                new List<string> {"NET", "WORK"}),
            new KeyValuePair<string, List<string>>("An input device?", new List<string> {"SCAN", "NER"}),
            new KeyValuePair<string, List<string>>(" The set of instructions that tells the computer what to do is",
                new List<string> {"SOFT", "WARE"}),
            new KeyValuePair<string, List<string>>(
                "During processing and cooking which part of food is mostly destroyed?",
                new List<string> {"VI", "TA", "MINS"}),
            new KeyValuePair<string, List<string>>("What is the Moon?", new List<string> {"SAT", "EL", "LITE"}),
            new KeyValuePair<string, List<string>>("Which country leads in the production of rubber?",
                new List<string> {"MA", "LAY", "SIA"}),
            new KeyValuePair<string, List<string>>("'DB' computer abbreviation usually means?",
                new List<string> {"DA", "TA", "BASE"}),
            new KeyValuePair<string, List<string>>("Fast growing tree?", new List<string> {"EU", "CA", "LYP", "TUS"}),
			new KeyValuePair<string, List<string>>("The capital city of Turkey", new List<string> {"AN", "KA", "RA"}),
			new KeyValuePair<string, List<string>>("The capital city of Romania", new List<string> {"BU", "CHA", "REST"}),
			new KeyValuePair<string, List<string>>("The capital city of Slovakia", new List<string> {"BRA", "TI", "SLA", "VA"}),
			new KeyValuePair<string, List<string>>("The capital city of Egypt", new List<string> {"CAI", "RO"}),
			new KeyValuePair<string, List<string>>("The capital city of Venezuela", new List<string> {"CA", "RA", "CAS"}),
			new KeyValuePair<string, List<string>>("The capital city of Denmark", new List<string> {"CO", "PEN", "HA", "GEN"}),
			new KeyValuePair<string, List<string>>("The capital city of Tanzania", new List<string> {"DO", "DO", "MA"}),
			new KeyValuePair<string, List<string>>("The capital city of Ireland", new List<string> {"DUB", "LIN"}),
			new KeyValuePair<string, List<string>>("The capital city of Botswana", new List<string> {"GA", "BO", "RO", "NE"}),
			new KeyValuePair<string, List<string>>("The capital city of Cuba", new List<string> {"HA", "VAN", "A"}),
			new KeyValuePair<string, List<string>>("The capital city of Israel", new List<string> {"JE", "RU", "SA", "LEM"}),
			new KeyValuePair<string, List<string>>("The capital city of Ukraine", new List<string> {"KI", "EV"}),
			new KeyValuePair<string, List<string>>("The capital city of Peru", new List<string> {"LI", "MA"}),
			new KeyValuePair<string, List<string>>("The capital city of Spain", new List<string> {"MA", "DRID"}),
			new KeyValuePair<string, List<string>>("The capital city of Monaco", new List<string> {"MON", "A", "CO"}),
			new KeyValuePair<string, List<string>>("The capital city of Russia", new List<string> {"MOS", "COW"}),
			new KeyValuePair<string, List<string>>("The capital city of Latvia", new List<string> {"RI", "GA"}),
			new KeyValuePair<string, List<string>>("The capital city of Singapore", new List<string> {"SIN", "GA", "PORE"}),
			new KeyValuePair<string, List<string>>("The capital city of Macedonia", new List<string> {"SKOP", "JE"}),
			new KeyValuePair<string, List<string>>("The capital city of Sweden", new List<string> {"STOCK", "HOLM"}),
			new KeyValuePair<string, List<string>>("The capital city of Japan", new List<string> {"TO", "KY", "O"}),
			new KeyValuePair<string, List<string>>("The capital city of Austria", new List<string> {"VI", "EN", "NA"}),
			new KeyValuePair<string, List<string>>("The capital city of Armenia", new List<string> {"YE", "RE", "VAN"}),
			new KeyValuePair<string, List<string>>("The capital city of Jamaica", new List<string> {"KINGS", "TON"}),
			new KeyValuePair<string, List<string>>("The largest primate in the world?", new List<string> {"GO", "RIL", "LA"}),
			new KeyValuePair<string, List<string>>("The wire inside an electric bulb is known as? ", new List<string> {"FIL", "A", "MENT"}),
			new KeyValuePair<string, List<string>>("What does Si stand for in Mendeleev's table?", new List<string> {"SIL", "I", "CON"}),
			new KeyValuePair<string, List<string>>("What does Mg stand for in Mendeleev's table?", new List<string> {"MAG", "NE", "SI", "UM"}),
			new KeyValuePair<string, List<string>>("What does Ni stand for in Mendeleev's table?", new List<string> {"NICK", "EL"}),
			new KeyValuePair<string, List<string>>("What does Br stand for in Mendeleev's table?", new List<string> {"BRO", "MINE"}),
			new KeyValuePair<string, List<string>>("What does Ca stand for in Mendeleev's table?", new List<string> {"CAL", "CI", "UM"}),
			new KeyValuePair<string, List<string>>("What does Hg stand for in Mendeleev's table?", new List<string> {"MER", "CU", "RY"}),
			new KeyValuePair<string, List<string>>("What does Co stand for in Mendeleev's table?", new List<string> {"CO", "BALT"}),
			new KeyValuePair<string, List<string>>("What does Ag stand for in Mendeleev's table?", new List<string> {"SIL", "VER"}),
			new KeyValuePair<string, List<string>>("What does Ba stand for in Mendeleev's table?", new List<string> {"BAR", "I", "UM"}),
			new KeyValuePair<string, List<string>>("With which device are earthquakes recorded?", new List<string> {"SEIS", "MO", "GRAPH"}),
			new KeyValuePair<string, List<string>>("Which US state capital ends in 'x'?", new List<string> {"PHOE", "NIX"}),
			new KeyValuePair<string, List<string>>("Which instrument did Louis Armstrong play?", new List<string> {"TRUM", "PET"}),
			new KeyValuePair<string, List<string>>("What animal is the national symbol of Scotland?", new List<string> {"U", "NI", "CORN"}),
			new KeyValuePair<string, List<string>>("Troodon is considered the most intelligent what?", new List<string> {"DI", "NO", "SAUR"}),
			new KeyValuePair<string, List<string>>("What is the fastest animal on two legs?", new List<string> {"OS", "TRICH"}),
			new KeyValuePair<string, List<string>>("What is the most intelligent subhuman primate?  ", new List<string> {"CHIM", "PAN", "ZEE"}),
			new KeyValuePair<string, List<string>>("What type of creature is a mandrill?", new List<string> {"MON", "KEY"}),
			new KeyValuePair<string, List<string>>("From which country does the Korat cat come?", new List<string> {"THAI", "LAND"})
        };

        #endregion

        private GameButton[] buttons;
        private int currentSyllableIndex;
        private List<string> currentSyllables;
        private static List<int> usedQuestIndexes;
        private Text question;

        #endregion

        #region methods

        protected override void Init()
        {
            base.Init();
            buttons = Go.GetComponentsInChildren<GameButton>();

            if(usedQuestIndexes == null || usedQuestIndexes.Count == questAnswers.Length)
                usedQuestIndexes = new List<int>();

            currentSyllables = new List<string>();
            question = GameObjectManager.GetGoInChildren(Go, "Question").GetComponent<Text>();
        }

        private void GenerateNewSyllables(int questIndex)
        {
            ResetSyllables(questIndex);
            var usedSyllables = new List<string>();

            do
            {
                var usedSyllableIndexes = new List<int>();
                usedSyllables.Clear();

                for (int i = 0; i < 4; i++)
                {
                    if (usedSyllableIndexes.Count < questAnswers[questIndex].Value.Count)
                    {
                        int syllableIndex;

                        do
                        {
                            syllableIndex = Random.Range(0, questAnswers[questIndex].Value.Count);
                        } while (usedSyllableIndexes.Contains(syllableIndex));

                        usedSyllableIndexes.Add(syllableIndex);
                        buttons[i].Go.SetActive(true);
                        buttons[i].SetText(questAnswers[questIndex].Value[syllableIndex]);
                        usedSyllables.Add(questAnswers[questIndex].Value[syllableIndex]);
                    }
                    else
                    {
                        buttons[i].Go.SetActive(false);
                    }
                }
            } while (SyllablesOrdered(questIndex, usedSyllables));
        }

        private void ResetSyllables(int questIndex)
        {
            currentSyllables.Clear();
            currentSyllables.AddRange(questAnswers[questIndex].Value);
            currentSyllableIndex = 0;
        }

        private bool SyllablesOrdered(int questIndex, IEnumerable<string> usedSyllables)
        {
            return !usedSyllables.Where((t, i) => t != questAnswers[questIndex].Value[i]).Any();
        }

        private int GetQuestIndex()
        {
            if (usedQuestIndexes.Count == questAnswers.Length)
            {
                usedQuestIndexes.Clear();
            }

            int questIndex;

            do
            {
                questIndex = Random.Range(0, questAnswers.Length);
            } while (usedQuestIndexes.Contains(questIndex));

            usedQuestIndexes.Add(questIndex);

            return questIndex;
        }

        //private void LockButtons(bool loock)
        //{
        //    foreach (var button in buttons)
        //    {
        //        if (loock)
        //        {
        //            button.Lock();
        //        }
        //        else
        //        {
        //            button.Unlock();
        //        }
        //    }
        //}

        private void ResetButtons()
        {
            foreach (var button in buttons)
            {
                button.Reset();
            }
        }

        protected virtual bool IsCorrect()
        {
            return ClickedBtn.GetText() == currentSyllables[currentSyllableIndex] &&
                   currentSyllableIndex == currentSyllables.Count - 1;
        }

        protected virtual bool IsIncorrect()
        {
            return ClickedBtn.GetText() != currentSyllables[currentSyllableIndex];
        }

        protected override void ValidateCorrect()
        {
            base.ValidateCorrect();
            ResetButtons();
            GenerateNew();
        }

        protected override void ValidateIncorrect()
        {
            base.ValidateIncorrect();

            if (GameOver) return;
            ResetButtons();
            GenerateNew();
        }

        protected override void OnGameButtonClick(GameButton clickedButton)
        {
            base.OnGameButtonClick(clickedButton);

            if (IsCorrect())
            {
                ValidateCorrect();
                return;
            }
            if (IsIncorrect())
            {
                ValidateIncorrect();
                return;
            }

            currentSyllableIndex++;
            ClickedBtn.Lock(false);
        }

        protected override void GenerateNew()
        {
            int questIndex = GetQuestIndex();
            question.text = questAnswers[questIndex].Key;
            GenerateNewSyllables(questIndex);
        }
        #endregion
    }
}