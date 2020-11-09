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

    [SerializeField] private GameObject panel;

    [SerializeField] private Text highScoreText;

    [SerializeField] private Text yourScoreText;
    
    [SerializeField] private GameObject[] obstacles;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float spawnRate = 2f;
    
    [SerializeField] private float timeToBoost = 5f;
    [SerializeField] private const int OneStarScore = 20;
    [SerializeField] private const int TwoStarScore = 40;
    [SerializeField] private const int ThreeStarScore = 60;

    private float _nextBoost, _nextScoreIncrease,  _nextSpawn;

    private int _highScore, _yourScore;

    internal static bool GameStopped;
    
    public List<GameObject> obstaclesInScene;
    public int numberOfStage;
    
    private Transform _achievedLeftStar;
    private Transform _achievedMidStar;
    private Transform _achievedRightStar;
    private Transform _emptyLeftStar;
    private Transform _emptyMidStar;
    private Transform _emptyRightStar;
    

    private void Start()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
        panel.SetActive(false);
        _yourScore = 0;
        GameStopped = false;
        Time.timeScale = 1f;
        // RESET HIGH SCORE
        //PlayerPrefs.SetInt("highScore", 0);
        _highScore = PlayerPrefs.GetInt("level" + numberOfStage + "Score", _yourScore);
        _nextSpawn = Time.time + spawnRate;
        _nextBoost = Time.unscaledTime + timeToBoost;
        
        _achievedLeftStar = panel.transform.Find("A Left Star");
        _achievedMidStar = panel.transform.Find("A Mid Star");
        _achievedRightStar = panel.transform.Find("A Right Star");
        _emptyLeftStar = panel.transform.Find("E Left Star");
        _emptyMidStar = panel.transform.Find("E Mid Star");
        _emptyRightStar = panel.transform.Find("E Right Star");
    }
    
    private void Update()
    {
        if (!GameStopped) IncreaseYourScore();
        highScoreText.text = "High Score: " + _highScore;
        yourScoreText.text = "Your Score: " + _yourScore;

        if (Time.time > _nextSpawn) SpawnObstacle();
        if (Time.unscaledTime > _nextBoost && !GameStopped) BoostTime();
    }

    public void Loss()
    {
        if (_yourScore > _highScore) PlayerPrefs.SetInt("level" + numberOfStage + "Score", _yourScore);
        AdjustTotalStars();
        
        Time.timeScale = 0;
        GameStopped = true;
        panel.SetActive(true);
        ShowCurrentStars();
    }

    private void SpawnObstacle()
    {
        _nextSpawn = Time.time + spawnRate;
        if (obstaclesInScene.Count != 0) return;
        var randomObstacle = Random.Range(0, obstacles.Length);
        var obstacle = obstacles[randomObstacle];
        obstaclesInScene.Add(Instantiate(obstacle, spawnPoint.position, Quaternion.identity));
    }

    private void BoostTime()
    {
        _nextBoost = Time.unscaledTime + timeToBoost;
        Time.timeScale += 0.25f;
    }

    private void IncreaseYourScore()
    {
        if (!(Time.unscaledTime > _nextScoreIncrease)) return;
        _yourScore += 1;
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
                if (_yourScore >= ThreeStarScore)
                {
                    PlayerPrefs.SetInt("level" + numberOfStage + "Stars", 3);
                }
                break;
            case 1:
                if (_yourScore >= ThreeStarScore)
                {
                    PlayerPrefs.SetInt("level" + numberOfStage + "Stars", 3);
                }
                else if (_yourScore >= TwoStarScore)
                {
                    PlayerPrefs.SetInt("level" + numberOfStage + "Stars", 2);
                }
                break;
            case 0:
                if (_yourScore >= ThreeStarScore)
                {
                    PlayerPrefs.SetInt("level" + numberOfStage + "Stars", 3);
                }
                else if (_yourScore >= TwoStarScore)
                {
                    PlayerPrefs.SetInt("level" + numberOfStage + "Stars", 2);
                }
                else if (_yourScore >= OneStarScore)
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
        if (_yourScore >= OneStarScore) _achievedLeftStar.gameObject.SetActive(true);
        if (_yourScore >= TwoStarScore) _achievedMidStar.gameObject.SetActive(true);
        if (_yourScore >= ThreeStarScore) _achievedRightStar.gameObject.SetActive(true);
    }
}
