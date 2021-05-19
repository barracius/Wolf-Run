using System;
using Helpers;
using UnityEditor;
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
        }

        private void Start()
        {            MainScript.Instance.PauseEvent += Pause;
            MainScript.Instance.UnpauseEvent += Unpause;
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
