using MainMenuScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Helpers
{
    public static class Methods
    {
        public static int CalculateTotalStars()
        {
            var counter = 0;
            for (int i = 1; i <= (int) Helpers.GobalVariables.NumberOfStages; i++)
            {
                var stars = PlayerPrefs.GetInt("level" + i + "Stars", 0);
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
    }
}