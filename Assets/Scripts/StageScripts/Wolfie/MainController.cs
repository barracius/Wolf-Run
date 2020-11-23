using System;
using System.Collections;
using Helpers;
using UnityEngine;

namespace StageScripts.Wolfie
{
    public class MainController : MonoBehaviour
    {
        [SerializeField] internal CollisionController collisionController = null;
        [SerializeField] internal AudioController audioController = null;
        [SerializeField] internal InputController inputController = null;
        [SerializeField] internal PhysicsController physicsController = null;
        [SerializeField] internal float maxSlideTime = 1.5f;
        internal GameObject Barrier;
        internal SpriteRenderer SrBarrier;
        [SerializeField] internal WolfieState wolfieState = WolfieState.Running;
        private float _slideTimer = 0f;
        private bool _isSliding = false;
        internal bool IsJumping = false;


        internal int ShieldCharges = 0;
        private void Update()
        {
            if (wolfieState == WolfieState.OnFire)
            {
                GameControl.Instance.Loss();
            }
        }

        private void FixedUpdate()
        {
            StartCoroutine(MakeAction());
        }

        private IEnumerator MakeAction()
        {
            switch (wolfieState)
            {
                case WolfieState.Stunned:
                    physicsController.GetStunned();
                    break;
                case WolfieState.Jumping when !IsJumping:
                    physicsController.Jump();
                    audioController.Jump();
                    IsJumping = true;
                    wolfieState = WolfieState.Running;
                    break;
                case WolfieState.Biting:
                    physicsController.Bite();
                    audioController.Bite();
                    wolfieState = WolfieState.Running;
                    break;
                case WolfieState.Sliding when !_isSliding:
                    yield return StartCoroutine(Slide());
                    break;
            }
        }

        private void Start()
        {
            
            Barrier = transform.Find("Barrier").gameObject;
            SrBarrier = Barrier.GetComponent<SpriteRenderer>();
            Barrier.SetActive(false);
            
        }

        private void CheckSliderTimer()
        {
            if (_isSliding)
            {
                _slideTimer += Time.deltaTime;
                if (_slideTimer > maxSlideTime)
                {
                    _isSliding = false;
                    physicsController.SlideEnd();
                }
            }
        }

        private IEnumerator Slide()
        {
            _isSliding = true;
            audioController.Slide();
            physicsController.SlideBegin();
            yield return new WaitForSeconds(maxSlideTime);
            physicsController.SlideEnd();
            wolfieState = WolfieState.Running;
            _isSliding = false;
        }
    }
}
