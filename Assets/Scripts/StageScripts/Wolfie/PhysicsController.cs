using UnityEngine;

namespace StageScripts.Wolfie
{
    public class PhysicsController : MonoBehaviour
    {
        private const float JumpHeight = 750f; 
        private const float MovementSpeed = 400f;
        public Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        internal void GetStunned()
        {
            rb.AddForce(Vector2.left * MovementSpeed);
        }

        internal void SlideEnd()
        {
            transform.localScale = new Vector3((float) 0.33, (float) 0.33);
        }

        internal void SlideBegin()
        {
            transform.localScale = new Vector2((float)0.2, (float)0.2);
        }
        internal void Jump()
        {
            rb.AddForce(Vector2.up * JumpHeight);
        }
    }
}
