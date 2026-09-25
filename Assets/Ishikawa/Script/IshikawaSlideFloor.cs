using UnityEngine;

public class IshikawaSlideFloor : MonoBehaviour
{
    public float speed = 2f;          // 動く速さ
    public float moveDistance = 3f;   // どれくらい動くか
    public bool moveHorizontal = true; // trueなら左右、falseなら上下

    private Vector3 startPos;
    private int direction = -1;  // ← ここを -1 にすると逆方向に動く！

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (moveHorizontal)
        {
            transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

            if (Mathf.Abs(transform.position.x - startPos.x) > moveDistance)
            {
                direction *= -1;
            }
        }
        else
        {
            transform.Translate(Vector2.up * speed * direction * Time.deltaTime);

            if (Mathf.Abs(transform.position.y - startPos.y) > moveDistance)
            {
                direction *= -1;
            }
        }
    }
}
