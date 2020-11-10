using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance;
    [SerializeField] private Canvas canvas;
    
    
    [SerializeField] private GameObject[] obstacles = null, powerUps = null;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float timeToBoost = 5f;
    [SerializeField] private int oneStarScore = 20;
    [SerializeField] private int twoStarScore = 50;
    [SerializeField] private int threeStarScore = 100;
    [SerializeField] private float clockPowerUpDuration = 20f;

    private float _nextBoost, _nextScoreIncrease,  _nextSpawn, _clockPowerUpTimePickedUp, _timeScalePrePowerUp;
    private bool _clockPowerUpActivated = false;
    private int _highScore, _yourScore;
    private GameObject _lossPanel, _startPanel, _powerUpsPanel, _scorePanel;
    private Text _highScoreText, _yourScoreText, _shieldChargesText;
    private Transform _achievedLeftStar, _achievedMidStar, _achievedRightStar, _shieldIcon, _clockIcon;

    internal static bool GameStopped;
    
    public List<GameObject> obstaclesInScene;
    public int numberOfStage;
    
    


    private void Start()
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
        
        //Assign power ups panel values
        _shieldIcon = _powerUpsPanel.transform.Find("ShieldIcon");
        _shieldChargesText = _shieldIcon.Find("ShieldChargesText").GetComponent<Text>();
        _clockIcon = _powerUpsPanel.transform.Find("ClockIcon");


        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
        _lossPanel.SetActive(false);
        _shieldIcon.gameObject.SetActive(false);
        _clockIcon.gameObject.SetActive(false);
        _yourScore = 0;
        GameStopped = true;
        Time.timeScale = 0;
        _highScore = PlayerPrefs.GetInt("level" + numberOfStage + "Score", _yourScore);
        _nextSpawn = Time.time + spawnRate;
        _nextBoost = Time.unscaledTime + timeToBoost;
        
    }
    
    private void Update()
    {
        if (!GameStopped) IncreaseYourScore();
        _highScoreText.text = "High Score: " + _highScore;
        _yourScoreText.text = "Your Score: " + _yourScore;

        if (Time.time > _nextSpawn) SpawnObstacle();
        if (Time.unscaledTime > _nextBoost && !GameStopped && !_clockPowerUpActivated) BoostTime();
        if (_clockPowerUpActivated)
        {
            if ((Time.time - _clockPowerUpTimePickedUp) >= clockPowerUpDuration)
            {
                _clockPowerUpActivated = false;
                Time.timeScale = _timeScalePrePowerUp;
                _clockIcon.gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    public void Loss()
    {
        if (_yourScore > _highScore) PlayerPrefs.SetInt("level" + numberOfStage + "Score", _yourScore);
        AdjustTotalStars();
        Time.timeScale = 0;
        GameStopped = true;
        _lossPanel.SetActive(true);
        ShowCurrentStars();
    }

    private void SpawnObstacle()
    {
        _nextSpawn = Time.time + spawnRate;
        if (obstaclesInScene.Count != 0) return;
        var obstacleOrPowerUp = Random.Range(0, 10);
        int obstacleToSpawn;
        if (obstacleOrPowerUp == 0)
        {
            obstacleToSpawn = Random.Range(0, powerUps.Length);
            var obstacle = powerUps[obstacleToSpawn];
            obstaclesInScene.Add(Instantiate(obstacle, spawnPoint.position, Quaternion.identity));
        }
        else
        {
            obstacleToSpawn = Random.Range(0, obstacles.Length);
            var obstacle = obstacles[obstacleToSpawn];
            obstaclesInScene.Add(Instantiate(obstacle, spawnPoint.position, Quaternion.identity));
        }
    }

    private void BoostTime()
    {
        _nextBoost = Time.unscaledTime + timeToBoost;
        Time.timeScale += 0.1f;
        _timeScalePrePowerUp = Time.timeScale;
    }

    private void IncreaseYourScore()
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
        SceneManager.LoadScene("WorldMap");
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

    internal void ClockPowerUpActivation()
    {
        _clockPowerUpActivated = true;
        _clockPowerUpTimePickedUp = Time.time;
        _clockIcon.gameObject.SetActive(true);
    }

    public void OnStartButtonPressed()
    {
        GameStopped = false;
        Time.timeScale = 1f;
        _startPanel.SetActive(false);
    }

    internal void ShieldPowerUpUI(int shieldCharges)
    {
        switch (shieldCharges)
        {
            case 0:
                _shieldIcon.gameObject.SetActive(false);
                break;
            case 1:
                _shieldIcon.gameObject.SetActive(true);
                _shieldChargesText.text = "1";
                break;
            case 2:
                _shieldChargesText.text = "2";
                break;
            case 3:
                _shieldChargesText.text = "3";
                break;
        }
    }
}
