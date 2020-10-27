using System;
using UnityEngine;

namespace Wolfie
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private MainController mainController;
        internal bool Biting;
        internal bool Jumping;
        internal bool Sliding;

        private void Update()
        {
            if (!GameControl.GameStopped && mainController.state == "running")
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Biting = true;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Jumping = true;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Sliding = true;
                }
            }
        }
    }
}
