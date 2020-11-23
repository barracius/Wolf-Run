using Helpers;
using UnityEngine;

namespace StageScripts.Wolfie
{
    public class CollisionController : MonoBehaviour
    {
        [SerializeField] private MainController mainController = null;
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Fire"))
            {
                mainController.wolfieState = WolfieState.OnFire;
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
                    mainController.wolfieState = WolfieState.Stunned;
                }
            }

            if (other.gameObject.CompareTag("Ground"))
            {
                mainController.IsJumping = false;
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
