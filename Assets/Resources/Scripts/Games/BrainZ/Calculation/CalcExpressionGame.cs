using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.UI;
using Assets.Resources.Scripts.UI.Buttons;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Resources.Scripts.Games.BrainZ.Calculation
{
    public class CalcExpressionGame : BrainGame, IHaveKeyboard
    {
        private const int MaxAnswer = 150,
                          MinAnswer = -150;

        private Text problemText, answerText;
        private int answer,
                    lastType;

        public string KeyboardText
        {
            get { return answerText.text; }
            set
            {
                if (value.Length > 5) return;

                if (value.Length > 0 && value[value.Length - 1] == '-')
                {
                    int currentNum;

                    if (int.TryParse(answerText.text, out currentNum))
                    {
                        currentNum = -currentNum;
                        answerText.text = currentNum.ToString();
                    }
                    else if (answerText.text == "-")
                    {
                        answerText.text = string.Empty;
                    }
                    else
                    {
                        answerText.text = value;
                    }
                }
                else
                {
                    answerText.text = value;
                }
            }
        }

        public MyKeyboard KeyboardRef { get; set; }

        private void GenType1()
        {
            //1.  20 + 3(5 - 1) =  (answer) 

            int first, second, third, fourth;

            do
            {
                first = Random.Range(-40, 40);
                second = Random.Range(2, 5);
                third = Random.Range(2, 10);
                fourth = Random.Range(1, third);
                answer = first + second * (third - fourth);

            } while (answer < MinAnswer || answer > MaxAnswer);

            problemText.text = string.Format("{0} + {1}({2} - {3}) = ", first, second, third, fourth);
        }

        private void GenType2()
        {
            // 3 + 22(1+8) =   (answer)

            int first, second, third, fourth;

            do
            {
                first = Random.Range(-40, 40);
                second = Random.Range(2, 15);
                third = Random.Range(2, 10);
                fourth = Random.Range(2, 10);
                answer = first + second * (third + fourth);

            } while (answer < MinAnswer || answer > MaxAnswer);

            problemText.text = string.Format("{0} + {1}({2} + {3}) = ", first, second, third, fourth);
        }

        private void GenType3()
        {
            int first, second, third, fourth;

            do
            {
                first = Random.Range(-40, 40);
                second = Random.Range(2, 15);
                third = Random.Range(2, 10);
                fourth = Random.Range(2, 10);
                answer = first + second * (third + fourth);

            } while (answer < MinAnswer || answer > MaxAnswer);

            problemText.text = string.Format("{0} + {1}({2} + {3}) = ", first, second, third, fourth);
        }

        private void GenType4()
        {
            //4.  2( 3 + 5 ) - 9 =  (answer)

            int first, second, third, fourth;

            do
            {
                do
                {
                    first = Random.Range(-9, 10);
                } while (first == 0 || first == -1 || first == 1);

                do
                {
                    second = Random.Range(-50, 50);
                } while (second == 0);

                third = Random.Range(2, 50);
                fourth = Random.Range(2, 40);
                answer = first * (second + third) - fourth;

            } while (answer < MinAnswer || answer > MaxAnswer);

            problemText.text = string.Format("{0}({1} + {2}) - {3} = ", first, second, third, fourth);
        }

        private void GenType5()
        {
            //5.  2[ 13 - (1+6)] =  (answer)

            int first, second, third, fourth;

            do
            {
                do
                {
                    first = Random.Range(-9, 10);
                } while (first == 0 || first == -1 || first == 1);

                do
                {
                    second = Random.Range(-50, 50);
                } while (second == 0);

                do
                {
                    third = Random.Range(-50, 50);
                } while (third == 0);

                fourth = Random.Range(2, 50);
                answer = first * (second - (third + fourth));

            } while (answer < MinAnswer || answer > MaxAnswer);

            problemText.text = string.Format("{0}[{1} - ({2} + {3})] = ", first, second, third, fourth);
        }

        private void GenType6()
        {
            //6.  48 / 3 + 5 =   (answer)

            int first, second, third;

            do
            {
                do
                {
                    first = Random.Range(-99, 100);
                    second = Random.Range(3, 90);
                } while ((first > -3 && first < 3) || Mathf.Abs(first) == Mathf.Abs(second) || first % second != 0);

                third = Random.Range(2, 120);
                answer = first / second + third;

            } while (answer < MinAnswer || answer > MaxAnswer);

            problemText.text = string.Format("{0}/{1} + {2} = ", first, second, third);
        }

        private void GenType7()
        {
            //7.  3(6+4)(5 - 3) =  (answer)

            int first, second, third, fourth, fifth;

            do
            {
                do
                {
                    first = Random.Range(-9, 10);
                } while (first == 0 || first == -1 || first == 1);

                do
                {
                    second = Random.Range(-99, 100);
                } while (second == 0);

                third = Random.Range(2, 100);

                do
                {
                    fourth = Random.Range(-99, 100);
                } while (fourth == 0);

                do
                {
                    fifth = Random.Range(-99, 100);
                } while (fifth == fourth);

                answer = first * (second + third) * (fourth - fifth);

            } while (answer < MinAnswer || answer > MaxAnswer);

            problemText.text = string.Format("{0}({1} + {2})({3} - {4}) = ", first, second, third, fourth, fifth);
        }

        private void GenType8()
        {
            //8.  100 - 4(7 - 4)3 =  (answer) 

            int first, second, third, fourth, fifth;

            do
            {
                first = Random.Range(-99, 100);
                second = Random.Range(2, 40);

                do
                {
                    third = Random.Range(-99, 100);
                } while (third == 0);

                do
                {
                    fourth = Random.Range(2, 100);
                } while (fourth == third);

                fifth = Random.Range(2, 40);

                answer = first - second * (third - fourth) * fifth;

            } while (answer < MinAnswer || answer > MaxAnswer);

            problemText.text = string.Format("{0} - {1}({2} - {3}){4} = ", first, second, third, fourth, fifth);
        }

        private void GenType9()
        {
            //9.   (24 - 6)/2 =  (answer)

            int first, second, third;

            do
            {
                do
                {
                    first = Random.Range(-99, 100);
                } while (first == 0);

                do
                {
                    second = Random.Range(2, 100);
                } while (second == first);

                third = Random.Range(2, 10);
                answer = (first - second) / third;

            } while ((first - second) % third != 0 || answer < MinAnswer || answer > MaxAnswer);

            problemText.text = string.Format("({0} - {1}) / {2} = ", first, second, third);
        }

        private bool Correct()
        {
            int enteredNumber;

            if (int.TryParse(answerText.text, out enteredNumber))
            {
                return enteredNumber == answer;
            }

            return false;
        }

        protected override void ValidateCorrect()
        {
            base.ValidateCorrect();
            GenerateNew();
            answerText.text = string.Empty;
        }

        protected override void ValidateIncorrect()
        {
            base.ValidateIncorrect();

            if (GameOver) return;

            GenerateNew();
            answerText.text = string.Empty;
        }

        protected override void Init()
        {
            base.Init();
            StartCoroutine(ShowHideKeyboard());
            problemText = GameObjectManager.GetGoInChildren(Go, "Problem").GetComponent<Text>();
            answerText = GameObjectManager.GetGoInChildren(Go, "Answer").GetComponent<Text>();
        }

        protected override void GenerateNew()
        {
            int type;

            do
            {
                type = Random.Range(1, 10);
            } while (type == lastType);

            lastType = type;

            switch (type)
            {
                case 1:
                    {
                        GenType1();
                        break;
                    }
                case 2:
                    {
                        GenType2();
                        break;
                    }
                case 3:
                    {
                        GenType3();
                        break;
                    }
                case 4:
                    {
                        GenType4();
                        break;
                    }
                case 5:
                    {
                        GenType5();
                        break;
                    }
                case 6:
                    {
                        GenType6();
                        break;
                    }
                case 7:
                    {
                        GenType7();
                        break;
                    }
                case 8:
                    {
                        GenType8();
                        break;
                    }
                case 9:
                    {
                        GenType9();
                        break;
                    }
            }
        }

        public void OpenKeyboard()
        {
            const string path = "Prefabs/UI/NumPad";
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

            answerText.text = string.Empty;
        }

        public IEnumerator ShowHideKeyboard()
        {
            while (!GameOver)
            {
                if (IsGameRunning && KeyboardRef == null)
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
