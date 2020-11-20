using System;
using UnityEngine;

namespace MainMenuScripts
{
    public class SkinPanelScript : MonoBehaviour
    {
        public Transform[] wolfieSkins;
        public Transform[] backgroundSkins;
        private int _currentWolfieSelectedSkin;
        private int _currentBackgroundSelectedSkin;

        internal void SelectCurrentSkin(string thing)
        {
            switch (thing)
            {
                case "wolfie":
                    _currentWolfieSelectedSkin = PlayerPrefs.GetInt("Wolf Body Skin", 0);
                    wolfieSkins[_currentWolfieSelectedSkin].Find("SelectionUI").gameObject.SetActive(true);
                    break;
                case "background":
                    _currentBackgroundSelectedSkin = PlayerPrefs.GetInt("Background Skin", 0);
                    backgroundSkins[_currentBackgroundSelectedSkin].Find("SelectionUI").gameObject.SetActive(true);
                    break;
            }
            
        }

        internal void UnselectOtherSkins(int lastSelected, string thing)
        {
            switch (thing)
            {
                case "wolfie":
                    for (int i = 0; i < wolfieSkins.Length; i++)
                    {
                        if (i == lastSelected) continue;
                        wolfieSkins[i].Find("SelectionUI").gameObject.SetActive(false);
                    }
                    break;
                case "background":
                    for (int i = 0; i < backgroundSkins.Length; i++)
                    {
                        if (i == lastSelected) continue;
                        backgroundSkins[i].Find("SelectionUI").gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }
}
