using UnityEngine;

namespace StageScripts.Wolfie
{
    public class CollisionController : MonoBehaviour
    {
        [SerializeField] private MainController mainController = null;
        internal bool OnFire = false;
        internal bool Stunned = false;
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Fire"))
            {
                OnFire = true;
            }

            if (other.gameObject.tag.Contains("Obstacle"))
            {
                Destroy(other.gameObject);
                if (mainController.ShieldCharges > 0)
                {
                    mainController.ShieldCharges -= 1;
                    GameControl.Instance.ShieldPowerUpUI(mainController.ShieldCharges);
                    GameControl.Instance.obstaclesInScene.RemoveAt(0);
                }
                else
                {
                    Stunned = true;
                }
            }

            if (other.gameObject.CompareTag("Ground"))
            {
                mainController.physicsController.IsJumping = false;
            }

            if (other.gameObject.tag.Contains("PowerUp"))
            {
                GameControl.Instance.obstaclesInScene.RemoveAt(0);
                if (other.gameObject.tag.Contains("Clock"))
                {
                    GameControl.Instance.ClockPowerUpActivation();
                }
                else if (other.gameObject.tag.Contains("Shield"))
                {
                    if (mainController.ShieldCharges == 3)
                    {
                        Destroy(other.gameObject);
                        return;
                    }
                    mainController.ShieldCharges += 1;
                    GameControl.Instance.ShieldPowerUpUI(mainController.ShieldCharges);
                }
                Destroy(other.gameObject);
            }
        }
    }
}
