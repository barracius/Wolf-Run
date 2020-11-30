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
        public int amountOfStages;
        
        void Start()
        {
            _counter = 0;
            _textComponent = GetComponent<Text>();
            for (int i = 1; i <= amountOfStages; i++)
            {
                _stars = PlayerPrefs.GetInt("level" + i + "Stars", 0);
                _counter += _stars;
            }

            _textComponent.text = _counter + "/" + amountOfStages*3;
        }
    }
}
