using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace World__Map_Scripts
{
    public class StageController : MonoBehaviour
    {
        [SerializeField] private string sceneToGo;
        [SerializeField] public int numberOfStage;
        private Transform _locked;
        private Transform _unlocked;
        private int _stars;
        private Transform _achievedLeftStar;
        private Transform _achievedMidStar;
        private Transform _achievedRightStar;
        private Transform _emptyLeftStar;
        private Transform _emptyMidStar;
        private Transform _emptyRightStar;
        private bool _isLocked;

        private void Start()
        {
            _locked = gameObject.transform.Find("Locked");
            _unlocked = gameObject.transform.Find("Unlocked");
            _achievedLeftStar = _unlocked.Find("A Left Star");
            _achievedMidStar = _unlocked.Find("A Mid Star");
            _achievedRightStar = _unlocked.Find("A Right Star");
            _emptyLeftStar = _unlocked.Find("E Left Star");
            _emptyMidStar = _unlocked.Find("E Left Star");
            _emptyRightStar = _unlocked.Find("E Left Star");
            _unlocked.gameObject.SetActive(false);
            
        }

        private void CalculateLevelStars()
        {
            _stars = PlayerPrefs.GetInt("level" + numberOfStage + "Stars");
            switch (_stars)
            {
                case 1:
                    _achievedLeftStar.gameObject.SetActive(true);
                    _emptyMidStar.gameObject.SetActive(true);
                    _emptyRightStar.gameObject.SetActive(true);
                    break;
                case 2:
                    _achievedLeftStar.gameObject.SetActive(true);
                    _achievedMidStar.gameObject.SetActive(true);
                    _emptyRightStar.gameObject.SetActive(true);
                    break;
                case 3:
                    _achievedLeftStar.gameObject.SetActive(true);
                    _achievedMidStar.gameObject.SetActive(true);
                    _achievedRightStar.gameObject.SetActive(true);
                    break;
            }
            
        }
    }
}
