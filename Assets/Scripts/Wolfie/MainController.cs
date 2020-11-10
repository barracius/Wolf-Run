using System;
using UnityEngine;

namespace Wolfie
{
    public class MainController : MonoBehaviour
    {
        [SerializeField] internal CollisionController collisionController = null;
        [SerializeField] internal AudioController audioController = null;
        [SerializeField] internal InputController inputController = null;
        [SerializeField] internal PhysicsController physicsController = null;
        internal String state = "running";
        public Rigidbody2D rb;
        internal int shieldCharges = 0;
        private void Update()
        {
            if (collisionController.OnFire)
            {
                GameControl.Instance.Loss();
            }
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }
}
