using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    public int startDirection = 1;   // Inspectorで方向を設定できるようにする

    public float speed = 3f;          // 動く速さ
    public float moveDistance = 8f;   // どれくらい動くか
    public bool moveHorizontal = true; // trueなら左右、falseなら上下

    public float waitTime = 1f;     // ★端で止まる時間（秒）
    private float waitTimer = 0f;     // ★止まっている時間を計測

    private Vector3 startPos;
    private int direction;
    void Start()
    {
        
        // ★ここで startDirection を使う！
        direction = startDirection;
        // 最初の位置を覚えておく
        startPos = transform.position;
    }

    void Update()
    {
        // ★止まっている間は動かない
        if (waitTimer > 0f)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        if (moveHorizontal)
        {
            // 左右に動く
            transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

            // 一定距離で折り返す
            if (Mathf.Abs(transform.position.x - startPos.x) >= moveDistance - 0.1f)
            {
                direction *= -1;//方向反転
                 waitTimer = waitTime; // ★止まる時間セット
            }
        }
        else
        {
            // 上下に動く
            transform.Translate(Vector2.up * speed * direction * Time.deltaTime);

            if (Mathf.Abs(transform.position.y - startPos.y) > moveDistance)
            {
                direction *= -1;
                waitTimer = waitTime; // ★止まる時間セット
            }
        }
    }
}


