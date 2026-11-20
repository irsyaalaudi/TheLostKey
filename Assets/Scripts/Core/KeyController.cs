
using System;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public event Action OnKeyCollected;

    [Header("Core References")]
    [SerializeField] private GameManager gameManager;

    [Header("Door Reference")]
    [SerializeField] private DoorController doorController;

    private bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Memastikan objek yang menyentuh adalah Player
        if (!other.CompareTag("Player"))
            return;

        // Mencegah kunci dikumpulkan lebih dari sekali
        if (isCollected)
            return;

        CollectKey();
    }

    private void CollectKey()
    {
        isCollected = true;

        Debug.Log("Kunci berhasil diambil!");

        // Membuka pintu setelah Key diambil
        if (doorController != null)
        {
            doorController.UnlockDoor();
        }
        else
        {
            Debug.LogError("DoorController belum dihubungkan pada Key!");
        }

        // Memberitahu GameManager bahwa Key berhasil diambil
        if (gameManager != null)
        {
            gameManager.HandleKeyCollected();
        }
        else
        {
            Debug.LogError("GameManager belum dihubungkan pada Key!");
        }

        // Memanggil event jika ada sistem lain yang berlangganan
        OnKeyCollected?.Invoke();

        // Menghilangkan objek Key dari scene
        Destroy(gameObject);
    }
}
