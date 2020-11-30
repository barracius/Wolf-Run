using System;
using Helpers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace MainMenuScripts
{
    public class MainScript : MonoBehaviour
    {
        [SerializeField] private Canvas skinsCanvas;
        [SerializeField] private Transform skinPanel;
        private GameObject _currentSkinButtonSelected;
        private SkinButtonScript _currentSkinButtonScriptSelectedScript;
        private SkinPanelScript _skinPanelScript;
        [SerializeField] private GameObject background;
        private BackgroundImageScript _backgroundImageScript;

        private void Start()
        {
            skinsCanvas.gameObject.SetActive(false);
            _skinPanelScript = skinPanel.gameObject.GetComponent<SkinPanelScript>();
            _backgroundImageScript = background.GetComponent<BackgroundImageScript>();

        }

        public void OnSkinsButtonClick()
        {
            skinsCanvas.gameObject.SetActive(true);
            _skinPanelScript.SelectCurrentSkins();
        }

        public void OnLevelSelectorButtonClick()
        {
            SceneManager.LoadScene("LevelSelector");
        }

        public void OnGoBackButtonClick()
        {
            skinsCanvas.gameObject.SetActive(false);
        }

        public void OnSkinClick()
        {
            _currentSkinButtonSelected = EventSystem.current.currentSelectedGameObject;
            _currentSkinButtonScriptSelectedScript = _currentSkinButtonSelected.GetComponent<SkinButtonScript>();
            if (_currentSkinButtonScriptSelectedScript.isLocked) return;
            
            
            _currentSkinButtonScriptSelectedScript.SelectingUiChange(true);
            switch (_currentSkinButtonScriptSelectedScript.skinType)
            {
                case SkinType.Wolfie:
                    PlayerPrefs.SetInt("Wolf Body Skin", _currentSkinButtonScriptSelectedScript.skinNumber);
                    _skinPanelScript.UnselectOtherSkins(_currentSkinButtonScriptSelectedScript.skinNumber, SkinType.Wolfie);
                    break;
                case SkinType.Background:
                    PlayerPrefs.SetInt("MainMenu Background Skin", _currentSkinButtonScriptSelectedScript.skinNumber);
                    _skinPanelScript.UnselectOtherSkins(_currentSkinButtonScriptSelectedScript.skinNumber, SkinType.Background);
                    _backgroundImageScript.SetBackground();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void ResetAllScoresButtonPressed()
        {
//            for (int i = 1; i <= amountOfStages; i++)
//            {
//                PlayerPrefs.SetInt("level" + i + "Stars", 0);
//                PlayerPrefs.SetInt("level" + i + "Score", 0);
//            }
            
            PlayerPrefs.DeleteAll();

            SceneManager.LoadScene("MainMenu");
        }
    }
}
