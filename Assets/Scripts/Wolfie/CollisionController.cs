using System;
using UnityEngine;

namespace Wolfie
{
    public class CollisionController : MonoBehaviour
    {
        [SerializeField] private MainController mainController;
        internal bool OnFire;
        internal bool Stunned;
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Fire"))
            {
                OnFire = true;
            }

            if (other.gameObject.tag.Contains("Obstacle"))
            {
                Destroy(other.gameObject);
                Stunned = true;
            }

            if (other.gameObject.CompareTag("Ground"))
            {
                mainController.physicsController.IsJumping = false;
            }

            if (other.gameObject.CompareTag("PowerUp"))
            {
                GameControl.Instance.obstaclesInScene.RemoveAt(0);
                Destroy(other.gameObject);
                // Power def
            }
        }
    }
}
