using System;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenuScripts
{
    public class BackgroundImageScript : MonoBehaviour
    {
        [SerializeField] private Sprite[] backgrounds = null;
        private int _pos;

        internal void SetBackground()
        {
            _pos = PlayerPrefs.GetInt("MainMenu Background Skin",0);
            GetComponent<Image>().sprite = backgrounds[_pos];
        }
    }
}
