using UnityEngine;

namespace Wolfie
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private MainController mainController;
        [SerializeField] private AudioSource biteSound;
        [SerializeField] private AudioSource jumpSound;
        [SerializeField] private AudioSource slideSound;

        private void Update()
        {
            if (mainController.state == "biting2")
            {
                biteSound.Play();
                mainController.state = "running";
            }
            else if (mainController.state == "jumping2")
            {
                jumpSound.Play();
                mainController.state = "running";
            }
            else if (mainController.state == "sliding2")
            {
                slideSound.Play();
                mainController.state = "running";
            }
        }
    }
}
