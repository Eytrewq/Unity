using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    private float spawnRate = 1.0f;
    private int score;
    private int lives;
    public bool isGameActive;
    public bool isGamePause;
    public Button restartButton;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
            QuitGame();
        if (isGameActive && Input.GetKeyDown("space") && isGamePause)
            resumeGame();
        else if (isGameActive && Input.GetKeyDown("space") && !isGamePause)
            pauseGame();
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToSub)
    {
        if (lives <= 0)
            return ;
        lives -= livesToSub;
        livesText.text = "Lives: " + lives;
        if (lives == 0)
            gameOver();
    }

    public void startGame(float difficulty)
    {
        titleScreen.gameObject.SetActive(false);
        isGameActive = true;
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        score = 0;
        lives = 3;
        UpdateScore(0);
        UpdateLives(0);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    void pauseGame()
    {
        isGamePause = true;
        pauseScreen.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    void resumeGame()
    {
        isGamePause = false;
        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
