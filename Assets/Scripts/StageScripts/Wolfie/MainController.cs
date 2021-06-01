using System;
using System.Collections;
using Helpers;
using StageScripts.Wolfie.Bite;
using UnityEngine;

namespace StageScripts.Wolfie
{
    public class MainController : MonoBehaviour
    {
        [SerializeField] internal AudioController audioController;
        [SerializeField] internal PhysicsController physicsController;
        [SerializeField] internal float maxSlideTime = 1.5f;
        internal GameObject barrier;
        [SerializeField] internal WolfieState wolfieState = WolfieState.Running;
        private bool _isJumping, _isBiting, _isSliding;
        private int _shieldCharges;
        [SerializeField] private GameObject biteGameObject;
        private BiteScript _biteScript;
        private Animator _animator;
        public Transform wolfieShadow;
        public Vector3 shadowRunningScale = new Vector3(5,1,1);
        public Vector3 shadowJumpingScale = new Vector3(3,1,1);
        public Vector3 shadowSlidingScale = new Vector3(6,1,1);
        private static readonly int isSliding = Animator.StringToHash("isSliding");
        private static readonly int takeOff = Animator.StringToHash("takeOff");
        private static readonly int isJumping = Animator.StringToHash("isJumping");
        private static readonly int isRunning = Animator.StringToHash("isRunning");
        private static readonly int onBeginSlide = Animator.StringToHash("onBeginSlide");
        public event Action OnEndSliding;


        private void SubscribeToEvents()
        {
            var inputController = GetComponent<InputController>();
            inputController.OnJumpInputPressed += Jump;
            inputController.OnSlideInputPressed += Slide;
            inputController.OnBiteInputPressed += Bite;
            var collisionController = GetComponent<CollisionController>();
            collisionController.OnJumpLanding += OnJumpLanding;
            collisionController.OnObstacleHit += ObstacleHit;
            collisionController.OnShieldPowerUpCollected += OnShieldPowerUpCollected;
            collisionController.OnFireTouched += OnFire;
            collisionController.OnClockPowerUpCollected += OnClockPowerUpCollected;
            _biteScript.OnStopBiting += OnStopBiting;
            OnEndSliding += OnSlideEnd;
        }

        private void OnSlideEnd()
        {
            physicsController.SlideEnd();
            _animator.SetBool(isSliding,false);
            wolfieState = WolfieState.Running;
            wolfieShadow.localScale = shadowRunningScale;
            _isSliding = false;
        }

        private void Jump()
        {
            if (_isJumping || _isSliding || _isBiting) return;
            _animator.SetTrigger(takeOff);
            wolfieState = WolfieState.Jumping;
            wolfieShadow.localScale = shadowJumpingScale;
            physicsController.Jump();
            audioController.PlaySound(Sounds.JumpSound);
            _isJumping = true;
            _animator.SetBool(isJumping, true);
        }

        private void Start()
        {
            
            barrier = transform.Find("Barrier").gameObject;
            barrier.SetActive(false);
            
            _biteScript = biteGameObject.GetComponent<BiteScript>();
            biteGameObject.SetActive(false);

            _animator = GetComponent<Animator>();
            _animator.SetBool(isRunning, true);
            SubscribeToEvents();
        }

        private void Slide()
        {
            if (_isJumping || _isSliding || _isBiting) return;
            _animator.SetTrigger(onBeginSlide);
            wolfieState = WolfieState.Sliding;
            wolfieShadow.localScale = shadowSlidingScale;
            StartCoroutine(SlideEnumerator());
        }
        
        private IEnumerator SlideEnumerator()
        {
            _isSliding = true;
            _animator.SetBool(isSliding,true);
            audioController.PlaySound(Sounds.SlideSound);
            physicsController.SlideBegin();
            wolfieShadow.localScale = shadowSlidingScale;
            yield return new WaitForSeconds(maxSlideTime);
            OnEndSliding?.Invoke();
        }

        private void Bite()
        {
            if (_isJumping || _isSliding || _isBiting) return;
            wolfieState = WolfieState.Biting;
            _isBiting = true;
            StartCoroutine(_biteScript.Bite());
            audioController.PlaySound(Sounds.BiteSound);
        }

        private void OnStopBiting()
        {
            _isBiting = false;
            wolfieState = WolfieState.Running;
        }

        private void ObstacleHit()
        {
            if (_shieldCharges > 0)
            {
                _shieldCharges -= 1;
                MainScript.instance.UpdateShieldPowerUpState(_shieldCharges);
                MainScript.instance.obstaclesInScene.RemoveAt(0);
            }
            else
            {
                if(_isSliding || _isBiting || _isJumping) StopAllCoroutines();
                wolfieState = WolfieState.Stunned;
                physicsController.GetStunned();
            }
        }

        private void OnJumpLanding()
        {
            if (_isJumping == false)
            {
                return;
            }
            _isJumping = false;
            _animator.SetBool(isJumping,false);
            wolfieShadow.localScale = shadowRunningScale;
            wolfieState = WolfieState.Running;
        }

        private void OnShieldPowerUpCollected()
        {
            if (_shieldCharges == 3)
            {
                return;
            }
            _shieldCharges += 1;
            MainScript.instance.UpdateShieldPowerUpState(_shieldCharges);
        }

        private void OnClockPowerUpCollected()
        {
            StartCoroutine(MainScript.instance.ClockPowerUpActivate());
        }

        private void OnFire()
        {
            wolfieState = WolfieState.OnFire;
            wolfieShadow.gameObject.SetActive(false);
            MainScript.instance.GameOver();
            audioController.PlaySound(Sounds.LossGameSound);
        }
    }
}
