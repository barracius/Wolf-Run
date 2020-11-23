using System;
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

        private void Start()
        {
            skinsCanvas.gameObject.SetActive(false);
            _skinPanelScript = skinPanel.gameObject.GetComponent<SkinPanelScript>();
        }

        public void OnSkinsButtonClick()
        {
            skinsCanvas.gameObject.SetActive(true);
            _skinPanelScript.SelectCurrentSkin("wolfie");
        }

        public void OnLevelSelectorButtonClick()
        {
            SceneManager.LoadScene("LevelSelector");
        }

        public void OnGoBackButtonClick()
        {
            skinsCanvas.gameObject.SetActive(false);
        }

        public void OnWolfieSkinsClick()
        {
            _currentSkinButtonSelected = EventSystem.current.currentSelectedGameObject;
            _currentSkinButtonScriptSelectedScript = _currentSkinButtonSelected.GetComponent<SkinButtonScript>();
            _currentSkinButtonScriptSelectedScript.SelectionUi.gameObject.SetActive(true);
            PlayerPrefs.SetInt("Wolf Body Skin", _currentSkinButtonScriptSelectedScript.skinNumber);
            _skinPanelScript.UnselectOtherSkins(_currentSkinButtonScriptSelectedScript.skinNumber, "wolfie");
        }
    }
}
