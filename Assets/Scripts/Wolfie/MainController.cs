using System;
using UnityEngine;

namespace Wolfie
{
    public class MainController : MonoBehaviour
    {
        [SerializeField] internal CollisionController collisionController;
        [SerializeField] internal AudioController audioController;
        [SerializeField] internal InputController inputController;
        [SerializeField] internal PhysicsController physicsController;
        internal String state = "running";
        private void Update()
        {
            if (collisionController.OnFire)
            {
                GameControl.Instance.Loss();
            }
        }
    }
}
