using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AchievementsScripts
{
    public class MainScript : MonoBehaviour
    {
        public void OnGoBackButtonClick()
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
