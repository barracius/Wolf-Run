using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl instance = null;

    [SerializeField] private GameObject restartButton;

    [SerializeField] private Text highScoreText;

    [SerializeField] private Text yourScoreText;

    [SerializeField] private GameObject[] obstacles;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float spawnRate = 2f;

    private float nextSpawn;

    [SerializeField] private float timeToBoost = 5f;

    private float nextBoost;

    private int highScore = 0, yourScore = 0;

    public static bool gameStopped;

    private float nextScoreIncrease = 0f;
    
    private void Start()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        restartButton.SetActive(false);
        yourScore = 0;
        gameStopped = false;
        Time.timeScale = 1f;
        highScore = PlayerPrefs.GetInt("highScore");
        nextSpawn = Time.time + spawnRate;
        nextBoost = Time.unscaledTime + timeToBoost;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!gameStopped) IncreaseYourScore();
        highScoreText.text = "High Score: " + highScore;
        yourScoreText.text = "Your Score: " + yourScore;

        if (Time.time > nextSpawn) SpawnObstacle();
        if (Time.unscaledTime > nextBoost && !gameStopped) BoostTime();
    }

    public void Loss()
    {
        if (yourScore > highScore) PlayerPrefs.SetInt("highScore", yourScore);
        Time.timeScale = 0;
        gameStopped = true;
        restartButton.SetActive(true);
    }

    void SpawnObstacle()
    {
        nextSpawn = Time.time + spawnRate;
        int randomObstacle = Random.Range(0, obstacles.Length);
        Instantiate(obstacles[randomObstacle], spawnPoint.position, Quaternion.identity);
    }

    void BoostTime()
    {
        nextBoost = Time.unscaledTime + timeToBoost;
        Time.timeScale += 0.25f;
    }

    void IncreaseYourScore()
    {
        if (Time.unscaledTime > nextScoreIncrease)
        {
            yourScore += 1;
            nextScoreIncrease = Time.unscaledTime + 1;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
