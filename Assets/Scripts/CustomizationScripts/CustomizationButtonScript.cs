using System;
using Helpers;
using UnityEngine;

namespace CustomizationScripts
{
    public class CustomizationButtonScript : MonoBehaviour
    {
         private Transform _selectionUi = null;
        private Transform _lockedUi = null;
        private Transform _skinOutlineUi = null;
        public bool isLocked = false;
        public int skinNumber = 0;
        public SkinType skinType;

        private void Start()
        {
            AssignUis();
            DetermineIfLocked();

        }

        private void AssignUis()
        {
            _selectionUi = transform.Find("SelectionUI");
            _lockedUi = transform.Find("LockedUI");
            _skinOutlineUi = transform.Find("SkinOutlineUI");
        }

        private void DetermineIfLocked()
        {
            if (CheckLock())
            {
                isLocked = true;
                _lockedUi.gameObject.SetActive(true);
            }
            else
            {
                isLocked = false;
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
            AssignUis();
            if (selecting)
            {
                _skinOutlineUi.gameObject.SetActive(false);
                _selectionUi.gameObject.SetActive(true);
            }
            else
            {
                _selectionUi.gameObject.SetActive(false);
                _skinOutlineUi.gameObject.SetActive(true);
            }
        }
        
        
    }
}
