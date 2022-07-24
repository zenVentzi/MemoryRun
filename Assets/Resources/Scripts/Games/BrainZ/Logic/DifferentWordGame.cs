using System.Collections.Generic;
using System.Linq;
using Assets.Resources.Scripts.UI;
using Assets.Resources.Scripts.UI.Buttons;
using UnityEngine;

namespace Assets.Resources.Scripts.Games.BrainZ.Logic
{
    public class DifferentWordGame : BrainGame
    {
        #region variables

        private string correctWord;
        private GameButton[] buttons;
        private static List<int> usedProblems;

        #region words

        private readonly KeyValuePair<List<string>, string>[] answers =
        {
            new KeyValuePair<List<string>, string>(new List<string>(3) {"inch", "ounce", "yard"}, "ounce"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"steering wheel", "engine", "car"}, "car"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"guitar", "flute", "violin"}, "flute"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"avoid", "flee", "duck"}, "duck"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"branch", "dirt", "root"}, "dirt"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"heading", "body", "letter"}, "letter"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"leopard", "cougar", "elephant"}, "elephant"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"cornea", "retina", "vision"}, "vision"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"book", "index", "glossary"}, "book"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"noun", "preposition", "punctuation"}, "punctuation"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"peninsula", "island", "bay"}, "bay"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"unique", "beautiful", "rare"}, "beautiful"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"car", "bus", "airplane"}, "airplane"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"rabbit", "snail", "dolphin"}, "snail"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"dolphin", "shark", "catfish"}, "dolphin"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"stork", "penguin", "swallow"}, "penguin"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"eyes", "nose", "arm"}, "arm"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"flower", "branch", "tree"}, "branch"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"keyboard", "mouse", "computer"}, "computer"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"guitar", "violin", "piano"}, "piano"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"printer", "mouse", "keyboard"}, "printer"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"tomato", "peach", "cucumber"}, "peach"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"peninsula", "island", "bay"}, "bay"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"black", "orange", "white"}, "orange"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"cherry", "strawberry", "banana"}, "banana"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"cherry", "strawberry", "watermelon"}, "cherry"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"up", "down", "left"}, "left"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"flat", "house", "beach"}, "beach"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"diary", "computer", "screen"}, "diary"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"rabbit", "pit", "goat"}, "pit"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"snake", "tiger", "cow"}, "cow"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"strawberry", "raspberry", "peach"}, "peach"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"bag", "tie", "pants"}, "bag"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"Tom", "Sally", "Ruth"}, "Tom"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"soccer", "wrestling", "baseball"}, "wrestling"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"Rome", "Peru", "Germany"}, "Rome"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"bread", "tea", "milk"}, "bread"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"airplane", "bird", "flower"}, "flower"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"man", "teacher", "nurse"}, "man"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"pineapple", "lettuce", "egg plant"}, "pineapple"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"pants", "t-shirt", "jeans"}, "t-shirt"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"lemon", "orange", "peach"}, "peach"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"peach", "kiwi", "apricot"}, "kiwi"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"notebook", "magazine", "book"}, "notebook"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"Google", "Bing", "Skype"}, "Skype"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"frog", "chicken", "snake"}, "chicken"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"veil", "hat", "helmet"}, "veil"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"kiwi", "emu", "eagle"}, "eagle"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"butter", "oil", "cheese"}, "oil"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"chalk", "rubber", "tea"}, "chalk"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"hangar", "dock", "park"}, "park"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"swan", "parrot", "sparrow"}, "swan"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"thin", "sharp", "small"}, "sharp"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"orange", "apple", "guava"}, "orange"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"hammer", "knife", "sword"}, "hammer"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"investor", "director", "financier"}, "director"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"tricycle", "trilogy", "trifle"}, "trifle"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"locust", "crocodile", "chameleon"}, "locust"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"calendar", "day", "year"}, "calendar"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"cinnamon", "pepper", "groundnut"}, "groundnut"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"biscuits", "chocolate", "cake"}, "chocolate"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"wash", "scrub", "stain"}, "stain"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"coat", "trousers", "shirt"}, "trousers"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"bee", "ant", "spider"}, "spider"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"reader", "writer", "printer"}, "reader"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"axe", "arrow", "knife"}, "arrow"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"horse", "goat", "fox"}, "fox"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"cat", "fox", "dog"}, "fox"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"house", "school", "palace"}, "school"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"geography", "botany", "zoology"}, "geography"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"physics", "geography", "chemistry"}, "geography"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"hockey", "volleyball", "chess"}, "chess"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"volleyball", "hockey", "football"}, "hockey"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"tree", "leaf", "flower"}, "tree"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"kill", "kidnap", "murder"}, "kidnap"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"cot", "sheet", "pillow"}, "cot"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"Italy", "Spain", "Korea"}, "Korea"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"giraffe", "hyena", "deer"}, "hyena"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"zebra", "deer", "wolf"}, "wolf"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"helicopter", "cycle", "car"}, "cycle"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"friend", "sister", "mother"}, "friend"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"pencil", "book", "sharpener"}, "book"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"brick", "bridge", "spade"}, "brick"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"carrot", "tomato", "cucumber"}, "carrot"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"lion", "seal", "scorpion"}, "seal"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"whale", "snake", "crow"}, "whale"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"actor", "musician", "poet"}, "poet"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"paper", "poster", "sketch"}, "paper"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"mew", "bark", "shout"}, "shout"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"blue", "white", "yellow"}, "white"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"think", "run", "swim"}, "think"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"swallow", "lick", "taste"}, "taste"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"Sun", "Universe", "Star"}, "Universe"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"watermelon", "guava", "papaya"}, "watermelon"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"papaya", "cabbage", "cucumber"}, "papaya"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"engineer", "mechanic", "architect"}, "mechanic"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"cow", "donkey", "goat"}, "donkey"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"instruct", "teach", "educate"}, "instruct"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"rectangle", "cube", "square"}, "cube"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"paper", "wood", "stone"}, "stone"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"bridge", "brook", "canal"}, "bridge"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"house", "roof", "wall"}, "house"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"hat", "gloves", "cap"}, "gloves"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"transparent", "foggy", "cloudy"}, "transparent"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"bake", "boil", "freeze"}, "freeze"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"funeral", "festival", "party"}, "funeral"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"walk", "hear", "run"}, "hear"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"tower", "valley", "river"}, ""),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"pineapple", "orange", "banana"}, "banana"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"x-ray", "radio", "television"}, "x-ray"),
            new KeyValuePair<List<string>, string>(new List<string>(3) {"Nile", "Niagara", "Amazon"}, "Niagara"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"computer", "laptop", "keyboard"}, "keyboard"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"eye", "leg", "ear"}, "leg"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"paper", "metal", "plastic"}, "plastic"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"bag", "shirt", "hat"}, "shirt"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"fox", "cat", "dog"}, "fox"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"calculate", "draw", "sing"}, "calculate"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"hamster", "cow", "cat"}, "cow"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"fish", "kangaroo", "lion"}, "fish"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"kangaroo", "lizard", "iguana"}, "kangaroo"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"motherboard", "mouse", "operating system"}, "operating system"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"radio", "telephone", "tv"}, "telephone"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"facebook", "google+", "youtube"}, "youtube"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"hammer", "gear", "bolt"}, "hammer"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"street", "park", "city"}, "city"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"honey", "nuts", "oil"}, "oil"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"city mall", "city hall", "court"}, "city mall"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"whale", "crocodile", "lizard"}, "whale"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"neighbourhood", "neighbour", "city block"}, "neighbour"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"village", "tribe", "house"}, "house"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"castle", "town", "fort"}, "town"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"tank", "submarine", "soldier"}, "soldier"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"general", "minister", "president"}, "general"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"landfill", "bin", "trash"}, "landfill"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"book", "laptop", "magazine"}, "laptop"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"ear", "eye", "nose"}, "nose"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"ear", "arm", "eye"}, "arm"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"arm", "leg", "nose"}, "nose"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"arm", "toe", "finger"}, "arm"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"toe", "leg", "finger"}, "leg"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"hat", "bag", "backpack"}, "hat"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"cocktail", "fresh", "juice"}, "cocktail"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"vegetarian", "doctor", "religious"}, "doctor"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"nurse", "doctor", "sailor"}, "sailor"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"hairdresser", "vegetarian", "seller"}, "vegetarian"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"repair", "break", "smash"}, "repair"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"bright", "dark", "shiny"}, "dark"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"conclude", "start", "initiate"}, "conclude"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"arrive", "come", "depart"}, "depart"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"pleasant", "worthy", "fear"}, "fear"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"talk", "scream", "cry"}, "talk"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"cover", "show", "mask"}, "show"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"idea", "concept", "product"}, "product"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"adore", "hate", "admire"}, "hate"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"write", "mark", "label"}, "write"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"move", "go", "come"}, "go"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"put", "place", "take"}, "take"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"give", "take", "gift"}, "take"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"play", "stop", "pause"}, "play"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"husky", "pitbull", "dog"}, "dog"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"dog", "dackel", "cat"}, "dackel"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"rabbit", "hamster", "fish"}, "fish"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"sport", "volleyball", "tennis"}, "sport"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"basketball", "volleyball", "running"}, "running"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"running", "jumping", "sleeping"}, "sleeping"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"run", "swim", "jump"}, "swim"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"write", "type", "draw"}, "draw"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"couple", "friends", "family"}, "family"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"cousin", "brother", "sister"}, "cousin"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"sky", "clouds", "rainbow"}, "sky"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"gmail", "yahoo", "skype"}, "skype"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"abobe illustrator", "corel draw", "photoshop"}, "photoshop"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"mouse", "printer", "monitor"}, "mouse"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"keyboard", "printer", "mouse"}, "printer"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"white", "blue", "yellow"}, "white"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"Oracle", "Blizzard", "Gameloft"}, "Oracle"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"Toughwin", "Dream Works", "Blizzard"}, "Dream Works"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"Google", "Halfbruck", "Toughwin"}, "Google"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"Toughwin", "Ubisoft", "Facebook"}, "Facebook"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"Microsoft", "Apple", "Toughwin"}, "Toughwin"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"busy", "free", "engaged"}, "free"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"hotel", "flat", "house"}, "hotel"),
			new KeyValuePair<List<string>, string>(new List<string>(3) {"lesson", "course", "school"}, "school"),
        };

        #endregion

        #endregion

        #region methods

        protected override void Init()
        {
            base.Init();

            if(usedProblems == null || usedProblems.Count == answers.Length)
                usedProblems = new List<int>();

            buttons = Tr.GetComponentsInChildren<GameButton>();
        }

        protected virtual bool IsCorrect()
        {
            return ClickedBtn.GetText() == correctWord;
        }

        protected override void OnGameButtonClick(GameButton clickedButton)
        {
            base.OnGameButtonClick(clickedButton);

            if (IsCorrect())
            {
                ValidateCorrect();
            }
            else
            {
                ValidateIncorrect();
            }

            if(!GameOver)
                GenerateNew();
        }

        protected override void GenerateNew()
        {
            int randomProblem;

            do
            {
                randomProblem = Random.Range(0, answers.Length);
            } while (usedProblems.Contains(randomProblem));

            usedProblems.Add(randomProblem);

            if (usedProblems.Count == answers.Count())
                usedProblems.Clear();

            WriteOverButtons(randomProblem);
            correctWord = answers[randomProblem].Value;
        }

        private void WriteOverButtons(int randomProblem)
        {
            var usedIndexers = new List<int>();

            foreach (var button in buttons)
            {
                int wordIndexer;

                do
                {
                    wordIndexer = Random.Range(0, 3);
                } while (usedIndexers.Contains(wordIndexer));

                usedIndexers.Add(wordIndexer);
                button.SetText(answers[randomProblem].Key[wordIndexer]);
            }
        }

        #endregion
    }
}