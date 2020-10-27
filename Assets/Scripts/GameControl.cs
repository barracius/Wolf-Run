using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance;

    [SerializeField] private GameObject restartButton;

    [SerializeField] private Text highScoreText;

    [SerializeField] private Text yourScoreText;

    [SerializeField] private GameObject[] obstacles;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float spawnRate = 2f;
    
    [SerializeField] private float timeToBoost = 5f;

    private float _nextBoost, _nextScoreIncrease,  _nextSpawn;

    private int _highScore, _yourScore;

    internal static bool GameStopped;

    private void Start()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
        restartButton.SetActive(false);
        _yourScore = 0;
        GameStopped = false;
        Time.timeScale = 1f;
        _highScore = PlayerPrefs.GetInt("highScore");
        _nextSpawn = Time.time + spawnRate;
        _nextBoost = Time.unscaledTime + timeToBoost;
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
        if (_yourScore > _highScore) PlayerPrefs.SetInt("highScore", _yourScore);
        Time.timeScale = 0;
        GameStopped = true;
        restartButton.SetActive(true);
    }

    private void SpawnObstacle()
    {
        _nextSpawn = Time.time + spawnRate;
        int randomObstacle = Random.Range(0, obstacles.Length);
        Instantiate(obstacles[randomObstacle], spawnPoint.position, Quaternion.identity);
    }

    private void BoostTime()
    {
        _nextBoost = Time.unscaledTime + timeToBoost;
        Time.timeScale += 0.25f;
    }

    private void IncreaseYourScore()
    {
        if (Time.unscaledTime > _nextScoreIncrease)
        {
            _yourScore += 1;
            _nextScoreIncrease = Time.unscaledTime + 1;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
