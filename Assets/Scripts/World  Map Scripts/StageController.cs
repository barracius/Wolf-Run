using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace World__Map_Scripts
{
    public class StageController : MonoBehaviour
    {
        [SerializeField] public int numberOfStage;
        private Transform _locked;
        private Transform _unlocked;
        private int _stars;
        private int _prevStageStars;
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
            _emptyMidStar = _unlocked.Find("E Mid Star");
            _emptyRightStar = _unlocked.Find("E Right Star");

            if (CheckIfUnlocked())
            {
                _locked.gameObject.SetActive(false);
                _unlocked.gameObject.SetActive(true);
                CalculateLevelStars();
                _isLocked = false;
            }
            else
            {
                _unlocked.gameObject.SetActive(false);
                _isLocked = true;
            }
        }

        private void CalculateLevelStars()
        {
            _stars = PlayerPrefs.GetInt("level" + numberOfStage + "Stars", 0);
            switch (_stars)
            {
                case 1:
                    _achievedLeftStar.gameObject.SetActive(true);
                    _emptyMidStar.gameObject.SetActive(true);
                    _emptyRightStar.gameObject.SetActive(true);
                    
                    _emptyLeftStar.gameObject.SetActive(false);
                    _achievedMidStar.gameObject.SetActive(false);
                    _achievedRightStar.gameObject.SetActive(false);
                    break;
                case 2:
                    _achievedLeftStar.gameObject.SetActive(true);
                    _achievedMidStar.gameObject.SetActive(true);
                    _emptyRightStar.gameObject.SetActive(true);
                    
                    _emptyLeftStar.gameObject.SetActive(false);
                    _emptyMidStar.gameObject.SetActive(false);
                    _achievedRightStar.gameObject.SetActive(false);
                    break;
                case 3:
                    _achievedLeftStar.gameObject.SetActive(true);
                    _achievedMidStar.gameObject.SetActive(true);
                    _achievedRightStar.gameObject.SetActive(true);
                    
                    _emptyLeftStar.gameObject.SetActive(false);
                    _emptyMidStar.gameObject.SetActive(false);
                    _emptyRightStar.gameObject.SetActive(false);
                    break;
            }
            
        }

        private bool CheckIfUnlocked()
        {
            if (numberOfStage == 1) return true;
            _prevStageStars = PlayerPrefs.GetInt("level" + (numberOfStage - 1) + "Stars", 0);
            return _prevStageStars != 0;
        }

        public void OnClickStage()
        {
            if (!_isLocked)
            {
                SceneManager.LoadScene(numberOfStage.ToString());
            }
        }
    }
}
