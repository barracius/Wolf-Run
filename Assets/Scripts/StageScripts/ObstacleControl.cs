using System;
using UnityEngine;

namespace StageScripts
{
    public class ObstacleControl : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = -5f;
        private bool _onPause = false;

        private void Update()
        {
            if (_onPause) return;
            var transform1 = transform;
            var position = transform1.position;
            position = new Vector2(position.x + moveSpeed * Time.deltaTime, position.y);
            transform1.position = position;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.transform.CompareTag("Fire")) return;
            Destroy(gameObject); 
            MainScript.Instance.obstaclesInScene.RemoveAt(0);
        }

        private void Start()
        {
            MainScript.Instance.PauseEvent += Pause;
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
