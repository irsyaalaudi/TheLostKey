
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D belum dipasang pada Player!");
        }
    }

    private void Update()
    {
        // Membaca input dari keyboard menggunakan New Input System
        movement = Vector2.zero;

        if (Keyboard.current == null) return;

        if (Keyboard.current.wKey.isPressed)
            movement.y += 1;

        if (Keyboard.current.sKey.isPressed)
            movement.y -= 1;

        if (Keyboard.current.aKey.isPressed)
            movement.x -= 1;

        if (Keyboard.current.dKey.isPressed)
            movement.x += 1;

        // Menyamakan kecepatan gerakan diagonal
        movement = movement.normalized;
    }

    private void FixedUpdate()
    {
        if (rb == null) return;

        // Menggerakkan player
        rb.linearVelocity = movement * moveSpeed;
    }
}
