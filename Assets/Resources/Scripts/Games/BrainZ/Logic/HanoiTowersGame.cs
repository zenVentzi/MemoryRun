using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.Menu;
using Assets.Resources.Scripts.Menu.RunTimeMenu;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Games.BrainZ.Logic
{
    public class HanoiTowersGame : SizeDependentGame
    {
        #region variables

        private GameObject caughtDisk;

        private List<GameObject> disks;
        private List<List<GameObject>> disksOnTower;
        private Vector2 diskTakeOffPos;

        private GameObject firstTower;

        private GameObject secondTower,
            thirdTower;


        private readonly Vector2 bottomFirstTowerPos = new Vector2(-259.4f, -186),
                                 bottomSecondTowerPos = new Vector2(0, -186),
                                 bottomThirdTowerPos = new Vector2(261, -186);

        private bool HasGameFinished
        {
            get
            {
                var finished = disksOnTower[2].Count == Size && HasGameStarted;

                if (finished && !GameOver)
                    GameOverMenu.Load();

                return finished;
            }
        }

        private bool MoreLessButtonsClicked
        {
            get
            {
                return ((MoreClicked && Size < 6) ||
                        (LessClicked && Size > 3)) &&
                       !HasGameStarted;
            }
        }

        protected override int MaxSize
        {
            get { return 6; }
        }

        #endregion

        [UsedImplicitly]
        protected void Update()
        {
            if (HasGameFinished || PauseButton.Instance.GamePaused)
                return;

            HandleMovement();

            if (MoreLessButtonsClicked)
            {
                ModifyNumOfDisks();
            }
        }

        protected override void OnSizeIncrease()
        {
            ModifyNumOfDisks();
        }

        protected override void OnSizeDecrease()
        {
            ModifyNumOfDisks();
        }

        protected override void Init()
        {
            base.Init();

            firstTower = GameObjectManager.GetGoInChildren(Go, "FirstTower");
            secondTower = GameObjectManager.GetGoInChildren(Go, "SecondTower");
            thirdTower = GameObjectManager.GetGoInChildren(Go, "ThirdTower");

            #region initializing disks

            disks = new List<GameObject>();
            disksOnTower = new List<List<GameObject>>();

            ModifyNumOfDisks();
            StartCoroutine(ActivateCollidersOnIntroFadeout());

            #endregion
        }

        private IEnumerator ActivateCollidersOnIntroFadeout()
        {
            while (!GameIntro.HasIntroBeenShown)
            {
                yield return null;
            }

            CollidersManager.EnableGameColliders();
        }

        private void ModifyNumOfDisks()
        {
            disks.Clear();
            disksOnTower.Clear();

            for (int i = 0; i < 3; i++)
            {
                disksOnTower.Add(new List<GameObject>());//adding towers
            }

            for (int i = (6 - Size) + 1; i <= 6; i++)
            {
                disks.Add(transform.FindChild(i.ToString()).gameObject);

                if (i == (6 - Size) + 1)
                {
                    disks.Last().SetActive(true); //if it was deactivated
                    disks.Last().transform.localPosition = bottomFirstTowerPos;
                }
                else
                {
                    PutOnTopOf(disks.Last(), disks[disks.Count - 2], bottomFirstTowerPos);
                }

                disksOnTower[0].Add(disks.Last());
            }

            for (int i = 0; i < (6 - Size); i++)
            {
                transform.FindChild((i + 1).ToString()).gameObject.SetActive(false);
            }
        }

        private void HandleMovement()
        {
            HandleDisk();

            if (caughtDisk == null) return;

            var newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            caughtDisk.transform.position = new Vector3(newPos.x, newPos.y, caughtDisk.transform.position.z);
        }

        private void HandleDisk()
        {
            if (InputManager.IsTouchHeld() && caughtDisk == null)
            {
                CatchDisk();
            }
            else if (caughtDisk != null && !InputManager.IsTouchHeld()) //when release touch
            {
                caughtDisk.GetComponent<Collider2D>().enabled = true;
                PutOnPlace();
                caughtDisk.GetComponent<SpriteRenderer>().sortingOrder = 2;
                caughtDisk = null;

                Canvas.transform.FindChild("Moves").GetComponent<Text>().text =
                    Moves.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void CatchDisk()
        {
            if (InputManager.IsTouchHeldFromBeginningOver(firstTower))
            {
                if (!HasGameStarted)
                {
                    HasGameStarted = true;
                    DisableModifyButtons();
                }

                if (disksOnTower[0].Count <= 0) return;

                caughtDisk = disksOnTower[0].Last();
                disksOnTower[0].Remove(caughtDisk);
                diskTakeOffPos = caughtDisk.transform.localPosition;
                caughtDisk.GetComponent<Collider2D>().enabled = false;
                caughtDisk.GetComponent<SpriteRenderer>().sortingOrder = 3;
            }
            else if (InputManager.IsTouchHeldFromBeginningOver(secondTower))
            {
                if (disksOnTower[1].Count <= 0) return;

                caughtDisk = disksOnTower[1].Last();
                disksOnTower[1].Remove(caughtDisk);
                diskTakeOffPos = caughtDisk.transform.localPosition;
                caughtDisk.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
            else if (InputManager.IsTouchHeldFromBeginningOver(thirdTower))
            {
                if (disksOnTower[2].Count <= 0) return;

                caughtDisk = disksOnTower[2].Last();
                disksOnTower[2].Remove(caughtDisk);
                diskTakeOffPos = caughtDisk.transform.localPosition;
                caughtDisk.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
        }

        private void PutOnPlace()
        {
            if (firstTower.GetComponent<Collider2D>().OverlapPoint(caughtDisk.transform.position) && PossiblePutOnTower(disksOnTower[0]))
            {
                PutOnTopOf(caughtDisk, disksOnTower[0].LastOrDefault(), bottomFirstTowerPos);
                disksOnTower[0].Add(caughtDisk);

                if (!IsSameTower(firstTower))
                {
                    Moves++;
                }
            }
            else if (secondTower.GetComponent<Collider2D>().OverlapPoint(caughtDisk.transform.position) &&
                     PossiblePutOnTower(disksOnTower[1]))
            {
                PutOnTopOf(caughtDisk, disksOnTower[1].LastOrDefault(), bottomSecondTowerPos);
                disksOnTower[1].Add(caughtDisk);

                if (!IsSameTower(secondTower))
                {
                    Moves++;
                }
            }
            else if (thirdTower.GetComponent<Collider2D>().OverlapPoint(caughtDisk.transform.position) &&
                     PossiblePutOnTower(disksOnTower[2]))
            {
                PutOnTopOf(caughtDisk, disksOnTower[2].LastOrDefault(), bottomThirdTowerPos);
                disksOnTower[2].Add(caughtDisk);

                if (!IsSameTower(thirdTower))
                {
                    Moves++;
                }
            }
            else
            {
                SetBackToTakeOffTower();
            }
        }

        private bool IsSameTower(GameObject tower)
        {
            return tower.GetComponent<Collider2D>().OverlapPoint(diskTakeOffPos);
        }

        private void SetBackToTakeOffTower()
        {
            if (firstTower.GetComponent<Collider2D>().OverlapPoint(diskTakeOffPos))
            {
                disksOnTower[0].Add(caughtDisk);
            }
            else if (secondTower.GetComponent<Collider2D>().OverlapPoint(diskTakeOffPos))
            {
                disksOnTower[1].Add(caughtDisk);
            }
            else if (thirdTower.GetComponent<Collider2D>().OverlapPoint(diskTakeOffPos))
            {
                disksOnTower[2].Add(caughtDisk);
            }
            caughtDisk.transform.localPosition = diskTakeOffPos;
        }

        private void PutOnTopOf(GameObject upper, GameObject down, Vector2 bottomPos)
        {
            if (down == null)
            {
                upper.transform.localPosition = bottomPos;
            }
            else
            {
                var y = down.transform.localPosition.y + down.GetComponent<SpriteRenderer>().bounds.extents.y;
                upper.transform.localPosition = new Vector2(bottomPos.x, y);
            }
        }

        private bool PossiblePutOnTower(ICollection<GameObject> towerDsks)
        {
            return towerDsks.Count == 0 || IsUpperDiskSmaller(towerDsks.Last());
        }

        private bool IsUpperDiskSmaller(GameObject below)
        {
            return below.GetComponent<Collider2D>().bounds.size.x > caughtDisk.GetComponent<Collider2D>().bounds.size.x;
        }
    }
}