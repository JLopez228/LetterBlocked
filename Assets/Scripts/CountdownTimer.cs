using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class CountdownTimer : MonoBehaviour
{
    public float startTimeInSeconds = 150f; // 2:30
    public TextMeshProUGUI timerText;
    public GameObject gameOverPanel; // UI panel for Game Over

    private float currentTime;
    private bool isGameOver = false;

    void Start()
    {
        currentTime = startTimeInSeconds;
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (isGameOver) return;

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            currentTime = 0;
            UpdateTimerDisplay();
            TriggerGameOver();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = $"{minutes:D2}:{seconds:D2}";
    }

    void TriggerGameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over!");
        
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

       // Pause the game
        Time.timeScale = 0f;
    }
}