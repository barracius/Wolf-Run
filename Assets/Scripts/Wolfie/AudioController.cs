using UnityEngine;

namespace Wolfie
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private MainController mainController;
        [SerializeField] private AudioSource biteSound;

        private void Update()
        {
            if (mainController.inputController.Biting)
            {
                biteSound.Play();
                mainController.inputController.Biting = false;
            }
        }
    }
}
