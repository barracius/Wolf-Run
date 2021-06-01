using UnityEngine;

namespace StageScripts
{
    public class ObstacleControl : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = -5f;
        private bool _onPause;

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
            MainScript.instance.obstaclesInScene.RemoveAt(0);
        }

        private void Start()
        {
            MainScript.instance.PauseEvent += Pause;
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
