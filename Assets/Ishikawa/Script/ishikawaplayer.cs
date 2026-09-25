using UnityEngine;

public class ishikawaplayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    // ★追加：スタート位置を保存する変数
    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ★追加：ゲーム開始時の位置を記録
        startPosition = transform.position;
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

        // ★追加：落下判定（Y座標が -10 より下ならスタート位置に戻す）
        if (transform.position.y < -10f)
        {
            Respawn();
        }
    }

    // ★追加：スタート位置に戻す処理
    void Respawn()
    {
        transform.position = startPosition;
        rb.linearVelocity = Vector2.zero; // 落下速度をリセット
    }
}
