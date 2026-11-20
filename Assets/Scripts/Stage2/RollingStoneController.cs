using UnityEngine;

public class RollingStoneController : MonoBehaviour
{
    [Header("Movement Points")]
    [SerializeField] private Transform pointLeft;
    [SerializeField] private Transform pointRight;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float waitTime = 0.5f;

    private Rigidbody2D rb;

    private Vector2 targetPosition;
    private bool movingRight = true;
    private float waitTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (rb == null || pointLeft == null || pointRight == null)
        {
            Debug.LogError("RollingStone: Rigidbody2D atau Movement Points belum diatur!");
            enabled = false;
            return;
        }

        if (Vector2.Distance(pointLeft.position, pointRight.position) < 1f)
        {
            Debug.LogError("PointLeft dan PointRight terlalu dekat!");
            enabled = false;
            return;
        }

        // Mulai dari titik kiri
        rb.position = pointLeft.position;

        movingRight = true;
        targetPosition = pointRight.position;
    }

    private void FixedUpdate()
    {
        if (waitTimer > 0f)
        {
            waitTimer -= Time.fixedDeltaTime;
            return;
        }

        Vector2 currentPosition = rb.position;
        float distance = Vector2.Distance(currentPosition, targetPosition);
        float movement = speed * Time.fixedDeltaTime;

        // Jika sudah mencapai titik tujuan
        if (distance <= movement)
        {
            rb.MovePosition(targetPosition);

            // Ganti arah setelah sampai
            movingRight = !movingRight;

            targetPosition = movingRight
                ? pointRight.position
                : pointLeft.position;

            waitTimer = waitTime;
            return;
        }

        // Bergerak menuju target
        Vector2 direction = (targetPosition - currentPosition).normalized;

        rb.MovePosition(currentPosition + direction * movement);
    }
}