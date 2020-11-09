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
    [SerializeField] private GameObject lossPanel, startPanel;
    [SerializeField] private Text highScoreText = null;
    [SerializeField] private Text yourScoreText = null;
    [SerializeField] private GameObject[] obstacles = null, powerUps = null;
    [SerializeField] private Transform spawnPoint = null;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float timeToBoost = 5f;
    [SerializeField] private int oneStarScore = 20;
    [SerializeField] private int twoStarScore = 50;
    [SerializeField] private int threeStarScore = 100;
    [SerializeField] private float clockPowerUpDuration = 20f;

    private float _nextBoost, _nextScoreIncrease,  _nextSpawn, _clockPowerUpTimePickedUp, _timeScalePrePowerUp;
    private bool _clockPowerUpActivated = false;
    private int _highScore, _yourScore;

    internal static bool GameStopped;
    
    public List<GameObject> obstaclesInScene;
    public int numberOfStage;
    
    private Transform _achievedLeftStar;
    private Transform _achievedMidStar;
    private Transform _achievedRightStar;


    private void Start()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
        lossPanel.SetActive(false);
        _yourScore = 0;
        GameStopped = true;
        Time.timeScale = 0;
        _highScore = PlayerPrefs.GetInt("level" + numberOfStage + "Score", _yourScore);
        _nextSpawn = Time.time + spawnRate;
        _nextBoost = Time.unscaledTime + timeToBoost;
        _achievedLeftStar = lossPanel.transform.Find("A Left Star");
        _achievedMidStar = lossPanel.transform.Find("A Mid Star");
        _achievedRightStar = lossPanel.transform.Find("A Right Star");
    }
    
    private void Update()
    {
        if (!GameStopped) IncreaseYourScore();
        highScoreText.text = "High Score: " + _highScore;
        yourScoreText.text = "Your Score: " + _yourScore;

        if (Time.time > _nextSpawn) SpawnObstacle();
        if (Time.unscaledTime > _nextBoost && !GameStopped && !_clockPowerUpActivated) BoostTime();
        if (_clockPowerUpActivated)
        {
            if ((Time.time - _clockPowerUpTimePickedUp) >= clockPowerUpDuration)
            {
                _clockPowerUpActivated = false;
                Time.timeScale = _timeScalePrePowerUp;
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
        lossPanel.SetActive(true);
        ShowCurrentStars();
    }

    private void SpawnObstacle()
    {
        _nextSpawn = Time.time + spawnRate;
        if (obstaclesInScene.Count != 0) return;
        var obstacleOrPowerUp = Random.Range(0, 10);
        int obstacleToSpawn = 0;
        Debug.Log(obstacleOrPowerUp);
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
    }

    public void OnStartButtonPressed()
    {
        GameStopped = false;
        Time.timeScale = 1f;
        startPanel.SetActive(false);
    }
}
