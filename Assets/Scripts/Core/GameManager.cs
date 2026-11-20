
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState
{
    Playing,
    Paused,
    GameOver,
    Win
}

public class GameManager : MonoBehaviour
{
    [Header("Player References")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Rigidbody2D playerRb;

    [Header("Core References")]
    [SerializeField] private GameUIManager gameUIManager;
    [SerializeField] private LevelManager levelManager;

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 3;

    public int CurrentHealth { get; private set; }

    public event Action<int, int> OnHealthChanged;

    [Header("Game State")]
    public GameState CurrentState { get; private set; }

    private float elapsedTime = 0f;

    public float ElapsedTime => elapsedTime;

    private void Start()
    {
        Time.timeScale = 1f;

        CurrentState = GameState.Playing;
        elapsedTime = 0f;

        CurrentHealth = maxHealth;
        CurrentKeys = 0;
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        // ESC untuk Pause dan Resume
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (CurrentState == GameState.Playing)
                PauseGame();

            else if (CurrentState == GameState.Paused)
                ResumeGame();
        }

        // R untuk Reset Stage
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            if (CurrentState == GameState.Playing ||
                CurrentState == GameState.Paused)
            {
                ResetStage();
            }
        }

        // Timer hanya berjalan ketika Playing
        if (CurrentState == GameState.Playing)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        return $"{minutes:00}:{seconds:00}";
    }

    // HEALTH SYSTEM
    public void TakeDamage(int damage)
    {
        if (CurrentState != GameState.Playing)
            return;

        CurrentHealth -= damage;

        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

        Debug.Log("Player terkena rintangan! Health: " +
                  CurrentHealth + "/" + maxHealth);

        if (CurrentHealth <= 0)
        {
            HandleGameOver();
        }
    }

    public void PauseGame()
    {
        if (CurrentState != GameState.Playing)
            return;

        CurrentState = GameState.Paused;

        if (playerMovement != null)
            playerMovement.enabled = false;

        if (playerRb != null)
            playerRb.linearVelocity = Vector2.zero;

        Time.timeScale = 0f;

        if (gameUIManager != null)
            gameUIManager.ShowPause(GetFormattedTime());

        Debug.Log("Game Paused!");
    }

    public void ResumeGame()
    {
        if (CurrentState != GameState.Paused)
            return;

        Time.timeScale = 1f;

        CurrentState = GameState.Playing;

        if (playerMovement != null)
            playerMovement.enabled = true;

        if (gameUIManager != null)
            gameUIManager.HidePause();

        Debug.Log("Game Resumed!");
    }

    public void ResetStage()
    {
        if (CurrentState == GameState.GameOver ||
            CurrentState == GameState.Win)
            return;

        Time.timeScale = 1f;

        if (levelManager != null)
            levelManager.RestartCurrentStage();
    }

    public void HandleKeyCollected()
    {
        if (CurrentState != GameState.Playing)
            return;

        if (CurrentKeys >= 1)
            return;

        CurrentKeys++;

        Debug.Log("Key berhasil diambil! Key: " + CurrentKeys + "/1");

        if (gameUIManager != null)
            gameUIManager.UpdateHUD();
    }

    public void HandleGameOver()
    {
        if (CurrentState != GameState.Playing)
            return;

        CurrentState = GameState.GameOver;

        Time.timeScale = 1f;

        StopPlayer();

        gameUIManager.ShowGameOver(GetFormattedTime());

        Debug.Log("Game Over! Waktu: " + GetFormattedTime());
    }

    public void HandleWin()
    {
        if (CurrentState != GameState.Playing)
            return;

        CurrentState = GameState.Win;

        Time.timeScale = 1f;

        StopPlayer();

        gameUIManager.ShowWin(GetFormattedTime());

        Debug.Log("Stage Completed! Waktu: " + GetFormattedTime());
    }

    private void StopPlayer()
    {
        if (playerMovement != null)
            playerMovement.enabled = false;

        if (playerRb != null)
            playerRb.linearVelocity = Vector2.zero;
    }

    public int CurrentKeys { get; private set; }
}
