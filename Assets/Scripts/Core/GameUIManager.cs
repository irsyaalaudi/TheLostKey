
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [Header("Core References")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LevelManager levelManager;

    [Header("UI Panels")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject pausePanel;

    [Header("Time Text")]
    [SerializeField] private TMP_Text winTimeText;
    [SerializeField] private TMP_Text gameOverTimeText;
    [SerializeField] private TMP_Text pauseTimeText;

    private void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (winPanel != null)
            winPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    public void ShowWin(string formattedTime)
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (winPanel != null)
            winPanel.SetActive(true);

        if (winTimeText != null)
            winTimeText.text = formattedTime;
    }

    public void ShowGameOver(string formattedTime)
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (winPanel != null)
            winPanel.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (gameOverTimeText != null)
            gameOverTimeText.text = formattedTime;
    }

    public void ShowPause(string formattedTime)
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);

        if (pauseTimeText != null)
            pauseTimeText.text = formattedTime;
    }

    public void HidePause()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    // Tombol Resume
    public void OnResumeButton()
    {
        if (gameManager != null)
            gameManager.ResumeGame();
    }

    // Tombol Reset Stage
    public void OnResetStageButton()
    {
        if (gameManager != null)
            gameManager.ResetStage();
    }

    // Tombol Next Stage
    public void OnNextStageButton()
    {
        Time.timeScale = 1f;

        if (levelManager != null)
            levelManager.LoadNextStage();
    }

    // Tombol Restart
    public void OnRestartButton()
    {
        Time.timeScale = 1f;

        if (levelManager != null)
            levelManager.RestartCurrentStage();
    }

    // Tombol Main Menu
    public void OnMainMenuButton()
    {
        Time.timeScale = 1f;

        if (levelManager != null)
            levelManager.LoadMainMenu();
    }
}
