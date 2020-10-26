using UnityEngine;

namespace Wolfie
{
    public class MainController : MonoBehaviour
    {
        [SerializeField] internal CollisionController collisionController;

        private void Update()
        {
            if (collisionController.OnFire)
            {
                GameControl.Instance.Loss();
            }
        }
    }
}
