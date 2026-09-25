using UnityEngine;

public class Ishikawatest : MonoBehaviour
{
    public float moveSpeed = 5f;        // 横移動の速さ
    public float jumpForce = 7f;        // ジャンプ力
    public Transform groundCheck;       // 地面判定の位置
    public float groundCheckRadius = 0.2f; // 地面判定の円の大きさ
    public LayerMask groundLayer;       // 地面レイヤー

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // --- 横移動 ---
        float x = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);

        // --- 地面判定 ---
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // --- ジャンプ ---
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}