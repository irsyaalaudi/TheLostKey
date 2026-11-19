
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

    [Header("Time Text")]
    [SerializeField] private TMP_Text winTimeText;
    [SerializeField] private TMP_Text gameOverTimeText;

    private void Start()
    {
        // Sembunyikan kedua panel saat permainan dimulai
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (winPanel != null)
            winPanel.SetActive(false);
    }

    public void ShowWin(string formattedTime)
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (winPanel != null)
            winPanel.SetActive(true);

        if (winTimeText != null)
            winTimeText.text = formattedTime;
    }

    public void ShowGameOver(string formattedTime)
    {
        if (winPanel != null)
            winPanel.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (gameOverTimeText != null)
            gameOverTimeText.text = formattedTime;
    }

    // Tombol Next Stage
    public void OnNextStageButton()
    {
        if (levelManager != null)
            levelManager.LoadNextStage();
    }

    // Tombol Restart
    public void OnRestartButton()
    {
        if (levelManager != null)
            levelManager.RestartCurrentStage();
    }

    // Tombol Main Menu
    public void OnMainMenuButton()
    {
        if (levelManager != null)
            levelManager.LoadMainMenu();
    }
}
