using UnityEngine;

// ============================================================
// マリオ風の敵
//
// ・重力で地面に乗る
// ・左右に移動する
// ・画面端で反対方向へ向きを変える
// ・時間が経つと巨大化する
// ・Playerに触れるとPlayerを吹き飛ばす
// ============================================================
public class GrowingEnemy : MonoBehaviour
{
    // ========================================================
    // 巨大化に関する設定
    // ========================================================

    // 1秒間にどれくらい大きくなるか
    public float growthSpeed = 0.1f;


    // 最大サイズ
    public float maxSize = 3f;


    // ========================================================
    // 移動に関する設定
    // ========================================================

    // 左右移動の速さ
    public float moveSpeed = 2f;


    // ========================================================
    // ノックバック
    // ========================================================

    // Playerを吹き飛ばす強さ
    public float knockbackPower = 8f;


    // Playerを上方向に
    // どれくらい吹き上げるか
    public float knockbackUpPower = 5f;


    // ========================================================
    // 内部で使う変数
    // ========================================================

    private Rigidbody2D rb;

    private Collider2D enemyCollider;

    private Camera mainCamera;


    // ========================================================
    // 敵が進む方向
    // ========================================================
    //
    // 1
    // → 右
    //
    // -1
    // → 左
    //
    private float moveDirection = 1f;


    // ========================================================
    // ゲーム開始時
    // ========================================================
    void Start()
    {
        // Rigidbody2D取得
        rb = GetComponent<Rigidbody2D>();


        // Collider2D取得
        enemyCollider =
            GetComponent<Collider2D>();


        // Main Camera取得
        mainCamera =
            Camera.main;


        // ====================================================
        // 最初の移動方向をランダムにする
        // ====================================================
        //
        // 動き自体は左右移動のみですが、
        //
        // ゲーム開始時に
        //
        // 左へ行くか
        // 右へ行くか
        //
        // だけランダムにしています。
        //
        if (Random.value < 0.5f)
        {
            moveDirection = -1f;
        }
        else
        {
            moveDirection = 1f;
        }
    }


    // ========================================================
    // 毎フレーム実行
    // ========================================================
    void Update()
    {
        // ====================================================
        // 敵を巨大化
        // ====================================================

        if (transform.localScale.x < maxSize)
        {
            // X・Yともに少しずつ大きくします。
            transform.localScale +=
                Vector3.one
                * growthSpeed
                * Time.deltaTime;


            // 最大サイズを超えた場合
            if (transform.localScale.x > maxSize)
            {
                transform.localScale =
                    new Vector3(
                        maxSize,
                        maxSize,
                        1f
                    );
            }
        }
    }


    // ========================================================
    // Rigidbody2Dの移動
    // ========================================================
    void FixedUpdate()
    {
        // 現在の速度
        Vector2 velocity =
            rb.linearVelocity;


        // ====================================================
        // X方向だけ変更
        // ====================================================
        //
        // Y方向には一切触りません。
        //
        // そのため、
        // 重力によって自然に地面へ落ちます。
        //
        velocity.x =
            moveDirection * moveSpeed;


        // Rigidbody2Dに設定
        rb.linearVelocity =
            velocity;


        // 画面端を確認します。
        CheckScreenEdge();
    }


    // ========================================================
    // 画面端で反転する処理
    // ========================================================
    void CheckScreenEdge()
    {
        if (
            mainCamera == null ||
            enemyCollider == null
        )
        {
            return;
        }


        // カメラと敵との距離
        float distance =
            Mathf.Abs(
                mainCamera.transform.position.z
                - transform.position.z
            );


        // 画面左下
        Vector3 bottomLeft =
            mainCamera.ViewportToWorldPoint(
                new Vector3(
                    0f,
                    0f,
                    distance
                )
            );


        // 画面右上
        Vector3 topRight =
            mainCamera.ViewportToWorldPoint(
                new Vector3(
                    1f,
                    1f,
                    distance
                )
            );


        // 敵の半分の横幅
        float halfWidth =
            enemyCollider.bounds.extents.x;


        // 敵の左端の限界位置
        float minX =
            bottomLeft.x + halfWidth;


        // 敵の右端の限界位置
        float maxX =
            topRight.x - halfWidth;


        // 現在位置
        Vector2 position =
            rb.position;


        // ====================================================
        // 左端に到達
        // ====================================================

        if (position.x <= minX)
        {
            // 位置を画面内へ戻す
            position.x = minX;


            // 右方向に変更
            moveDirection = 1f;
        }


        // ====================================================
        // 右端に到達
        // ====================================================

        if (position.x >= maxX)
        {
            // 位置を画面内へ戻す
            position.x = maxX;


            // 左方向に変更
            moveDirection = -1f;
        }


        // 修正した位置を設定
        rb.position =
            position;
    }


    // ========================================================
    // Playerにぶつかった瞬間
    // ========================================================
    private void OnCollisionEnter2D(
        Collision2D collision
    )
    {
        // ぶつかった相手から
        // PlayerMovementを探します。
        PlayerMovement player =
            collision.gameObject
            .GetComponentInParent<PlayerMovement>();


        // Playerではなかった場合
        if (player == null)
        {
            return;
        }


        // ====================================================
        // 敵からPlayerへ向かう方向
        // ====================================================

        Vector2 playerPosition =
            player.transform.position;


        Vector2 enemyPosition =
            transform.position;


        // Playerが敵の
        // 左側にいるか右側にいるか調べます。
        float horizontalDirection;


        if (playerPosition.x < enemyPosition.x)
        {
            // Playerが敵の左側
            horizontalDirection = -1f;
        }
        else
        {
            // Playerが敵の右側
            horizontalDirection = 1f;
        }


        // ====================================================
        // 吹っ飛ばす方向を作ります。
        // ====================================================
        //
        // 横方向
        // +
        // 少し上方向
        //
        // に吹き飛ばします。
        //
        // マリオ系のゲームでよくある
        // 「斜め上に弾かれる」ような感じです。
        //
        Vector2 knockbackDirection =
            new Vector2(
                horizontalDirection
                    * knockbackPower,

                knockbackUpPower
            );


        // ====================================================
        // Playerを吹っ飛ばします。
        // ====================================================

        // PlayerMovement側では
        //
        // direction × power
        //
        // にしているため、
        //
        // 今回はすでに強さを入れているので
        // powerを1にしています。
        //
        player.Knockback(
            knockbackDirection,
            1f
        );
    }
}