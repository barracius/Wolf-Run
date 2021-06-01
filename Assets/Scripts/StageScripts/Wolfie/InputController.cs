using System;
using UnityEngine;

namespace StageScripts.Wolfie
{
    public class InputController : MonoBehaviour
    {
        public event Action OnJumpInputPressed;
        public event Action OnSlideInputPressed;
        public event Action OnBiteInputPressed;

        private bool _onPause = true;

        private void Update()
        {
            if (_onPause) return;
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                OnBiteInputPressed?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                OnJumpInputPressed?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                OnSlideInputPressed?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                MainScript.instance.OnPauseButtonPress();
            }
        }

        private void Start()
        {            MainScript.instance.PauseEvent += Pause;
            MainScript.instance.UnpauseEvent += Unpause;
        }

        private void Pause()
        {
            _onPause = true;
        }

        private void Unpause()
        {
            _onPause = false;
        }
    }
}
