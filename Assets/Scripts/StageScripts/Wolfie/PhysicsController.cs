using UnityEngine;

namespace StageScripts.Wolfie
{
    public class PhysicsController : MonoBehaviour
    {
        [SerializeField] private MainController mainController = null;
        [SerializeField] private float jumpHeight = 650; 
        private const int MovementSpeed = 5;
        internal bool IsJumping;
        private bool _isSliding;
        private float _slideTimer = 0f;
        public float maxSlideTime = 1.5f;
        private void FixedUpdate()
        {
            if (mainController.collisionController.Stunned)
            {
                transform.Translate(MovementSpeed * Time.deltaTime * Vector3.left);
            }
            
            
            if (mainController.State.Equals("jumping1") && !IsJumping && !_isSliding)
            {
                Jump();
            }
            else if (mainController.State.Equals("sliding1") && !_isSliding && !IsJumping)
            {
                Slide();
            }
        }

        private void Update()
        {
            if (mainController.State.Equals("biting1") && GameControl.Instance.obstaclesInScene.Count > 0)
            {
                Bite();
            }

            if (_isSliding)
            {
                _slideTimer += Time.deltaTime;
                if (_slideTimer > maxSlideTime)
                {
                    _isSliding = false;
                    transform.localScale = new Vector3((float) 0.33, (float) 0.33);
                }
            }
            
        }

        private void Slide()
        {
            _slideTimer = 0;
            _isSliding = true;
            transform.localScale = new Vector2((float)0.2, (float)0.2);
            mainController.State = "sliding2";
        }
        private void Jump()
        {
            IsJumping = true;
            mainController.rb.AddForce(Vector2.up * jumpHeight);
            mainController.State = "jumping2";
        }

        private void Bite()
        {
            if (GameControl.Instance.obstaclesInScene != null && GameControl.Instance.obstaclesInScene[0].tag.Contains("Bite"))
            {
                GameObject obstacleToDelete = GameControl.Instance.obstaclesInScene[0];
                GameControl.Instance.obstaclesInScene.RemoveAt(0);
                Destroy(obstacleToDelete);
            }
            mainController.State = "biting2";
        }
    }
}
