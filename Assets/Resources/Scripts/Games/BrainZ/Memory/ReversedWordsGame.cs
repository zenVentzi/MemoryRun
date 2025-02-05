﻿using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.UI;
using Assets.Resources.Scripts.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Resources.Scripts.Games.BrainZ.Memory
{
    public class ReversedWordsGame : BrainGame, IHaveKeyboard
    {
        private const int MinNumberOfChars = 5,
                          MaxNumberOfChars = 30;

        private Text displayText;

        private string correctText,
                       prevDisplayText;
        private int anchorLength;
        private bool textDisplayed;
        #region words
        private readonly List<string> allWords = new List<string>
        {
		    "a",
			"be",
			"in",
			"we",
			"do",
			"to",
			"my",
			"he",
			"it",
			"hi",
			"me",
			"row",
			"air",
            "why",
			"any",
			"bar",
			"bed",
			"bat",
			"add",
			"and",
			"art",
			"eye",
			"box",
			"buy",
			"cry",
			"bio",
			"bug",
			"bus",
			"can",
			"bet",
			"boy",
			"ask",
			"bin",
			"day",
			"far",
			"fat",
			"dog",
			"cat",
			"dot",
			"elf",
			"get",
			"gun",
			"gym",
			"ink",
			"how",
			"him",
			"her",
			"hat",
			"ice",
			"jam",
			"kid",
			"leg",
			"man",
			"mad",
			"son",
			"key",
			"oil",
			"now",
			"own",
			"shy",
			"tea",
			"shy",
			"six",
			"two",
			"tie",
			"ten",
			"tan",
			"spa",
			"win",
			"wet",
			"yet",
			"yes",
			"use",
			"zoo",
			"bad",
			"jazz",
			"belt",
			"bird",
			"bill",
			"bark",
			"beer",
			"best",
			"bass",
			"bite",
			"blog",
			"blur",
			"blue",
			"bend",
			"bike",
			"calm",
			"busy",
			"chat",
			"chin",
			"camp",
			"cell",
			"care",
			"come",
			"cook",
			"cyan",
			"coma",
			"doll",
			"diet",
			"diva",
			"dirt",
			"dice",
			"down",
			"drink",
			"dust",
			"easy",
			"else",
			"even",
			"ever",
			"fire",
			"folk",
			"free",
			"firm",
			"file",
			"five",
			"game",
			"gold",
			"give",
			"golf",
			"girl",
			"grow",
			"icon",
			"home",
			"iron",
			"hook",
			"long",
			"inch",
			"kilo",
			"keep",
			"kick",
			"kiss",
            "boss",
			"lost",
			"lord",
			"luck",
			"loud",
			"loop",
			"logo",
			"many",
			"maze",
			"make",
			"math",
			"meow",
			"meal",
			"meat",
			"menu",
			"male",
			"mail",
			"nerd",
			"neck",
			"nail",
			"neon",
			"must",
			"none",
			"noon",
			"next",
			"need",
			"mute",
			"oval",
			"oxen",
			"okay",
			"open",
			"oven",
			"pawn",
			"part",
			"paid",
			"rest",
			"pass",
			"pear",
			"pain",
			"part",
			"pure",
            "roof",
			"race",
			"rent",
			"sale",
			"self",
			"seem",
			"seed",
			"seat",
			"save",
			"soap",
			"spam",
			"soon",
			"moon",
			"song",
			"sing",
			"star",
			"stay",
			"soul",
			"some",
			"sofa",
			"cool",
			"pool",
			"stop",
			"sore",
			"sock",
			"surf",
			"talk",
			"swim",
			"swag",
			"tell",
			"than",
			"thin",
			"text",
			"team",
			"tall",
			"tape",
			"then",
			"they",
			"tent",
			"taxi",
			"feet",
			"trim",
			"turn",
			"tuna",
			"twin",
			"task",
			"ugly",
			"tour",
			"tree",
			"undo",
			"upon",
			"void",
			"visa",
			"vote",
			"vein",
			"rick",
			"word",
			"wall",
			"verb",
			"mine",
			"your",
			"yard",
			"wrap",
			"hero",
			"wink",
			"beef",
			"wife",
			"rock",
			"idea",
			"year",
			"worm",
			"wolf",
			"yoga",
			"zoom",
			"cute",
			"good",
			"land",
			"drop",
			"head",
			"green",
			"blame",
			"blade",
			"block",
			"blow",
			"bossy",
			"blitz",
			"boost",
			"boots",
			"bored",
			"bench",
			"beach",
			"bingo",
			"front",
			"level",
			"brave",
			"canal",
			"bunch",
			"cabin",
			"chips",
			"break",
			"alert",
			"again",
			"brain",
			"blood",
			"allow",
			"fruit",
			"brown",
			"build",
			"baker",
			"bacon",
			"essay",
			"blend",
			"brush",
			"bonus",
			"calve",
			"chair",
			"child",
			"cheap",
			"cobra",
			"curly",
			"delta",
			"digit",
			"delay",
			"derby",
			"craft",
			"crisp",
			"event",
			"grape",
			"draft",
			"empty",
			"elite",
			"funny",
			"floor",
			"flyer",
			"frame",
			"grill",
			"honey",
			"house",
			"ideal",
			"pizza",
			"jelly",
			"combo",
			"novel",
			"quick",
			"junky",
			"crazy",
			"plaza",
			"joker",
			"jokes",
			"hokey",
			"anime",
			"aloha",
			"media",
			"panda",
			"basic",
			"brick",
			"candy",
			"chalk",
			"cloth",
			"coral",
			"crime",
			"daddy",
			"daily",
			"denim",
			"delta",
			"diner",
			"donut",
			"early",
			"drums",
			"earth",
			"enjoy",
			"fifty",
			"sixty",
			"seven",
			"eight",
			"fixed",
			"fizzy",
			"front",
			"groom",
			"habit",
			"heigh",
			"hater",
			"lover",
			"hobby",
			"koala",
			"lemur",
			"lilac",
			"liver",
			"mango",
			"magic",
			"maker",
			"metro",
			"mixer",
			"music",
			"never",
			"ocean",
			"often",
			"photo",
			"racer",
			"radar",
			"runner",
			"robot",
			"scary",
			"snake",
			"shake",
			"start",
			"tasty",
			"dance",
			"title",
			"towel",
			"tower",
			"total",
			"witch",
			"while",
            "smart",
			"worth",
			"wings",
			"squat",
			"access",
			"adverb",
			"almond",
			"aspect",
			"banker",
			"blonde",
			"beacon",
			"botany",
			"busted",
			"cherry",
			"chosen",
			"chaise",
			"clumsy",
			"crispy",
			"curved",
			"cousin",
			"boxing",
			"direct",
			"double",
			"donkey",
			"family",
			"format",
			"fridge",
			"flower",
			"forest",
			"fourth",
			"friend",
			"garlic",
            "golden",
			"guitar",
            "hacker",
			"hinted",
			"honest",
			"icebox",
			"impact",
			"import",
			"injury",
			"invest",
			"island",
			"jargon",
			"keypad",
			"lizard",
			"magnet",
			"minute",
			"modify",
			"nougat",
			"oxygen",
			"packet",
			"parody",
			"paused",
			"pencil",
			"petrol",
			"police",
			"poster",
			"safely",
			"sailor",
			"salmon",
			"sample",
			"scored",
			"shaker",
			"shrift",
			"simply",
            "skater",
			"slider",
			"sketch",
			"social",
			"softly",
			"square",
			"spread",
			"sponge",
			"turkey",
			"tundra",
			"vector",
			"wealth",
			"winner",
			"winter",
			"summer",
			"zodiac",
			"finger",
			"ability",
			"account",
			"acrobat",
			"already",
			"android",
			"amateur",
			"animals",
			"awkward",
			"awesome",
			"anybody",
			"attract",
			"attempt",
            "average",
			"avocado",
            "babysit",
			"balance",
			"bananas",
			"bedroom",
			"believe",
			"battery",
			"between",
			"billion",
			"biscuit",
			"collect",
			"college",
			"cookies",
			"creator",
			"cupcake",
			"current",
			"culture",
			"cowgirl",
			"country",
			"cottage",
			"costume",
			"couples",
			"cracker",
			"catfish",
			"capital",
			"correct",
			"circuit",
			"chicken",
			"classic",
			"clothes",
			"destiny",
			"divorce",
			"divided",
            "destroy",
			"digital",
			"develop",
			"diamond",
			"channel",
			"careful",
			"cartoon",
			"cheetah",
			"chapter",
			"century",
			"coconut",
			"daycare",
			"daytime",
			"decoder",
			"default",
			"dentist",
			"despite",
			"desktop",
			"dynamic",
			"dreamer",
			"dolphin",
			"drivers",
			"eclipse",
			"economy",
			"educate",
			"elastic",
			"elegant",
            "element",
			"english",
            "exactly",
			"example",
			"explore",
			"extreme",
			"eyelash",
			"fantasy",
			"freedom",
			"finally",
			"general",
			"freezer",
			"giraffe",
			"goodbye",
			"gorilla",
			"grammar",
			"grandma",
			"graphic",
			"habitat",
			"gameboy",
			"grocery",
			"haircut",
			"germany",
			"holiday",
			"husband",
			"iceberg",
			"kitchen",
			"january",
			"kittens",
			"licence",
			"machine",
			"members",
			"monitor",
			"monster",
            "muffins",
			"morning",
			"medical",
			"married",
			"mermaid",
			"message",
			"million",
			"mission",
			"mistake",
			"musical",
			"napkin",
			"natural",
			"network",
			"neutral",
			"nothing",
			"numeral",
			"octopus",
			"october",
			"organic",
			"outside",
			"pacific",
			"passion",
			"payment",
			"plastic",
			"popcorn",
			"printer",
			"pumpkin",
            "partner",
			"quarter",
            "raccoon",
			"sundown",
			"rainbow",
			"routine",
			"pinball",
			"serious",
			"secret",
			"shooter",
			"sandman",
			"support",
			"snowman",
			"sixteen",
			"stamina",
			"skypark",
			"subtext",
			"storage",
			"success",
			"tornado",
			"talents",
			"teenage",
			"tonight",
			"tourist",
			"tuesday",
			"twister",
			"unready",
			"upstair",
			"vehicle",
			"version",
			"vibrate",
			"village",
			"visitor",
			"vanilla",
            "waffles",
			"walnuts",
			"warrior",
			"weather",
			"website",
			"windows",
			"without",
			"academic",
			"absolute",
			"asteroid",
			"behaviour",
			"blockage",
			"category",
			"children",
			"computer",
			"complain",
			"domestic",
			"dominate",
			"favourite",
			"fragment",
			"goldfish",
			"hydrogen",
			"humorist",
			"humanist",
			"improved",
			"industry",
			"informal",
            "isometry",
			"keyboard",
            "knitwear",
			"laughter",
			"lifeboat",
			"magnetic",
			"literacy",
			"neighbor",
			"mythical",
			"obstacle",
			"organize",
			"personal",
			"phonetic",
			"powerful",
			"postmark",
			"portable",
			"question",
			"randomly",
			"reaction",
			"reading",
			"relaxing",
			"romantic",
			"sandwich",
			"shortage",
			"shoulder",
			"shooting",
			"software",
			"speaking",
			"vocalist",
			"yourself",
			"thousand",
			"algorithm",
			"biography",
			"binocular",
            "boyfriend",
			"breathing",
			"chemistry",
			"certainly",
			"clipboard",
			"configure",
			"craftsmen",
			"dangerous",
			"declaring",
			"discovery",
			"dragonfly",
			"drinkable",
			"education",
			"fisherman",
			"fruitcake",
			"glutamine",
			"hairstyle",
			"lifeguard",
			"logarithm",
			"marketing",
			"mechanist",
            "microwave",
			"nailbrush",
            "nightclub",
			"nightmare",
			"numerical",
			"overnight",
			"paintwork",
			"playhouse",
			"producing",
			"publisher",
			"quickstep",
			"redacting",
			"republish",
			"replaying",
			"screaming",
			"secondary",
			"shipboard",
			"simulated",
			"snowflake",
			"treading",
			"unblocked",
			"voluntary",
			"wonderful",
			"workable",
			"wrestling",
			"workplace",
            "worldwide"
        };
        #endregion
        public MyKeyboard KeyboardRef { get; set; }

        public string KeyboardText
        {
            get { return displayText.text; }
            set
            {
                if(value.Length > 100) return;
                displayText.text = value;
            }
        }

        private bool Correct()
        {
            return (displayText.text == correctText);
        }

        private string ReverseString(string str)
        {
            var arr = str.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        private IEnumerator DisplayText(string txt)
        {
            if (KeyboardRef != null) KeyboardRef.HideKeyboard();
            textDisplayed = false;
            yield return new WaitForSeconds(1);

            var reversed = Random.Range(0, 2) == 1;
            displayText.text = reversed ? ReverseString(txt) : txt;
            correctText = ReverseString(displayText.text);
            var displayTime = txt.Length * 0.21f;

            if (displayTime < .5f)
            {
                displayTime = .5f;
            }
            else if (displayTime > 6)
            {
                displayTime = 6;
            }

            yield return new WaitForSeconds(displayTime);

            OpenKeyboard();
            textDisplayed = true;
            displayText.text = string.Empty;
        }

        protected override void Init()
        {
            base.Init();
            anchorLength = MinNumberOfChars;
            StartCoroutine(ShowHideKeyboard());
            displayText = GameObjectManager.GetGoInChildren(Go, "Text").GetComponent<Text>();
        }

        protected override void ValidateCorrect()
        {
            base.ValidateCorrect();

            if (anchorLength < MaxNumberOfChars)
                anchorLength++;

            prevDisplayText = "-1";
            GenerateNew();
        }

        protected override void ValidateIncorrect()
        {
            base.ValidateIncorrect();

            if(!GameOver)
                GenerateNew();
        }

        public void OpenKeyboard()
        {
            const string path = "Prefabs/UI/Qwerty";
            var keyboard = Instantiate(UnityEngine.Resources.Load<GameObject>(path));
            KeyboardRef = keyboard.GetComponentInChildren<MyKeyboard>();
            keyboard.transform.parent = Tr;
        }

        public void SubmitInput()
        {
            if (Correct())
            {
                ValidateCorrect();
            }
            else
            {
                ValidateIncorrect();
            }

            displayText.text = string.Empty;
        }

        protected override void GenerateNew()
        {
            var usedWords = new List<string>();
            var tempText = string.Empty;

            do
            {
                string current;

                do
                {
                    current = allWords[Random.Range(0, allWords.Count)];
                } while (usedWords.Contains(current));

                usedWords.Add(current);
                tempText += current + " ";

                if (tempText.Length <= anchorLength + 3) continue;
                usedWords.Clear();
                tempText = string.Empty;
            } while (tempText.Length < anchorLength || tempText.Length > anchorLength + 3 || tempText.Trim() == prevDisplayText);

            prevDisplayText = tempText.Trim();
            StartCoroutine(DisplayText(tempText.Trim()));
        }

        public IEnumerator ShowHideKeyboard()
        {
            while (!GameOver)
            {
                if (IsGameRunning && KeyboardRef == null && textDisplayed)
                {
                    OpenKeyboard();
                }
                else if (!IsGameRunning && KeyboardRef != null)
                {
                    KeyboardRef.HideKeyboard();
                }

                yield return null;
            }

            if (KeyboardRef != null) KeyboardRef.HideKeyboard();
        }
    }
}
