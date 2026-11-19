
using System;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public event Action OnDoorReached;

    [Header("Door Settings")]
    [SerializeField] private bool isUnlocked = false;

    [Header("Core Reference")]
    [SerializeField] private GameManager gameManager;

    private Collider2D doorCollider;

    private void Awake()
    {
        doorCollider = GetComponent<Collider2D>();

        if (doorCollider == null)
        {
            Debug.LogError("Door membutuhkan Collider2D!");
            return;
        }

        doorCollider.isTrigger = isUnlocked;
    }

    public void UnlockDoor()
    {
        if (isUnlocked) return;

        isUnlocked = true;

        if (doorCollider != null)
            doorCollider.isTrigger = true;

        Debug.Log("Pintu berhasil dibuka!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isUnlocked) return;
        if (!other.CompareTag("Player")) return;

        Debug.Log("Player berhasil mencapai pintu keluar!");

        OnDoorReached?.Invoke();

        if (gameManager != null)
        {
            gameManager.HandleWin();
        }
        else
        {
            Debug.LogError("GameManager belum dihubungkan pada Door!");
        }
    }
}
