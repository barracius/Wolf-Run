using System;
using UnityEngine;

namespace Wolfie
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private MainController mainController = null;

        private void Update()
        {
            if (GameControl.GameStopped || mainController.state != "running") return;
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                mainController.state = "biting1";
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                mainController.state = "jumping1";
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                mainController.state = "sliding1";
            }
        }
    }
}
