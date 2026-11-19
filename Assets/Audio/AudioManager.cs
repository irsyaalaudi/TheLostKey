
using UnityEngine;
using TMPro;

public class MusicSettings : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text volumeText;

    [Header("Volume Settings")]
    [SerializeField] private int volume = 70;
    [SerializeField] private int volumeStep = 10;

    private const string VolumeKey = "MusicVolume";

    private void Start()
    {
        // Mengambil volume yang pernah disimpan
        volume = PlayerPrefs.GetInt(VolumeKey, 70);

        ApplyVolume();
    }

    public void IncreaseVolume()
    {
        volume += volumeStep;

        if (volume > 100)
            volume = 100;

        ApplyVolume();
    }

    public void DecreaseVolume()
    {
        volume -= volumeStep;

        if (volume < 0)
            volume = 0;

        ApplyVolume();
    }

    private void ApplyVolume()
    {
        // Mengatur volume audio keseluruhan
        AudioListener.volume = volume / 100f;

        // Memperbarui angka di UI
        if (volumeText != null)
            volumeText.text = volume.ToString();

        // Menyimpan pengaturan
        PlayerPrefs.SetInt(VolumeKey, volume);
        PlayerPrefs.Save();

        Debug.Log("Volume Music: " + volume);
    }
}
