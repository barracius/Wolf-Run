using System;
using Helpers;
using UnityEngine;

namespace MainMenuScripts
{
    public class SkinPanelScript : MonoBehaviour
    {
        public Transform[] wolfieSkins;
        public Transform[] backgroundSkins;
        private int _currentWolfieSelectedSkin;
        private int _currentBackgroundSelectedSkin;

        private void SelectCurrentSkin(SkinType skinType)
        {
            switch (skinType)
            {
                case SkinType.Wolfie:
                    _currentWolfieSelectedSkin = PlayerPrefs.GetInt("Wolf Body Skin", 0);
                    wolfieSkins[_currentWolfieSelectedSkin].GetComponent<SkinButtonScript>().SelectingUiChange(true);
                    break;
                case SkinType.Background:
                    _currentBackgroundSelectedSkin = PlayerPrefs.GetInt("MainMenu Background Skin", 0);
                    backgroundSkins[_currentBackgroundSelectedSkin].GetComponent<SkinButtonScript>().SelectingUiChange(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(skinType), skinType, null);
            }
            
        }

        internal void UnselectOtherSkins(int lastSelected, SkinType skinType)
        {
            
            switch (skinType)
            {
                case SkinType.Wolfie:
                    for (var i = 0; i < wolfieSkins.Length; i++)
                    {
                        if (i == lastSelected) continue;
                        wolfieSkins[i].GetComponent<SkinButtonScript>().SelectingUiChange(false);
                    }
                    break;
                case SkinType.Background:
                    for (var i = 0; i < backgroundSkins.Length; i++)
                    {
                        if (i == lastSelected) continue;
                        backgroundSkins[i].GetComponent<SkinButtonScript>().SelectingUiChange(false);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(skinType), skinType, null);
            }
        }

        internal void SelectCurrentSkins()
        {
            SelectCurrentSkin(SkinType.Wolfie);
            SelectCurrentSkin(SkinType.Background);
        }
    }
}
