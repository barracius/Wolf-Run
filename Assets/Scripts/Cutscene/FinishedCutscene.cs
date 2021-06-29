using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cutscene
{
    public class FinishedCutscene : MonoBehaviour
    {
        void Start()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
