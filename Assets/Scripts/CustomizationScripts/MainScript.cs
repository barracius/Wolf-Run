﻿using System;
using Helpers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CustomizationScripts
{
    public class MainScript : MonoBehaviour
    {
        public static MainScript Instance;
        [SerializeField] private GameObject customizationPanel = null;
        private GameObject _currentCustomizationButtonSelected = null;
        private CustomizationButtonScript _currentCustomizationButtonScript = null;
        private CustomizationPanelScript _customizationPanelScript = null;

        public event Action UpdateBackground;
        
        public void OnGoBackButtonClick()
        {
            Methods.GoBackToMainMenu();
        }
        
        public void OnSkinClick()
        {
            _currentCustomizationButtonSelected = EventSystem.current.currentSelectedGameObject;
            _currentCustomizationButtonScript = _currentCustomizationButtonSelected.GetComponent<CustomizationButtonScript>();
            if (_currentCustomizationButtonScript.isLocked) return;
            
            
            _currentCustomizationButtonScript.SelectingUiChange(true);
            switch (_currentCustomizationButtonScript.skinType)
            {
                case SkinType.Wolfie:
                    PlayerPrefs.SetInt("Wolf Body Skin", _currentCustomizationButtonScript.skinNumber);
                    _customizationPanelScript.UnselectOtherSkins(_currentCustomizationButtonScript.skinNumber, SkinType.Wolfie);
                    break;
                case SkinType.Background:
                    PlayerPrefs.SetInt("MainMenu Background Skin", _currentCustomizationButtonScript.skinNumber);
                    _customizationPanelScript.UnselectOtherSkins(_currentCustomizationButtonScript.skinNumber, SkinType.Background);
                    UpdateBackground?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Start()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this) Destroy(gameObject);
            _customizationPanelScript = customizationPanel.GetComponent<CustomizationPanelScript>();
            Methods.PlayMainMenuMusic();
        }
        
        private void Update()
        {
            Methods.CheckInputAndGoBackToMainMenu();
            Methods.CheckInputAndCloseGame();
        }
    }
}
