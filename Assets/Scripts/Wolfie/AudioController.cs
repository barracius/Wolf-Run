using UnityEngine;

namespace Wolfie
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private MainController mainController = null;
        [SerializeField] private AudioSource audioSource = null;
        [SerializeField] private AudioClip biteSound = null;
        [SerializeField] private AudioClip jumpSound = null;
        [SerializeField] private AudioClip slideSound = null;
        [SerializeField] private AudioClip clockSound = null;
        [SerializeField] private AudioClip shieldSound = null;

        private void Update()
        {
            switch (mainController.state)
            {
                case "biting2":
                    audioSource.clip = biteSound;
                    audioSource.Play();
                    mainController.state = "running";
                    break;
                case "jumping2":
                    audioSource.clip = jumpSound;
                    audioSource.Play();
                    mainController.state = "running";
                    break;
                case "sliding2":
                    audioSource.clip = slideSound;
                    audioSource.Play();
                    mainController.state = "running";
                    break;
                default:
                    break;
            }
        }
    }
}
