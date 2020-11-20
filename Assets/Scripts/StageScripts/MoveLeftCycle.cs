using UnityEngine;

namespace StageScripts
{
    public class MoveLeftCycle : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3;

        [SerializeField] private float leftWayPointX = -13, rightWayPointX = 13;
    
        private void FixedUpdate()
        {
            var transform1 = transform;
            var position = transform1.position;
            position = new Vector2(position.x + moveSpeed * Time.deltaTime, position.y);
            transform1.position = position;
            if (!(transform.position.x < leftWayPointX)) return;
            var transform2 = transform;
            transform2.position = new Vector2(rightWayPointX, transform2.position.y);
        }
    }
}
