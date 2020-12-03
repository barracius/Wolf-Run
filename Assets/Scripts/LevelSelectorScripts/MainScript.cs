using System;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelSelectorScripts
{
    public class MainScript : MonoBehaviour
    {
        public void GoBackButtonPressed()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void Update()
        {
            Methods.CheckInputAndGoBackToMainMenu();
            Methods.CheckInputAndCloseGame();
        }

        private void Start()
        {
            Methods.PlayMainMenuMusic();
        }
    }
}
