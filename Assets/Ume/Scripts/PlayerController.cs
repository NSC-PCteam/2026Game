using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 移動速度（インスペクターから調整可能）
    [SerializeField] private float moveSpeed = 5f;

    // Rigidbody 2Dコンポーネントの参照
    private Rigidbody2D rb;

    // 移動入力を受け取る変数
    private Vector2 movement;

    private void Awake()
    {
        // オブジェクトがアクティブになったときに、Rigidbody 2Dコンポーネントを取得
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // ----- 移動入力の取得 -----
        // 矢印キーまたはWASDキーの入力を取得
        // -1.0から1.0までの範囲で値が返る（キーを押していないときは0）
        movement.x = Input.GetAxisRaw("Horizontal"); // 左右（A/Dキー、左/右矢印キー）
        movement.y = Input.GetAxisRaw("Vertical");   // 上下（W/Sキー、上/下矢印キー）

        // 斜め移動したときに移動速度が速くならないように、入力を正規化
        movement.Normalize();
    }

    private void FixedUpdate()
    {
        // ----- 物理演算（移動処理） -----
        // FixedUpdateは物理演算を行うために一定間隔で呼ばれるメソッドです。

        // 現在の入力（movement）と速度（moveSpeed）から、新しい移動速度ベクトルを計算
        // Rigidbody 2Dのvelocity（速度）を直接更新することで、移動を処理
        rb.linearVelocity = movement * moveSpeed;
    }
}