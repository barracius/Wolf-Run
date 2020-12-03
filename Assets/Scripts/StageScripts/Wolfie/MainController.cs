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
        internal bool IsJumping = false, IsBiting = false, IsSliding = false;
        internal int ShieldCharges = 0;
        private GameObject _biteGameObject;
        private BiteScript _biteScript;
        private bool _lossSoundPlayed = false;

        private void Update()
        {
            if (wolfieState != WolfieState.OnFire) return;
            GameControl.Instance.GameOver();
            PlayGameOverSound();
        }

        private void PlayGameOverSound()
        {
            if (_lossSoundPlayed) return;
            audioController.PlaySound(Sounds.LossGameSound);
            _lossSoundPlayed = true;
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
                    audioController.PlaySound(Sounds.JumpSound);
                    IsJumping = true;
                    wolfieState = WolfieState.Running;
                    break;
                case WolfieState.Biting when !IsBiting && !IsJumping:
                    IsBiting = true;
                    yield return _biteScript.Bite();
                    audioController.PlaySound(Sounds.BiteSound);
                    wolfieState = WolfieState.Running;
                    break;
                case WolfieState.Sliding when !IsSliding && !IsJumping:
                    yield return StartCoroutine(Slide());
                    break;
                case WolfieState.Running:
                    break;
                case WolfieState.OnFire:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Start()
        {
            
            Barrier = transform.Find("Barrier").gameObject;
            SrBarrier = Barrier.GetComponent<SpriteRenderer>();
            Barrier.SetActive(false);

            _biteGameObject = transform.Find("BiteGameObject").gameObject;
            _biteScript = _biteGameObject.GetComponent<BiteScript>();
            _biteGameObject.SetActive(false);
        }

        private IEnumerator Slide()
        {
            IsSliding = true;
            audioController.PlaySound(Sounds.SlideSound);
            physicsController.SlideBegin();
            yield return new WaitForSeconds(maxSlideTime);
            physicsController.SlideEnd();
            wolfieState = WolfieState.Running;
            IsSliding = false;
        }
    }
}
