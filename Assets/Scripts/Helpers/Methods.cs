using MainMenuScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Helpers
{
    public static class 
        
        Methods
    {
        public static int CalculateTotalStars()
        {
            var counter = 0;
            for (int i = 1; i <= (int) Helpers.GlobalVariables.NumberOfStages; i++)
            {
                var stars = GetStarsInStage(i);
                counter += stars;
            }

            return counter;
        }

        public static void CheckInputAndGoBackToMainMenu()
        {
            if (Input.GetKey(KeyCode.Backspace))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        public static void CheckInputAndCloseGame()
        {
            if (Input.GetKey(KeyCode.Escape)){
                Application.Quit();                
            }
        }

        public static void PlayMainMenuMusic()
        {
            GameObject.FindGameObjectWithTag("Music").GetComponent<MusicScript>().PlayMusic();
        }

        public static void DontPlayMainMenuMusic()
        {
            GameObject.FindGameObjectWithTag("Music").GetComponent<MusicScript>().StopMusic();
        }

        public static void GoBackToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public static void GoBackToLevelSelection()
        {
            SceneManager.LoadScene("LevelSelection");
        }

        public static int GetScoreInStage(int stage)
        {
            return PlayerPrefs.GetInt("level" + stage + "Score", 0);
        }

        public static int GetStarsInStage(int stage)
        {
            return PlayerPrefs.GetInt("level" + stage + "Stars", 0);
        }
    }
}