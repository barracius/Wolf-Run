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
    }
}
