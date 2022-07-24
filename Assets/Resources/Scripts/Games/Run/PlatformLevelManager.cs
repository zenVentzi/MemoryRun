using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.Menu.RunTimeMenu;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Resources.Scripts.Games.Run
{
    public class PlatformLevelManager : MyMono
    {
        private readonly Vector3 startPosition = new Vector3(540, 0, 0);

        public GameObject[] Levels;
        private bool isInTheEndOfLevels;
        private List<GameObject> usedLevels;

        [UsedImplicitly]
        private void Start()
        {
            usedLevels = new List<GameObject>();
            StartCoroutine(RemoveEmpty());
            StartCoroutine(GenerateLevel(true));

            if (RunGame.IsPreview)
                StartCoroutine(CheckIfLevelsFinished());
        }

        private IEnumerator CheckIfLevelsFinished()
        {
            while (!Game.GameInstance.GameOver)
            {
                if (HasLastLevelReachedEnd())
                {
                    GameOverMenu.Load();
                    yield break;
                }

                yield return null;
            }
        }

        private IEnumerator RemoveEmpty()
        {
            while (!Game.GameInstance.GameOver)
            {
                if (usedLevels.Count > 0 && usedLevels.Last().transform.childCount <= 0)
                {
                    Destroy(usedLevels.Last());
                    usedLevels.RemoveAt(usedLevels.Count - 1);
                }

                yield return null;
            }
        }

        private bool HasLastLevelReachedEnd()
        {
            bool lastElementAtTheEnd =
                (GetLastElementXPos(usedLevels.Last()) +
                 GetLastElementLength(usedLevels.Last()) / 2) <= GamePlayerPrefs.GetFloat("CameraWidth") / 2;

            return isInTheEndOfLevels && lastElementAtTheEnd;
        }

        private IEnumerator GenerateLevel(bool isFirstLevel)
        {
            while (!Game.GameInstance.GameOver)
            {
                if (isFirstLevel || IsTimeToGenerate())
                {
                    try
                    {
                        var newLevel = Instantiate(Levels[GetRandomLevelIndex()]);
                        usedLevels.Add(newLevel);
                    }
                    catch (Exception)
                    {
                        yield break;
                    }

                    AssignLevelSettings(isFirstLevel);
                }

                if (isFirstLevel)
                {
                    break;
                }

                yield return null;
            }

            if (isFirstLevel)
            {
                StartCoroutine(GenerateLevel(false));
            }
        }

        private void AssignLevelSettings(bool isFirstLevel)
        {
            usedLevels.Last().transform.parent = Tr;
            usedLevels.Last().transform.localScale = Vector3.one;
            usedLevels.Last().transform.position = GetNewPosition(isFirstLevel);
            usedLevels.Last().name = usedLevels.Last().name.Substring(0, usedLevels.Last().name.LastIndexOf("(Clone)"));
        }

        private int GetRandomLevelIndex()
        {
            if (usedLevels.Count == Levels.Length)
            {
                if (RunGame.IsPreview)
                {
                    isInTheEndOfLevels = true;
                    return -1;
                }

                GameObject lastLevel = usedLevels.Last();
                usedLevels.Clear();
                usedLevels.Add(lastLevel);
            }

            int index;

            do
            {
                index = Random.Range(0, Levels.Length);
            } while (ManagerContainsLevel(Levels[index]));

            return index;
        }

        private bool ManagerContainsLevel(GameObject level)
        {
            return usedLevels.Any(go => go.name == level.name);
        }

        private float GetLastElementXPos(GameObject level)
        {
            return level.transform.GetChild(level.transform.childCount - 1).position.x;
        }

        private Vector3 GetNewPosition(bool isFirst)
        {
            return (isFirst ? startPosition : CalculatePosition());
        }

        private float GetElementLength(Transform platform)
        {
            return platform.GetComponent<Collider2D>().bounds.size.x;
        }

        private float GetLastElementLength(GameObject level)
        {
            return GetElementLength(level.transform.GetChild(level.transform.childCount - 1));
        }

        //private float GetFirstElementLength(GameObject level)
        //{
        //    return GetElementLength(level.transform.GetChild(0));
        //}

        private Vector3 CalculatePosition()
        {
            GameObject previousLevel = usedLevels.ElementAt(usedLevels.Count - 2);
            var pos = new Vector3
            {
                x = GetLastElementXPos(previousLevel) +
                    GetLastElementLength(previousLevel) / 2
            };

            return pos;
        }

        private bool IsTimeToGenerate()
        {
            return GetLastElementXPos(usedLevels.Last()) <= GamePlayerPrefs.GetFloat("CameraWidth");
        }
    }
}