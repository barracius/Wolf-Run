using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelSelectorScripts
{
    public class UiScript : MonoBehaviour
    {
        public void GoBackButtonPressed()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
