using Helpers;
using UnityEngine;

namespace StageScripts.Wolfie
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private MainController mainController = null;

        private void Update()
        {
            if (GameControl.GameStopped || mainController.wolfieState != WolfieState.Running) return;
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                mainController.wolfieState = WolfieState.Biting;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                mainController.wolfieState = WolfieState.Jumping;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                mainController.wolfieState = WolfieState.Sliding;
            }
        }
    }
}
