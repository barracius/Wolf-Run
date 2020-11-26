using UnityEngine;

namespace StageScripts.Wolfie
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource = null;
        [SerializeField] private AudioClip biteSound = null;
        [SerializeField] private AudioClip jumpSound = null;
        [SerializeField] private AudioClip slideSound = null;
        [SerializeField] private AudioClip clockSound = null;
        [SerializeField] private AudioClip shieldSound = null;

        internal void Bite()
        {
            audioSource.clip = biteSound;
            audioSource.Play();
        }

        internal void Jump()
        {
            audioSource.clip = jumpSound;
            audioSource.Play();
        }

        internal void Slide()
        {
            audioSource.clip = slideSound;
            audioSource.Play();
        }
    }
}
