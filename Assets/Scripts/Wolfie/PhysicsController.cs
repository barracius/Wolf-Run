using UnityEngine;

namespace Wolfie
{
    public class PhysicsController : MonoBehaviour
    {
        [SerializeField] private MainController mainController;
        private const int MovementSpeed = 5;
        private void FixedUpdate()
        {
            if (mainController.collisionController.Stunned)
            {
                transform.Translate(MovementSpeed * Time.deltaTime * Vector3.left);
            }
        }
    }
}
