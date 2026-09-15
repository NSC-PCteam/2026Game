using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpPower = 12f;

    // 地上ジャンプを含めた最大ジャンプ回数
    public int maxJumpCount = 2;

    Rigidbody2D rb;
    public bool isGrounded;
    Vector2 respawnPosition;

    // 現在のジャンプ回数
    int jumpCount;

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

        // ジャンプボタンを押した瞬間に実行
        if (Input.GetButtonDown("Jump") &&
            jumpCount < maxJumpCount)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpPower
            );

            jumpCount++;
            isGrounded = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            // 地面に触れたらジャンプ回数をリセット
            jumpCount = 0;
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
        jumpCount = 0;
    }
}