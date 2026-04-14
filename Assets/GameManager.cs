using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject gameHUDPanel;
    public GameObject gameOverPanel;

    [Header("Text Displays")]
    public TextMeshProUGUI scoreText;      // For the HUD during play
    public TextMeshProUGUI timerText;      // For the HUD during play
    public TextMeshProUGUI finalScoreText; // For the Game Over screen

    [Header("Game Settings")]
    public float gameDuration = 60f; // Seconds per round

    private float currentTime;
    private bool isGameActive = false;

    void Start()
    {
        // Start the game at the Main Menu
        ShowMainMenu();
    }

    void Update()
    {
        if (isGameActive)
        {
            UpdateTimer();
        }
    }

    public void ShowMainMenu()
    {
        isGameActive = false;
        Time.timeScale = 0f; // Pause physics
        mainMenuPanel.SetActive(true);
        gameHUDPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void StartGame()
    {
        // Reset Score and Timer
        TargetHit.totalScore = 0;
        currentTime = gameDuration;

        isGameActive = true;
        Time.timeScale = 1f; // Resume physics

        mainMenuPanel.SetActive(false);
        gameHUDPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    void UpdateTimer()
    {
        currentTime -= Time.deltaTime;

        // Update the HUD display
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.CeilToInt(currentTime).ToString();
        }

        // Update the HUD score
        if (scoreText != null)
        {
            scoreText.text = "Score: " + TargetHit.totalScore.ToString();
        }

        if (currentTime <= 0)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        isGameActive = false;
        Time.timeScale = 0f; // Pause physics

        mainMenuPanel.SetActive(false);
        gameHUDPanel.SetActive(false);
        gameOverPanel.SetActive(true);

        if (finalScoreText != null)
        {
            finalScoreText.text = "Final Score: " + TargetHit.totalScore.ToString();
        }
    }
}