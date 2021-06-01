using UnityEngine;

namespace StageScripts.Wolfie
{
    public class PhysicsController : MonoBehaviour
    {
        private const float jumpHeight = 750f;
        public Rigidbody2D rb;
        public BoxCollider2D boxCollider2D;
        public Transform fire;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        internal void GetStunned()
        {
            var position = fire.position;
            rb.MovePosition(new Vector2(position.x - 2, position.y - 5));
        }

        internal void SlideEnd()
        {
            boxCollider2D.offset = new Vector2(6.37f, 7.8f);
        }

        internal void SlideBegin()
        {
            boxCollider2D.offset = new Vector2(6.37f, 3.7f);
        }
        internal void Jump()
        {
            rb.AddForce(Vector2.up * jumpHeight);
        }
        
    }
}
