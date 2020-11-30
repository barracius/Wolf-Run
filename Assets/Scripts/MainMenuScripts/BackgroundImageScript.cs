using System;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenuScripts
{
    public class BackgroundImageScript : MonoBehaviour
    {
        [SerializeField] private Sprite[] backgrounds;
        private Image _image;
        private int _pos;
        private void Start()
        {
            _image = GetComponent<Image>();
            SetBackground();
        }

        internal void SetBackground()
        {
            _pos = PlayerPrefs.GetInt("MainMenu Background Skin",0);
            _image.sprite = backgrounds[_pos];
        }
    }
}
