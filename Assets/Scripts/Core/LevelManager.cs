
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField]
    private string[] levelNames =
    {
        "Stage1",
        "Stage2",
        "Stage3"
    };

    [SerializeField] private string mainMenuName = "MainMenu";

    // Memuat stage berdasarkan nomor urut (1, 2, atau 3)
    public void LoadStage(int stageNumber)
    {
        int index = stageNumber - 1;

        if (index < 0 || index >= levelNames.Length)
        {
            Debug.LogError("Nomor stage tidak valid: " + stageNumber);
            return;
        }

        LoadScene(levelNames[index]);
    }

    // Memuat stage berikutnya
    public void LoadNextStage()
    {
        int currentIndex = GetCurrentStageIndex();

        if (currentIndex < 0)
        {
            Debug.LogError("Scene saat ini tidak terdaftar sebagai stage!");
            return;
        }

        if (currentIndex + 1 >= levelNames.Length)
        {
            Debug.Log("Semua stage berhasil diselesaikan!");
            return;
        }

        LoadScene(levelNames[currentIndex + 1]);
    }

    // Mengulang stage yang sedang dimainkan
    public void RestartCurrentStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Kembali ke Main Menu
    public void LoadMainMenu()
    {
        LoadScene(mainMenuName);
    }

    private int GetCurrentStageIndex()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        for (int i = 0; i < levelNames.Length; i++)
        {
            if (levelNames[i] == currentScene)
            {
                return i;
            }
        }

        return -1;
    }

    private void LoadScene(string sceneName)
    {
        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError(
                "Scene tidak ditemukan atau belum dimasukkan ke Build Profiles: "
                + sceneName
            );
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}
