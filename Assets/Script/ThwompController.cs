using System.Collections;
using UnityEngine;

public class ThwompController : MonoBehaviour
{
    [Header("プレイヤー")]
    public Transform player;

    [Header("反応範囲")]
    public float detectionDistance = 3f;

    [Header("落下設定")]
    public float fallSpeed = 12f;

    // 落下開始から元へ戻り始めるまでの最大時間
    public float maxFallTime = 2f;

    // 最初の位置から落下できる最大距離
    public float maxFallDistance = 8f;

    [Header("帰還設定")]
    public float waitTime = 0.5f;
    public float returnSpeed = 3f;

    Rigidbody2D rb;
    Vector2 startPosition;

    bool isFalling;
    bool isWaiting;
    bool isReturning;

    float fallTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 最初の位置を保存
        startPosition = rb.position;

        // 念のためコード側でも設定
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        rb.linearVelocity = Vector2.zero;
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        // 待機中だけプレイヤーを探す
        if (isFalling || isWaiting || isReturning)
        {
            return;
        }

        // プレイヤーとの横方向の距離
        float horizontalDistance = Mathf.Abs(
            player.position.x - transform.position.x
        );

        // プレイヤーがドッスンより下にいるか
        bool playerIsBelow =
            player.position.y < transform.position.y;

        if (horizontalDistance <= detectionDistance &&
            playerIsBelow)
        {
            StartFalling();
        }
    }

    void FixedUpdate()
    {
        if (isFalling)
        {
            Fall();
        }

        if (isReturning)
        {
            ReturnToStart();
        }
    }

    void StartFalling()
    {
        isFalling = true;
        fallTimer = 0f;

        rb.linearVelocity = new Vector2(0f, -fallSpeed);
    }

    void Fall()
    {
        // 落下時間を計測
        fallTimer += Time.fixedDeltaTime;

        // 一定の速度で真下へ落下
        rb.linearVelocity = new Vector2(0f, -fallSpeed);

        // 最初の位置からどれくらい落下したか
        float fallenDistance =
            startPosition.y - rb.position.y;

        // 時間または距離が上限に達したら帰還準備
        if (fallTimer >= maxFallTime ||
            fallenDistance >= maxFallDistance)
        {
            BeginReturn();
        }
    }

    void BeginReturn()
    {
        if (!isFalling)
        {
            return;
        }

        isFalling = false;
        isWaiting = true;

        rb.linearVelocity = Vector2.zero;

        StartCoroutine(ReturnAfterDelay());
    }

    IEnumerator ReturnAfterDelay()
    {
        // 落下終了地点で少し待つ
        yield return new WaitForSeconds(waitTime);

        isWaiting = false;
        isReturning = true;
    }

    void ReturnToStart()
    {
        rb.linearVelocity = Vector2.zero;

        Vector2 nextPosition = Vector2.MoveTowards(
            rb.position,
            startPosition,
            returnSpeed * Time.fixedDeltaTime
        );

        rb.MovePosition(nextPosition);

        // 最初の位置へほぼ到着した
        if (Vector2.Distance(rb.position, startPosition) <= 0.05f)
        {
            rb.position = startPosition;
            rb.linearVelocity = Vector2.zero;

            isReturning = false;
            fallTimer = 0f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーに当たった場合
        PlayerMove playerMove =
            collision.gameObject.GetComponent<PlayerMove>();

        if (playerMove != null)
        {
            playerMove.Respawn();
        }

        // 地面に当たった場合
        if (collision.gameObject.CompareTag("Ground") &&
            isFalling)
        {
            BeginReturn();
        }
    }
}