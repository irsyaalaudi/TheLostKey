
using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("Core Reference")]
    [SerializeField] private GameManager gameManager;

    [Header("Damage Settings")]
    [SerializeField] private float damageCooldown = 1f;

    private bool canTakeDamage = true;

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
        if (!obj.CompareTag("Hazard"))
            return;

        if (!canTakeDamage)
            return;

        if (gameManager == null)
        {
            Debug.LogError("GameManager belum dihubungkan!");
            return;
        }

        if (gameManager.CurrentState != GameState.Playing)
            return;

        StartCoroutine(DamageCooldown());

        gameManager.TakeDamage(1);
    }

    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false;

        yield return new WaitForSeconds(damageCooldown);

        canTakeDamage = true;
    }
}
