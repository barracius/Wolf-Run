using System;
using UnityEngine;

namespace MainMenuScripts
{
    public class SkinButtonScript : MonoBehaviour
    {
        internal Transform SelectionUi;
        public int skinNumber;

        private void Start()
        {
            SelectionUi = transform.Find("SelectionUI");
            SelectionUi.gameObject.SetActive(false);
        }
    }
}
