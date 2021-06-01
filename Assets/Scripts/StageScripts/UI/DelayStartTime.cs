using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace StageScripts.UI
{
    public class DelayStartTime : MonoBehaviour
    {
        public GameObject counterGO;
        private float _counterTimeLeft;
        private const float counterDuration = 4f;
        private bool _isCounterActivated = false;
        private Text _counterText;

        private void Start()
        {
            MainScript.instance.UnpauseEvent += Unpause;
            _counterText = counterGO.GetComponent<Text>();
            counterGO.gameObject.SetActive(false);
        }

        private void Unpause()
        {
            gameObject.SetActive(false);
            _counterText.text = "";
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGameCounter();
            }
        }
        
        private IEnumerator DisplayCount()
        {
            counterGO.gameObject.SetActive(true);
            if (_isCounterActivated)
            {
                _counterTimeLeft = counterDuration;
            }
            else
            {
                _isCounterActivated = true;
                for (_counterTimeLeft = counterDuration; _counterTimeLeft > 0; _counterTimeLeft -= Time.unscaledDeltaTime)
                {
                    var counterTimeLeft = (int) _counterTimeLeft;
                    _counterText.text = counterTimeLeft.ToString();
                    yield return null;
                }
                _isCounterActivated = false;
                MainScript.instance.OnStartButtonPressed();
            }
            
        }

        public void StartGameCounter()
        {
            StartCoroutine(DisplayCount());
        }
    }
}
