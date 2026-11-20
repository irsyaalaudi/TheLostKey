
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
    [SerializeField] private GameObject hudPanel;

    [Header("HUD")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text gameplayTimeText;
    [SerializeField] private TMP_Text keyCountText;

    [Header("Time Text")]
    [SerializeField] private TMP_Text winTimeText;
    [SerializeField] private TMP_Text gameOverTimeText;
    [SerializeField] private TMP_Text pauseTimeText;

    private void Start()
    {
        // Sembunyikan semua panel saat permainan dimulai
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (winPanel != null)
            winPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);

        // Tampilkan HUD saat gameplay dimulai
        SetHUDVisible(true);

        UpdateHUD();
    }

    private void Update()
    {
        // Perbarui HUD hanya saat permainan berlangsung
        if (gameManager != null &&
            gameManager.CurrentState == GameState.Playing)
        {
            UpdateHUD();
        }
    }

    // Mengatur tampilan HUD
    private void SetHUDVisible(bool visible)
    {
        if (hudPanel != null)
            hudPanel.SetActive(visible);
    }

    // Memperbarui informasi HUD
    public void UpdateHUD()
    {
        if (gameManager == null)
            return;

        // Menampilkan Health
        if (healthText != null)
        {
            string hearts = "";

            for (int i = 0; i < 3; i++)
            {
                hearts += i < gameManager.CurrentHealth ? "♥ " : "♡ ";
            }

            healthText.text = hearts.TrimEnd();
        }

        // Menampilkan Timer Gameplay
        if (gameplayTimeText != null)
            gameplayTimeText.text = gameManager.GetFormattedTime();

        // Menampilkan jumlah Key
        if (keyCountText != null)
            keyCountText.text = gameManager.CurrentKeys + "/1";
    }

    // Menampilkan Win Panel
    public void ShowWin(string formattedTime)
    {
        SetHUDVisible(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (winPanel != null)
            winPanel.SetActive(true);

        if (winTimeText != null)
            winTimeText.text = formattedTime;
    }

    // Menampilkan Game Over Panel
    public void ShowGameOver(string formattedTime)
    {
        SetHUDVisible(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (winPanel != null)
            winPanel.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (gameOverTimeText != null)
            gameOverTimeText.text = formattedTime;
    }

    // Menampilkan Pause Panel
    public void ShowPause(string formattedTime)
    {
        SetHUDVisible(false);

        if (pausePanel != null)
            pausePanel.SetActive(true);

        if (pauseTimeText != null)
            pauseTimeText.text = formattedTime;
    }

    // Menyembunyikan Pause Panel dan melanjutkan HUD
    public void HidePause()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        SetHUDVisible(true);

        UpdateHUD();
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
