using System;
using System.Collections;
using Helpers;
using UnityEngine;

namespace StageScripts.Wolfie.Bite
{
    public class BiteScript : MonoBehaviour
    {
        internal Vector2 StartPosition;
        private Vector2 _currentPosition;
        public float speed = 8f;
        private bool _isBiting;
        private MainController _wolfieMainController;
        public event Action OnStopBiting;
        public AudioSource audioSource;

        private void Start()
        {
            StartPosition = transform.position;
            _wolfieMainController = gameObject.GetComponentInParent<MainController>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.tag.Contains("Bite")) return;
            audioSource.Play();
            var obstacleToDelete = other.gameObject;
            MainScript.instance.obstaclesInScene.RemoveAt(0);
            Destroy(obstacleToDelete);
        }

        private IEnumerator BiteEnumerator(Action<bool> resultCB)
        {
            var endXPosition = _currentPosition.x + 8f;
            while (endXPosition > _currentPosition.x)
            {
                Transform transform1;
                (transform1 = transform).Translate(Time.deltaTime * speed * Vector2.right);
                _currentPosition = transform1.position;
                yield return null;
            }
            gameObject.SetActive(false);
            transform.position = StartPosition;
            _isBiting = false;
            resultCB?.Invoke( _isBiting );
        }

        public IEnumerator Bite()
        {
            _isBiting = true;
            gameObject.SetActive(true);
            StartPosition = transform.position;
            _currentPosition = StartPosition;
            yield return Process ( r => _isBiting = r );
            if (!_isBiting)
            {
                OnStopBiting?.Invoke();
            }
        }

        YieldInstruction Process ( Action<bool> resultCB )
        {
            return StartCoroutine ( BiteEnumerator ( resultCB ) );
        }
    }
    
}
