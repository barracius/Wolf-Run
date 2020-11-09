using System;
using UnityEngine;

namespace Wolfie
{
    public class PhysicsController : MonoBehaviour
    {
        [SerializeField] private MainController mainController;
        [SerializeField] private float jumpHeight = 650; 
        private const int MovementSpeed = 5;
        internal bool IsJumping;
        internal bool IsSliding;
        private float slideTimer = 0f;
        public float maxSlideTime = 1.5f;
        private void FixedUpdate()
        {
            if (mainController.collisionController.Stunned)
            {
                transform.Translate(MovementSpeed * Time.deltaTime * Vector3.left);
            }
            
            
            if (mainController.state.Equals("jumping1") && !IsJumping && !IsSliding)
            {
                Jump();
            }
            else if (mainController.state.Equals("sliding1") && !IsSliding && !IsJumping)
            {
                Slide();
            }
        }

        private void Update()
        {
            if (mainController.state.Equals("biting1") && GameControl.Instance.obstaclesInScene.Count > 0)
            {
                Bite();
            }

            if (IsSliding)
            {
                slideTimer += Time.deltaTime;
                if (slideTimer > maxSlideTime)
                {
                    IsSliding = false;
                    transform.localScale = new Vector3((float) 0.33, (float) 0.33);
                }
            }
            
        }

        private void Slide()
        {
            slideTimer = 0;
            IsSliding = true;
            transform.localScale = new Vector2((float)0.2, (float)0.2);
            mainController.state = "sliding2";
        }
        private void Jump()
        {
            IsJumping = true;
            mainController.rb.AddForce(Vector2.up * jumpHeight);
            mainController.state = "jumping2";
        }

        private void Bite()
        {
            if (GameControl.Instance.obstaclesInScene != null && GameControl.Instance.obstaclesInScene[0].tag.Contains("Bite"))
            {
                GameObject obstacleToDelete = GameControl.Instance.obstaclesInScene[0];
                GameControl.Instance.obstaclesInScene.RemoveAt(0);
                Destroy(obstacleToDelete);
            }
            mainController.state = "biting2";
        }
    }
}
