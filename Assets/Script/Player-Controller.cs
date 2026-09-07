using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpPower = 12f;

    Rigidbody2D rb;
    public bool isGrounded;
    Vector2 respawnPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPosition = rb.position;
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(
            move * moveSpeed,
            rb.linearVelocity.y
        );

        if (Input.GetButton("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpPower
            );

            isGrounded = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spike"))
        {
            Respawn();
        }
    }

    // 外部のスクリプトからも呼び出せる
    public void Respawn()
    {
        rb.linearVelocity = Vector2.zero;
        rb.position = respawnPosition;
        isGrounded = false;
    }
}