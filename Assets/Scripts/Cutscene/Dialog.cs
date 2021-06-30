using System;
using System.Collections;
using UnityEngine;
using TMPro;

namespace Cutscene
{
    public class Dialog : MonoBehaviour
    {
        public TextMeshProUGUI textDisplay;
        public string[] sentences;
        private int index;
        public float typingSpeed;
        public float inBetweenSentencesTime;
        public GameObject[] floatingHeads;
        public AudioClip[] thisSounds;
        public AudioSource thisAudioSource;
        public AudioSource rightGuyAudioSource;

        private void OnEnable()
        {
            textDisplay.text = "";
            StartCoroutine(Type());
        }

        private void Update()
        {
            if (index >= sentences.Length)
            {
                return;
            }
            if (textDisplay.text == sentences[index])
            {
                index++;
                Invoke(nameof(NextSentence), inBetweenSentencesTime);
            }
        }

        IEnumerator Type()
        {
            foreach (var letter in sentences[index].ToCharArray())
            {
                textDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        public void NextSentence()
        {
            if (index < sentences.Length)
            {
                textDisplay.text = "";
                StartCoroutine(Type());
                EnableAndDisableHeads(index);
            }
            else
            {
                textDisplay.text = "";
            }
            
        }

        public void EnableAndDisableHeads(int turnToSpeak)
        {
            switch (turnToSpeak)
            {
                case 1:
                    floatingHeads[0].SetActive(false);
                    floatingHeads[1].SetActive(true);
                    break;
                case 2:
                    floatingHeads[0].SetActive(true);
                    floatingHeads[1].SetActive(false);
                    floatingHeads[2].SetActive(true);
                    break;
                default:
                    Debug.Log("Invalid turn to speak, check the index");
                    foreach (var floatingHead in floatingHeads)
                    {
                        floatingHead.SetActive(false);
                    }
                    break;
            }
            PlaySound(turnToSpeak);
        }

        private void PlaySound(int turnToSpeak)
        {
            switch (turnToSpeak)
            {
                case 1:
                    thisAudioSource.clip = thisSounds[0];
                    thisAudioSource.volume = 0.9f;
                    thisAudioSource.Play();
                    break;
                case 2:
                    thisAudioSource.clip = thisSounds[1];
                    thisAudioSource.volume = 0.35f;
                    thisAudioSource.Play();
                    rightGuyAudioSource.Play();
                    break;
                default:
                    Debug.Log("Invalid audio, check the index");
                    break;
            }
        }
    }
}
