using System;
using System.Xml.Schema;
using Helpers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace MainMenuScripts
{
    public class MainScript : MonoBehaviour
    {
        [SerializeField] private GameObject background = null;

        private void Start()
        {
            background.GetComponent<BackgroundImageScript>().SetBackground();
        }

        public void OnCustomizationButtonClick()
        {
            SceneManager.LoadScene("Customization");
        }

        public void OnAchievementsButtonClick()
        {
            SceneManager.LoadScene("Achievements");
        }

        public void OnLevelSelectorButtonClick()
        {
            SceneManager.LoadScene("LevelSelection");
        }

        public void ResetAllScoresButtonPressed()
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("MainMenu");
        }
        
        private void Update()
        {
            Methods.CheckInputAndGoBackToMainMenu();
            Methods.CheckInputAndCloseGame();
            Cheat();
        }

        private void Cheat()
        {
            if (!Input.GetKey(KeyCode.LeftShift)) return;
            if (Input.GetKey(KeyCode.Alpha1))
            {
                   PlayerPrefs.SetInt("level1Score", 30000);
                   PlayerPrefs.SetInt("level1Stars", 3);
                   Debug.Log("Cheat 1 Activated");
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                PlayerPrefs.SetInt("level2Score", 30000);
                PlayerPrefs.SetInt("level2Stars", 3);
                Debug.Log("Cheat 2 Activated");
            }
        }
    }
}
