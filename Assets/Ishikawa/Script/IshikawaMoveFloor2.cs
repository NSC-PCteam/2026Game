using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;          // 動く速さ
    public float moveDistance = 3f;   // どれくらい動くか
    public bool moveHorizontal = true; // trueなら左右、falseなら上下

    private Vector3 startPos;
    private int direction = 1;

    void Start()
    {
        // 最初の位置を覚えておく
        startPos = transform.position;
    }

    void Update()
    {
        if (moveHorizontal)
        {
            // 左右に動く
            transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

            // 一定距離で折り返す
            if (Mathf.Abs(transform.position.x - startPos.x) > moveDistance)
            {
                direction *= -1;
            }
        }
        else
        {
            // 上下に動く
            transform.Translate(Vector2.up * speed * direction * Time.deltaTime);

            if (Mathf.Abs(transform.position.y - startPos.y) > moveDistance)
            {
                direction *= -1;
            }
        }
    }
}
