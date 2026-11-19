
using UnityEngine;

public enum GameState
{
    Playing,
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

    [Header("Game State")]
    public GameState CurrentState { get; private set; }

    private float elapsedTime = 0f;

    public float ElapsedTime => elapsedTime;

    private void Start()
    {
        CurrentState = GameState.Playing;
        elapsedTime = 0f;
    }

    private void Update()
    {
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

    public void HandleKeyCollected()
    {
        if (CurrentState != GameState.Playing)
            return;

        Debug.Log("Key berhasil diambil!");
    }

    public void HandleGameOver()
    {
        if (CurrentState != GameState.Playing)
            return;

        CurrentState = GameState.GameOver;

        StopPlayer();

        gameUIManager.ShowGameOver(GetFormattedTime());

        Debug.Log("Game Over! Waktu: " + GetFormattedTime());
    }

    public void HandleWin()
    {
        if (CurrentState != GameState.Playing)
            return;

        CurrentState = GameState.Win;

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
}
