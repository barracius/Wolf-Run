using System;
using System.Collections;
using Helpers;
using StageScripts.Wolfie.Bite;
using UnityEngine;

namespace StageScripts.Wolfie
{
    public class MainController : MonoBehaviour
    {
        [SerializeField] internal AudioController audioController = null;
        [SerializeField] internal PhysicsController physicsController = null;
        [SerializeField] internal float maxSlideTime = 1.5f;
        internal GameObject Barrier;
        internal SpriteRenderer SrBarrier;
        [SerializeField] internal WolfieState wolfieState = WolfieState.Running;
        private bool _isJumping = false, _isBiting = false, _isSliding = false;
        private int _shieldCharges = 0;
        private GameObject _biteGameObject;
        private BiteScript _biteScript;
        private Animator _animator;

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
        }

        private void Jump()
        {
            if (_isJumping || _isSliding || _isBiting) return;
            _animator.SetTrigger("takeOff");
            wolfieState = WolfieState.Jumping;
            physicsController.Jump();
            audioController.PlaySound(Sounds.JumpSound);
            _isJumping = true;
            _animator.SetBool("isJumping", true);
        }

        private void Start()
        {
            
            Barrier = transform.Find("Barrier").gameObject;
            SrBarrier = Barrier.GetComponent<SpriteRenderer>();
            Barrier.SetActive(false);

            _biteGameObject = transform.Find("BiteGameObject").gameObject;
            _biteScript = _biteGameObject.GetComponent<BiteScript>();
            _biteGameObject.SetActive(false);

            _animator = GetComponent<Animator>();
            _animator.SetBool("isRunning", true);
            SubscribeToEvents();
        }

        private void Slide()
        {
            if (_isJumping || _isSliding || _isBiting) return;
            _animator.SetTrigger("onBeginSlide");
            wolfieState = WolfieState.Sliding;
            StartCoroutine(SlideEnumerator());
        }
        
        private IEnumerator SlideEnumerator()
        {
            _isSliding = true;
            _animator.SetBool("isSliding",true);
            audioController.PlaySound(Sounds.SlideSound);
            physicsController.SlideBegin();
            yield return new WaitForSeconds(maxSlideTime);
            physicsController.SlideEnd();
            _animator.SetBool("isSliding",false);
            wolfieState = WolfieState.Running;
            _isSliding = false;
        }

        private void Bite()
        {
            wolfieState = WolfieState.Biting;
            if (_isJumping || _isSliding || _isBiting) return;
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
                MainScript.Instance.UpdateShieldPowerUpState(_shieldCharges);
                MainScript.Instance.obstaclesInScene.RemoveAt(0);
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
            _isJumping = false;
            _animator.SetBool("isJumping",false);
            wolfieState = WolfieState.Running;
        }

        private void OnShieldPowerUpCollected()
        {
            if (_shieldCharges == 3)
            {
                return;
            }
            _shieldCharges += 1;
            MainScript.Instance.UpdateShieldPowerUpState(_shieldCharges);
        }

        private void OnClockPowerUpCollected()
        {
            StartCoroutine(MainScript.Instance.ClockPowerUpActivate());
        }

        private void OnFire()
        {
            wolfieState = WolfieState.OnFire;
            MainScript.Instance.GameOver();
            audioController.PlaySound(Sounds.LossGameSound);
        }
    }
}
