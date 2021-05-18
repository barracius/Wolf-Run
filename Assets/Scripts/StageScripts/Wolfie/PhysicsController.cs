using UnityEngine;

namespace StageScripts.Wolfie
{
    public class PhysicsController : MonoBehaviour
    {
        private const float JumpHeight = 750f; 
        private const float MovementSpeed = 400f;
        public Rigidbody2D rb;
        public BoxCollider2D _boxCollider2D;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            // _boxCollider2D.GetComponent<BoxCollider2D>();
        }

        internal void GetStunned()
        {
            rb.AddForce(Vector2.left * MovementSpeed);
        }

        internal void SlideEnd()
        {
            // transform.localScale = new Vector3((float) 0.33, (float) 0.33);
            _boxCollider2D.offset = new Vector2(6.37f, 7.8f);
        }

        internal void SlideBegin()
        {
            // transform.localScale = new Vector2((float)0.2, (float)0.2);
            _boxCollider2D.offset = new Vector2(6.37f, 3.7f);
        }
        internal void Jump()
        {
            rb.AddForce(Vector2.up * JumpHeight);
        }
    }
}
