using Helpers;
using UnityEngine;

namespace SettingsScripts
{
    public class MainScript : MonoBehaviour
    {
        private void Start()
        {
            Methods.PlayMainMenuMusic();
        }
    
        public void GoBackButtonPressed()
        {
            Methods.GoBackToMainMenu();
        }

        private void Update()
        {
            Methods.CheckInputAndGoBackToMainMenu();
            Methods.CheckInputAndCloseGame();
        }
    }
}
