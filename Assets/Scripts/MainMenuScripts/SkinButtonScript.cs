using System;
using Helpers;
using UnityEngine;

namespace MainMenuScripts
{
    public class SkinButtonScript : MonoBehaviour
    {
        internal Transform SelectionUi;
        internal Transform LockedUi;
        internal Transform SkinOutlineUi;
        public bool isLocked;
        public int skinNumber;
        public SkinType skinType;

        private void Start()
        {
            SelectionUi = transform.Find("SelectionUI");
            LockedUi = transform.Find("LockedUI");
            SkinOutlineUi = transform.Find("SkinOutlineUI");
            SelectionUi.gameObject.SetActive(false);

            if (CheckLock())
            {
                isLocked = true;
            }
            else
            {
                isLocked = false;
                LockedUi.gameObject.SetActive(false);
            }
        }

        private bool CheckLock()
        {
            if (skinNumber == 0) return false;
            int lockedValue;
            switch (skinType)
            {
                case SkinType.Wolfie:
                    lockedValue = PlayerPrefs.GetInt("WolfieSkin" + skinNumber + "LockedStatus", 0);
                    break;
                case SkinType.Background:
                    lockedValue = PlayerPrefs.GetInt("BackgroundSkin" + skinNumber + "LockedStatus", 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (lockedValue)
            {
                case 0:
                    return true;
                case 1:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SelectingUiChange(bool selecting)
        {
            if (selecting)
            {
                SkinOutlineUi.gameObject.SetActive(false);
                SelectionUi.gameObject.SetActive(true);
            }
            else
            {
                SelectionUi.gameObject.SetActive(false);
                SkinOutlineUi.gameObject.SetActive(true);
            }
        }
    }
}
