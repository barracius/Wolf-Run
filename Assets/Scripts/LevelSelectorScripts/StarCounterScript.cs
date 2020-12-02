using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LevelSelectorScripts
{
    public class StarCounterScript : MonoBehaviour
    {
        private Text _textComponent;
        private int _stars;
        private int _counter;

        void Start()
        {
            _textComponent = GetComponent<Text>();
            _counter = Helpers.Methods.CalculateTotalStars();
            _textComponent.text = _counter + "/" + (int) Helpers.GobalVariables.NumberOfStages * 3;
        }
    }
}
