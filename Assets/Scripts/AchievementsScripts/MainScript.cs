using Helpers;
using UnityEngine;

namespace AchievementsScripts
{
    public class MainScript : MonoBehaviour
    {
        public void OnGoBackButtonClick()
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
