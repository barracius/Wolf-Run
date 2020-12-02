using System;
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
    }
}
