using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cutscene
{
    public class InputHandler : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
