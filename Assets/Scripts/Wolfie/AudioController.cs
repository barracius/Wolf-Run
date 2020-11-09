using UnityEngine;

namespace Wolfie
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private MainController mainController;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip biteSound;
        [SerializeField] private AudioClip jumpSound;
        [SerializeField] private AudioClip slideSound;
        [SerializeField] private AudioClip clockSound;

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
