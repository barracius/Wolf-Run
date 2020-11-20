using UnityEngine;

namespace StageScripts.Wolfie
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private MainController mainController = null;

        private void Update()
        {
            if (GameControl.GameStopped || mainController.State != "running") return;
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                mainController.State = "biting1";
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                mainController.State = "jumping1";
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                mainController.State = "sliding1";
            }
        }
    }
}
