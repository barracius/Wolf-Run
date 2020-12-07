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
            Methods.GoBackToMainMenu();
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
