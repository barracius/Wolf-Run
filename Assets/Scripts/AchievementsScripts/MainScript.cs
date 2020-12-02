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
    }
}
