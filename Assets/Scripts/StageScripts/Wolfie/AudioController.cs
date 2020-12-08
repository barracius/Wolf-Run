using Helpers;
using UnityEngine;

namespace StageScripts.Wolfie
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource = null;
        [SerializeField] private AudioClip biteSound = null;
        [SerializeField] private AudioClip jumpSound = null;
        [SerializeField] private AudioClip slideSound = null;
        //[SerializeField] private AudioClip clockSound = null;
        [SerializeField] private AudioClip shieldSound = null;
        [SerializeField] private AudioClip lossSound = null;
        [SerializeField] private AudioClip bittenTreeSound = null;

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
