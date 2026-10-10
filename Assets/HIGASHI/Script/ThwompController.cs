using System.Collections;
using UnityEngine;

public class ThwompController : MonoBehaviour
{
    [Header("プレイヤー")]
    [SerializeField] private Transform player;

    [Header("ギミックの種類")]
    [Tooltip("ドッスンならオン、固定トゲならオフ")]
    [SerializeField] private bool movesLikeThwomp = true;

    [Header("反応範囲")]
    [SerializeField] private float detectionDistance = 3f;

    [Header("落下設定")]
    [SerializeField] private float fallSpeed = 12f;

    [Tooltip("落下開始から元へ戻り始めるまでの最大時間")]
    [SerializeField] private float maxFallTime = 2f;

    [Tooltip("最初の位置から落下できる最大距離")]
    [SerializeField] private float maxFallDistance = 8f;

    [Header("帰還設定")]
    [SerializeField] private float waitTime = 0.5f;

    [SerializeField] private float returnSpeed = 3f;

    private Rigidbody2D rb;
    private Vector2 startPosition;

    private bool isFalling;
    private bool isWaiting;
    private bool isReturning;

    private float fallTimer;

    void Start()
    {
        // 固定トゲの場合、Rigidbody2Dがなくても動作する
        rb = GetComponent<Rigidbody2D>();

        if (movesLikeThwomp)
        {
            if (rb == null)
            {
                Debug.LogError(
                    "ドッスンとして使用する場合は、" +
                    "Rigidbody2Dが必要です。",
                    this
                );

                enabled = false;
                return;
            }

            // ドッスンの最初の位置を保存
            startPosition = rb.position;

            rb.gravityScale = 0f;
            rb.freezeRotation = true;
            rb.collisionDetectionMode =
                CollisionDetectionMode2D.Continuous;

            rb.linearVelocity = Vector2.zero;
        }

        if (player == null)
        {
            Debug.LogWarning(
                "ThwompControllerのPlayerが設定されていません。" +
                "固定トゲの場合、落下判定には影響しません。",
                this
            );
        }
    }

    void Update()
    {
        // 固定トゲの場合は落下処理を行わない
        if (!movesLikeThwomp)
        {
            return;
        }

        if (player == null)
        {
            return;
        }

        // 落下中・待機中・帰還中は再反応しない
        if (isFalling || isWaiting || isReturning)
        {
            return;
        }

        float horizontalDistance = Mathf.Abs(
            player.position.x - transform.position.x
        );

        bool playerIsBelow =
            player.position.y < transform.position.y;

            Debug.Log($"playerposition={player.position}, transform.position={transform.position}");

        if (horizontalDistance <= detectionDistance &&
            playerIsBelow)
        {
            StartFalling();
        }
    }

    void FixedUpdate()
    {
        if (!movesLikeThwomp || rb == null)
        {
            return;
        }

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
        if (rb == null)
        {
            return;
        }

        isFalling = true;
        fallTimer = 0f;

        rb.linearVelocity = new Vector2(0f, -fallSpeed);
    }

    void Fall()
    {
        fallTimer += Time.fixedDeltaTime;

        rb.linearVelocity = new Vector2(0f, -fallSpeed);

        float fallenDistance =
            startPosition.y - rb.position.y;

        if (fallTimer >= maxFallTime ||
            fallenDistance >= maxFallDistance)
        {
            BeginReturn();
        }
    }

    void BeginReturn()
    {
        if (!movesLikeThwomp ||
            rb == null ||
            !isFalling)
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

        if (Vector2.Distance(
                rb.position,
                startPosition
            ) <= 0.05f)
        {
            rb.position = startPosition;
            rb.linearVelocity = Vector2.zero;

            isReturning = false;
            fallTimer = 0f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーまたはPlayerの子Colliderから
        // PlayerRespawnを探す
        PlayerRespawn playerRespawn =
            collision.gameObject
                .GetComponentInParent<PlayerRespawn>();

        if (playerRespawn != null)
        {
            playerRespawn.Respawn();

            // ドッスンの場合は接触後に帰還を開始
            if (movesLikeThwomp && isFalling)
            {
                BeginReturn();
            }

            return;
        }

        // ドッスンが地面に当たった場合
        if (movesLikeThwomp &&
            isFalling &&
            collision.gameObject.CompareTag("Ground"))
        {
            BeginReturn();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // トゲのColliderでIs Triggerをオンにしている場合
        PlayerRespawn playerRespawn =
            other.GetComponentInParent<PlayerRespawn>();

        if (playerRespawn != null)
        {
            playerRespawn.Respawn();

            if (movesLikeThwomp && isFalling)
            {
                BeginReturn();
            }
        }
    }
}