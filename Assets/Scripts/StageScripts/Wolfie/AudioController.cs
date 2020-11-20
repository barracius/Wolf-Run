using UnityEngine;

namespace StageScripts.Wolfie
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
            switch (mainController.State)
            {
                case "biting2":
                    audioSource.clip = biteSound;
                    audioSource.Play();
                    mainController.State = "running";
                    break;
                case "jumping2":
                    audioSource.clip = jumpSound;
                    audioSource.Play();
                    mainController.State = "running";
                    break;
                case "sliding2":
                    audioSource.clip = slideSound;
                    audioSource.Play();
                    mainController.State = "running";
                    break;
                default:
                    break;
            }
        }
    }
}
