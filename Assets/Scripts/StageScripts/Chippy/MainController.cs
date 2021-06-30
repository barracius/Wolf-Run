using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StageScripts.Chippy
{
    public class MainController : MonoBehaviour
    {
        public GameObject[] powerUps;
        private int _timeForNextPowerUp;
        public int timeMin = 30;
        public int timeMax = 60;
        private Transform _powerUpSpawner;
        private bool _onPause = true;
        private bool _waiting = false;
        public AudioSource audioSource;

        private void Start()
        {
            _powerUpSpawner = transform.Find("PowerUpSpawner");
            MainScript.instance.PauseEvent += Pause;
            MainScript.instance.UnpauseEvent += Unpause;
        }

        private void Update()
        {
            if (_onPause) return;
            if (_waiting) return;
            CalculateTimeForNextPowerUp();
            _waiting = true;
            StartCoroutine(WaitCoolDown(_timeForNextPowerUp));
        }


        private void CalculateTimeForNextPowerUp()
        {
            _timeForNextPowerUp = (int) Random.Range(timeMin, timeMax);
        }



        private void TossPowerUp()
        {
            audioSource.Play();
            var powerUp = Random.Range(0, powerUps.Length);
            MainScript.instance.obstaclesInScene.Add(Instantiate(powerUps[powerUp], _powerUpSpawner.position, Quaternion.identity));
        }

        private void Pause()
        {
            _onPause = true;
        }

        private void Unpause()
        {
            _onPause = false;
        }

        private IEnumerator WaitCoolDown(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            TossPowerUp();
            _waiting = false;
        }
    }
}
