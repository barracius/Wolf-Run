﻿using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace StageScripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MainScript : MonoBehaviour
    {
        public static MainScript Instance;
        [SerializeField] private Canvas canvas = null;
        [SerializeField] private GameObject[] obstacles = null; 
//        [SerializeField] private GameObject[] powerUps = null;
        [SerializeField] private Transform spawnPoint = null;
        [SerializeField] private float spawnRate = 2f;
        [SerializeField] private float timeToBoost = 5f;
        [SerializeField] private int oneStarScore = 20;
        [SerializeField] private int twoStarScore = 50;
        [SerializeField] private int threeStarScore = 100;
        [SerializeField] private float clockPowerUpDuration = 20f;
        [SerializeField] private Transform wolfie = null;
        [SerializeField] private Sprite[] wolfieSprites = null;

        private float _nextBoost, _nextScoreIncrease,  _nextSpawn, _clockPowerUpTimePickedUp, _timeScalePrePowerUp, _clockPowerUpTimeLeft, _timeScalePrePause;
        private bool _clockPowerUpActivated = false;
        private int _highScore, _yourScore;
        private GameObject _lossPanel, _startPanel, _powerUpsPanel, _scorePanel, _oneStar1, _twoStar1, _twoStar2, _threeStar1, _threeStar2, _threeStar3;
        private Text _highScoreText, _yourScoreText, _shieldChargesText, _clockRemainingTimeText, _oneStarText, _twoStarText, _threeStarText;
        private Transform _achievedLeftStar, _achievedMidStar, _achievedRightStar, _shieldIcon, _clockIcon, _targetOneStar, _targetTwoStar, _targetThreeStar, _objectivePanel;
        private Wolfie.MainController _wolfieMainController;
        private SpriteRenderer _wolfieSpriteRenderer;

        private bool _gameStopped;
    
        public List<GameObject> obstaclesInScene;
        public int numberOfStage;

        public event Action PauseEvent;
        public event Action UnpauseEvent;



        private void Start()
        {
            Methods.DontPlayMainMenuMusic();
            Assignations();
            Pause();
            LoadSkins();
        }

        private void Assignations()
        {
            //Assign panels
            _lossPanel = canvas.transform.Find("LossPanel").gameObject;
            _startPanel = canvas.transform.Find("StartPanel").gameObject;
            _powerUpsPanel = canvas.transform.Find("PowerUpsPanel").gameObject;
            _scorePanel = canvas.transform.Find("ScorePanel").gameObject;

            //Assign lost panel values
            _achievedLeftStar = _lossPanel.transform.Find("A Left Star");
            _achievedMidStar = _lossPanel.transform.Find("A Mid Star");
            _achievedRightStar = _lossPanel.transform.Find("A Right Star");

            //Assign score panel values
            _highScoreText = _scorePanel.transform.Find("HighScoreText").GetComponent<Text>();
            _yourScoreText = _scorePanel.transform.Find("YourScoreText").GetComponent<Text>();
            _objectivePanel = _scorePanel.transform.Find("ObjectivePanel");

            _targetOneStar = _objectivePanel.transform.Find("OneStar");
            _oneStar1 = _targetOneStar.Find("A_Star_1").gameObject;
            _oneStar1.SetActive(false);
            _oneStarText = _targetOneStar.Find("TargetScoreText").GetComponent<Text>();
            _oneStarText.text = oneStarScore.ToString();

            _targetTwoStar = _objectivePanel.transform.Find("TwoStar");
            _twoStar1 = _targetTwoStar.Find("A_Star_1").gameObject;
            _twoStar1.SetActive(false);
            _twoStar2 = _targetTwoStar.Find("A_Star_2").gameObject;
            _twoStar2.SetActive(false);
            _twoStarText = _targetTwoStar.Find("TargetScoreText").GetComponent<Text>();
            _twoStarText.text = twoStarScore.ToString();

            _targetThreeStar = _objectivePanel.transform.Find("ThreeStar");
            _threeStar1 = _targetThreeStar.Find("A_Star_1").gameObject;
            _threeStar1.SetActive(false);
            _threeStar2 = _targetThreeStar.Find("A_Star_2").gameObject;
            _threeStar2.SetActive(false);
            _threeStar3 = _targetThreeStar.Find("A_Star_3").gameObject;
            _threeStar3.SetActive(false);
            _threeStarText = _targetThreeStar.Find("TargetScoreText").GetComponent<Text>();
            _threeStarText.text = threeStarScore.ToString();

            //Assign power ups panel values
            _shieldIcon = _powerUpsPanel.transform.Find("ShieldIcon");
            _shieldChargesText = _shieldIcon.Find("ShieldChargesText").GetComponent<Text>();
            _clockIcon = _powerUpsPanel.transform.Find("ClockIcon");
            _clockRemainingTimeText = _clockIcon.Find("ClockRemainingTimeText").GetComponent<Text>();

            if (Instance == null) Instance = this;
            else if (Instance != this) Destroy(gameObject);
            _lossPanel.SetActive(false);
            _shieldIcon.gameObject.SetActive(false);
            _clockIcon.gameObject.SetActive(false);
            _yourScore = 0;
            _highScore = PlayerPrefs.GetInt("level" + numberOfStage + "Score", _yourScore);
            _nextSpawn = Time.time + spawnRate;
            _nextBoost = Time.unscaledTime + timeToBoost;

            //Assign wolfies values
            _wolfieMainController = wolfie.GetComponent<Wolfie.MainController>();
            _wolfieSpriteRenderer = wolfie.GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (!_gameStopped) IncreaseScore();
            _highScoreText.text = "High Score: " + _highScore;
            _yourScoreText.text = "Your Score: " + _yourScore;

            if (Time.time > _nextSpawn) SpawnObstacle();
            if (Time.unscaledTime > _nextBoost && !_gameStopped && !_clockPowerUpActivated) BoostTime();
            CheckInGameStarsAchieved(_yourScore);
            Methods.CheckInputAndGoBackToMainMenu();
            Methods.CheckInputAndCloseGame();
        }

        private void CheckInGameStarsAchieved(int score)
        {
            if (score >= oneStarScore) _oneStar1.SetActive(true);
            if (score >= twoStarScore)
            {
                _twoStar1.SetActive(true);
                _twoStar2.SetActive(true);
            }
            if (score < threeStarScore) return;
            _threeStar1.SetActive(true);
            _threeStar2.SetActive(true);
            _threeStar3.SetActive(true);
        }

        public void GameOver()
        {
            if (_yourScore > _highScore) PlayerPrefs.SetInt("level" + numberOfStage + "Score", _yourScore);
            AdjustTotalStars();
            Time.timeScale = 0;
            Pause();
            _lossPanel.SetActive(true);
            ShowCurrentStars();
        }

        private void SpawnObstacle()
        {
            _nextSpawn = Time.time + spawnRate;
            if (obstaclesInScene.Count != 0) return;
//            var obstacleOrPowerUp = Random.Range(0, 20);
//            int obstacleToSpawn;
//            if (obstacleOrPowerUp == 0)
//            {
//                obstacleToSpawn = Random.Range(0, powerUps.Length);
//                var obstacle = powerUps[obstacleToSpawn];
//                obstaclesInScene.Add(Instantiate(obstacle, spawnPoint.position, Quaternion.identity));
//            }
//            else
//            {
//                obstacleToSpawn = Random.Range(0, obstacles.Length);
//                var obstacle = obstacles[obstacleToSpawn];
//                obstaclesInScene.Add(Instantiate(obstacle, spawnPoint.position, Quaternion.identity));
//            }
                var obstacleToSpawn = Random.Range(0, obstacles.Length);
                var obstacle = obstacles[obstacleToSpawn];
                obstaclesInScene.Add(Instantiate(obstacle, spawnPoint.position, Quaternion.identity));
        }

        private void BoostTime()
        {
            _nextBoost = Time.unscaledTime + timeToBoost;
            Time.timeScale += 0.1f;
        }

        private void IncreaseScore()
        {
            if (!(Time.unscaledTime > _nextScoreIncrease)) return;
            _yourScore += (int)Time.timeSinceLevelLoad;
            _nextScoreIncrease = Time.unscaledTime + 1;
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(numberOfStage.ToString());
        }

        public void OnCrossPressed()
        {
            SceneManager.LoadScene("LevelSelection");
        }

        private void AdjustTotalStars()
        {
            switch (PlayerPrefs.GetInt("level" + numberOfStage + "Stars", 0))
            {
                case 3:
                    break;
                case 2:
                    if (_yourScore >= threeStarScore)
                    {
                        PlayerPrefs.SetInt("level" + numberOfStage + "Stars", 3);
                    }
                    break;
                case 1:
                    if (_yourScore >= threeStarScore)
                    {
                        PlayerPrefs.SetInt("level" + numberOfStage + "Stars", 3);
                    }
                    else if (_yourScore >= twoStarScore)
                    {
                        PlayerPrefs.SetInt("level" + numberOfStage + "Stars", 2);
                    }
                    break;
                case 0:
                    if (_yourScore >= threeStarScore)
                    {
                        PlayerPrefs.SetInt("level" + numberOfStage + "Stars", 3);
                    }
                    else if (_yourScore >= twoStarScore)
                    {
                        PlayerPrefs.SetInt("level" + numberOfStage + "Stars", 2);
                    }
                    else if (_yourScore >= oneStarScore)
                    {
                        PlayerPrefs.SetInt("level" + numberOfStage + "Stars", 1);
                    }
                    break;
            }
        }

        private void ShowCurrentStars()
        {
            _achievedLeftStar.gameObject.SetActive(false);
            _achievedMidStar.gameObject.SetActive(false);
            _achievedRightStar.gameObject.SetActive(false);
            if (_yourScore >= oneStarScore) _achievedLeftStar.gameObject.SetActive(true);
            if (_yourScore >= twoStarScore) _achievedMidStar.gameObject.SetActive(true);
            if (_yourScore >= threeStarScore) _achievedRightStar.gameObject.SetActive(true);
        }

        public void OnStartButtonPressed()
        {
            Unpause();
            Time.timeScale = 1f;
            _startPanel.SetActive(false);
        }

        internal void UpdateShieldPowerUpState(int shieldCharges)
        {
            switch (shieldCharges)
            {
                case 0:
                    _shieldIcon.gameObject.SetActive(false);
                    _wolfieMainController.Barrier.SetActive(false);
                    break;
                case 1:
                    _shieldIcon.gameObject.SetActive(true);
                    Color whiteBizarre = new Color(1,1,1, 0.5f);
                    _wolfieMainController.Barrier.SetActive(true);
                    _wolfieMainController.SrBarrier.color = whiteBizarre;
                    _shieldChargesText.text = "1";
                    break;
                case 2:
                    _shieldChargesText.text = "2";
                    Color redBizarre = new Color(1,1,1, 0.7f);
                    _wolfieMainController.SrBarrier.color = redBizarre;
                    break;
                case 3:
                    _shieldChargesText.text = "3";
                    Color purpleBizarre = new Color(1,1,1, 0.9f);
                    _wolfieMainController.SrBarrier.color = purpleBizarre;
                    break;
            }
        }

        private void LoadSkins()
        {
            //Get current skins through player prefs
            int wolfBody = PlayerPrefs.GetInt("Wolf Body Skin", 0);
        
            //Set current skins
            _wolfieSpriteRenderer.sprite = wolfieSprites[wolfBody];
        }
        
        //CoRoutine that handles when Wolfie grabs Clock Power Up.
        internal IEnumerator ClockPowerUpActivate()
        {
            _timeScalePrePowerUp = Time.timeScale;
            _clockIcon.gameObject.SetActive(true);
            if (_clockPowerUpActivated)
            {
                _clockPowerUpTimeLeft += clockPowerUpDuration;
            }
            else
            {
                _clockPowerUpActivated = true;
                for (_clockPowerUpTimeLeft = clockPowerUpDuration; _clockPowerUpTimeLeft > 0; _clockPowerUpTimeLeft -= Time.deltaTime)
                {
                    Time.timeScale = 1;
                    int clockPowerTimeLeftInt = (int) _clockPowerUpTimeLeft;
                    _clockRemainingTimeText.text = clockPowerTimeLeftInt.ToString();
                    yield return null;
                }
                Time.timeScale = _timeScalePrePowerUp;
                _clockIcon.gameObject.SetActive(false);
                _clockPowerUpActivated = false;
            }
        }

        private void Pause()
        {
            _timeScalePrePause = Time.timeScale;
            _gameStopped = true;
            Time.timeScale = 0;
            PauseEvent?.Invoke();
        }

        private void Unpause()
        {
            _gameStopped = false;
            Time.timeScale = _timeScalePrePause;
            UnpauseEvent?.Invoke();
        }
    }
}
