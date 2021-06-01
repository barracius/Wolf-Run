using System;
using UnityEngine;

namespace StageScripts.Wolfie
{
    public class CollisionController : MonoBehaviour
    {
        public event Action OnObstacleHit;
        public event Action OnJumpLanding;
        public event Action OnClockPowerUpCollected;
        public event Action OnShieldPowerUpCollected;
        public event Action OnFireTouched;
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Fire"))
            {
                OnFireTouched?.Invoke();
            }

            if (other.gameObject.tag.Contains("Obstacle"))
            {
                Destroy(other.gameObject);
                OnObstacleHit?.Invoke();
            }

            if (other.gameObject.CompareTag("Ground"))
            {
                OnJumpLanding?.Invoke();
            }

            if (other.gameObject.tag.Contains("PowerUp"))
            {
                Destroy(other.gameObject);
                MainScript.instance.obstaclesInScene.RemoveAt(0);
                if (other.gameObject.tag.Contains("Clock"))
                {
                    OnClockPowerUpCollected?.Invoke();
                }
                else if (other.gameObject.tag.Contains("Shield"))
                {
                    OnShieldPowerUpCollected?.Invoke();
                }
            }
        }
    }
}
