using UnityEngine;

// ============================================================
// マリオ風Playerの操作
//
// ・A / D または ← / → で左右移動
// ・Spaceでジャンプ
// ・重力で自然に落下
// ・敵に触れると吹き飛ばされる
// ・画面外には出ない
// ============================================================
public class PlayerMovement : MonoBehaviour
{
    // ========================================================
    // 移動に関する設定
    // ========================================================

    // 左右に移動する速さです。
    //
    // Inspectorから変更できます。
    public float moveSpeed = 5f;


    // ========================================================
    // ジャンプに関する設定
    // ========================================================

    // ジャンプの強さです。
    //
    // 数字が大きいほど高くジャンプします。
    public float jumpPower = 10f;


    // Playerの足元に置いた
    // GroundCheckをここに指定します。
    public Transform groundCheck;


    // 地面として扱うLayerを指定します。
    //
    // Inspectorから「Ground」を選択します。
    public LayerMask groundLayer;


    // GroundCheckを中心に
    // どれくらいの範囲で地面を探すかです。
    public float groundCheckRadius = 0.2f;


    // ========================================================
    // ノックバック設定
    // ========================================================

    // 敵に触れたあと、
    // 何秒間操作できなくするかです。
    public float knockbackTime = 0.3f;


    // ========================================================
    // 内部で使用する変数
    // ========================================================

    // PlayerについているRigidbody2D
    private Rigidbody2D rb;


    // PlayerについているCollider2D
    private Collider2D playerCollider;


    // Main Camera
    private Camera mainCamera;


    // 左右の入力を保存します。
    //
    // -1 = 左
    //  0 = 動かない
    //  1 = 右
    private float horizontalInput;


    // 現在地面に立っているかどうか
    //
    // true
    // → 地面にいる
    //
    // false
    // → 空中にいる
    private bool isGrounded;


    // 現在ノックバックされているか
    private bool isKnockback = false;


    // ノックバックの残り時間
    private float knockbackTimer = 0f;


    // ========================================================
    // ゲーム開始時に1度だけ実行
    // ========================================================
    void Start()
    {
        // PlayerについているRigidbody2Dを取得します。
        rb = GetComponent<Rigidbody2D>();


        // PlayerについているCollider2Dを取得します。
        playerCollider = GetComponent<Collider2D>();


        // Main Cameraを取得します。
        mainCamera = Camera.main;
    }


    // ========================================================
    // 毎フレーム実行
    // ========================================================
    void Update()
    {
        // ====================================================
        // 地面にいるか確認
        // ====================================================

        // GroundCheckの周囲に
        // Ground LayerのColliderがあるか調べています。
        //
        // あればtrue
        // なければfalse
        //
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );


        // ====================================================
        // ノックバック中の処理
        // ====================================================

        if (isKnockback)
        {
            // 残り時間を減らします。
            knockbackTimer -= Time.deltaTime;


            // ノックバック時間が終了したら
            if (knockbackTimer <= 0f)
            {
                // 通常操作へ戻します。
                isKnockback = false;
            }


            // ノックバック中は
            // Player自身による操作を受け付けません。
            return;
        }


        // ====================================================
        // 左右の入力
        // ====================================================

        // Aキー・左矢印
        // → -1
        //
        // Dキー・右矢印
        // → 1
        //
        horizontalInput =
            Input.GetAxisRaw("Horizontal");


        // ====================================================
        // ジャンプ
        // ====================================================

        // Spaceキーを押した瞬間
        //
        // かつ
        //
        // 地面に立っている場合
        //
        // だけジャンプします。
        //
        if (
            Input.GetKeyDown(KeyCode.Space)
            && isGrounded
        )
        {
            // 現在の速度を取得します。
            Vector2 velocity = rb.linearVelocity;


            // Y方向の速度を
            // jumpPowerに変更します。
            //
            // Y方向は上方向なので、
            // Playerが上へ飛びます。
            velocity.y = jumpPower;


            // Rigidbody2Dに設定します。
            rb.linearVelocity = velocity;
        }
    }


    // ========================================================
    // 物理演算用
    // ========================================================
    void FixedUpdate()
    {
        // ====================================================
        // 普通に操作できる場合
        // ====================================================

        if (!isKnockback)
        {
            // 現在の速度を取得します。
            Vector2 velocity = rb.linearVelocity;


            // ================================================
            // X方向だけ変更します。
            // ================================================
            //
            // Y方向は変更しません。
            //
            // なぜなら、
            //
            // Y方向
            // ↓
            // ジャンプや重力
            //
            // に使っているからです。
            //
            velocity.x =
                horizontalInput * moveSpeed;


            // Rigidbody2Dに設定します。
            rb.linearVelocity = velocity;
        }


        // 画面外へ出ないようにします。
        KeepInsideScreen();
    }


    // ========================================================
    // Playerを画面内に収める
    // ========================================================
    void KeepInsideScreen()
    {
        // Cameraなどが見つからない場合は
        // 処理を終了します。
        if (
            mainCamera == null ||
            playerCollider == null
        )
        {
            return;
        }


        // カメラとPlayerとのZ方向の距離
        float distance =
            Mathf.Abs(
                mainCamera.transform.position.z
                - transform.position.z
            );


        // ====================================================
        // 画面左下
        // ====================================================

        Vector3 bottomLeft =
            mainCamera.ViewportToWorldPoint(
                new Vector3(
                    0f,
                    0f,
                    distance
                )
            );


        // ====================================================
        // 画面右上
        // ====================================================

        Vector3 topRight =
            mainCamera.ViewportToWorldPoint(
                new Vector3(
                    1f,
                    1f,
                    distance
                )
            );


        // Playerの半分の大きさ
        Vector2 halfSize =
            playerCollider.bounds.extents;


        // Playerの体全体が画面内に入るように
        // 移動できる範囲を決めます。
        float minX =
            bottomLeft.x + halfSize.x;

        float maxX =
            topRight.x - halfSize.x;


        // 現在位置
        Vector2 position = rb.position;


        // X方向だけ画面内に制限します。
        //
        // マリオ風ゲームなので、
        // Y方向については重力と地面に任せます。
        position.x =
            Mathf.Clamp(
                position.x,
                minX,
                maxX
            );


        // 修正した位置を設定します。
        rb.position = position;
    }


    // ========================================================
    // 敵から呼ばれるノックバック処理
    // ========================================================
    public void Knockback(
        Vector2 direction,
        float power
    )
    {
        // ノックバック中にします。
        isKnockback = true;


        // ノックバック時間を設定します。
        knockbackTimer =
            knockbackTime;


        // ====================================================
        // Playerを吹き飛ばします。
        // ====================================================

        rb.linearVelocity =
            direction * power;
    }


    // ========================================================
    // GroundCheckの範囲を
    // Scene画面で確認するための機能です。
    // ========================================================
    //
    // ゲームそのものには影響しません。
    //
    private void OnDrawGizmosSelected()
    {
        // GroundCheckが設定されていなければ
        // 何もしません。
        if (groundCheck == null)
        {
            return;
        }


        // GroundCheckを中心に
        // 円を表示します。
        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundCheckRadius
        );
    }
}