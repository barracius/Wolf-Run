using Helpers;
using UnityEngine;

namespace StageScripts.Wolfie
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip biteSound;
        [SerializeField] private AudioClip jumpSound;
        [SerializeField] private AudioClip slideSound;
        //[SerializeField] private AudioClip clockSound = null;
        [SerializeField] private AudioClip shieldSound;
        [SerializeField] private AudioClip lossSound;
        [SerializeField] private AudioClip bittenTreeSound;

        internal void PlaySound(Sounds sound)
        {
            if (sound == Sounds.BiteSound) audioSource.clip = biteSound;
            else if (sound == Sounds.JumpSound) audioSource.clip = jumpSound;
            else if (sound == Sounds.SlideSound) audioSource.clip = slideSound;
            else if (sound == Sounds.ShieldSound) audioSource.clip = shieldSound;
            else if (sound == Sounds.LossGameSound) audioSource.clip = lossSound;
            else if (sound == Sounds.BittenTreeSound) audioSource.clip = bittenTreeSound;
            audioSource.Play();
        }
    }
}
