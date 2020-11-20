using System;
using UnityEngine;

namespace StageScripts.Wolfie
{
    public class MainController : MonoBehaviour
    {
        [SerializeField] internal CollisionController collisionController = null;
        [SerializeField] internal AudioController audioController = null;
        [SerializeField] internal InputController inputController = null;
        [SerializeField] internal PhysicsController physicsController = null;
        internal GameObject Barrier;
        internal SpriteRenderer SrBarrier;
        internal string State = "running";
        public Rigidbody2D rb;
        internal int ShieldCharges = 0;
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
            Barrier = transform.Find("Barrier").gameObject;
            SrBarrier = Barrier.GetComponent<SpriteRenderer>();
            Barrier.SetActive(false);
            
        }
    }
}
