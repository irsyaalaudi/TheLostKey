
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string stage1SceneName = "Stage1";

    [Header("UI References")]
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        // Memastikan panel Settings tertutup saat Main Menu dibuka
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    // Tombol START
    public void StartGame()
    {
        SceneManager.LoadScene(stage1SceneName);
    }

    // Tombol SETTING
    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }

    // Tombol CLOSE SETTINGS
    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    // Tombol EXIT
    public void ExitGame()
    {
        Debug.Log("Keluar dari permainan...");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
