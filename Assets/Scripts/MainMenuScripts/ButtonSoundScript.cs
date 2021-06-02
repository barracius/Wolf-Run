using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hoverSound;

    public void HoverSound()
    {
        audioSource.PlayOneShot(hoverSound);
    }
    public void ClickSound()
    {
        audioSource.PlayOneShot(hoverSound);
    }
}
