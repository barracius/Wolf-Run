using System;
using Helpers;
using UnityEngine;

namespace SettingsScripts
{
    public class MainScript : MonoBehaviour
    {
        public GameObject muteButton;
        public GameObject unMuteButton;
        private MuteSetting _muteSetting;
        private void Start()
        {
            Methods.PlayMainMenuMusic();

            _muteSetting = PlayerPrefs.GetInt("Muted", 0) == 0 ? MuteSetting.UnMuted : MuteSetting.Muted;

            switch (_muteSetting)
            {
                case MuteSetting.UnMuted:
                    muteButton.SetActive(true);
                    unMuteButton.SetActive(false);
                    break;
                case MuteSetting.Muted:
                    muteButton.SetActive(false);
                    unMuteButton.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    
        public void GoBackButtonPressed()
        {
            Methods.GoBackToMainMenu();
        }

        private void Update()
        {
            Methods.CheckInputAndGoBackToMainMenu();
            Methods.CheckInputAndCloseGame();
        }
        
        public void MuteAllSound()
        {
            AudioListener.volume = 0;
            muteButton.SetActive(false);
            unMuteButton.SetActive(true);
            PlayerPrefs.SetInt("Muted", 1);
        }

        public void UnMuteAllSound()
        {
            AudioListener.volume = 1;
            muteButton.SetActive(true);
            unMuteButton.SetActive(false);
            PlayerPrefs.SetInt("Muted", 0);
        }
        
        
    }
}
