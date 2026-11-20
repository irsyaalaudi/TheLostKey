
using System.Collections;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [Header("Laser Settings")]
    [SerializeField] private float activeDuration = 2f;
    [SerializeField] private float inactiveDuration = 2f;

    [Header("Group Settings")]
    [SerializeField] private float startDelay = 0f;

    private SpriteRenderer spriteRenderer;
    private Collider2D laserCollider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        laserCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        SetLaser(false);
        StartCoroutine(LaserCycle());
    }

    private IEnumerator LaserCycle()
    {
        // Jeda awal berdasarkan kelompok laser
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            // Laser menyala
            SetLaser(true);
            yield return new WaitForSeconds(activeDuration);

            // Laser mati
            SetLaser(false);
            yield return new WaitForSeconds(inactiveDuration);
        }
    }

    private void SetLaser(bool active)
    {
        if (spriteRenderer != null)
            spriteRenderer.enabled = active;

        if (laserCollider != null)
            laserCollider.enabled = active;
    }
}
