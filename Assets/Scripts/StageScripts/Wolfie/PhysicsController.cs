using System;
using UnityEngine;

namespace StageScripts.Wolfie
{
    public class PhysicsController : MonoBehaviour
    {
        [SerializeField] private float jumpHeight = 650; 
        private const int MovementSpeed = 5;
        public Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        internal void GetStunned()
        {
            transform.Translate(MovementSpeed * Time.deltaTime * Vector3.left);
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
            rb.AddForce(Vector2.up * jumpHeight);
        }

        internal void Bite()
        {
            if (GameControl.Instance.obstaclesInScene == null ||
                !GameControl.Instance.obstaclesInScene[0].tag.Contains("Bite")) return;
            GameObject obstacleToDelete = GameControl.Instance.obstaclesInScene[0];
            GameControl.Instance.obstaclesInScene.RemoveAt(0);
            Destroy(obstacleToDelete);
        }
        
        
        
        
    }
}
