
using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public event Action OnHazardHit;

    [Header("Core Reference")]
    [SerializeField] private GameManager gameManager;

    private bool hasHitHazard = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckHazard(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckHazard(other.gameObject);
    }

    private void CheckHazard(GameObject obj)
    {
        if (hasHitHazard) return;

        if (obj.CompareTag("Hazard"))
        {
            hasHitHazard = true;

            Debug.Log("Player terkena rintangan!");

            OnHazardHit?.Invoke();

            if (gameManager != null)
            {
                gameManager.HandleGameOver();
            }
            else
            {
                Debug.LogError("GameManager belum dihubungkan pada PlayerCollision!");
            }
        }
    }
}
