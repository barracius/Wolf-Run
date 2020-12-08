using UnityEngine;

namespace StageScripts
{
    public class MoveLeftCycle : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3;

        [SerializeField] private float leftWayPointX = -13, rightWayPointX = 13;
        private bool _onPause = false;
    
        private void FixedUpdate()
        {
            if (_onPause) return;
            var transform1 = transform;
            var position = transform1.position;
            position = new Vector2(position.x + moveSpeed * Time.deltaTime, position.y);
            transform1.position = position;
            if (!(transform.position.x < leftWayPointX)) return;
            var transform2 = transform;
            transform2.position = new Vector2(rightWayPointX, transform2.position.y);
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
