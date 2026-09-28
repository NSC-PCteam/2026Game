using UnityEngine;

public class Ishikawabreak : MonoBehaviour
{
    public Sprite normalSprite;      // 通常
    public Sprite crackSprite;       // ひび割れ
    public Sprite breakSprite;       // 崩れる直前

    public float crackTime = 1f;     // 乗って1秒後にひび割れ
    public float breakTime = 2f;     // さらに1秒後に崩れる
     public float destroyDelay = 2f;  // 崩れた後に消えるまでの時間 ← 追加！

    private SpriteRenderer sr;
    private bool isStepped = false;
    private float timer = 0f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = normalSprite;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーが乗ったら開始
        if (collision.gameObject.tag == "Player")
        {
            isStepped = true;
        }
    }

    void Update()
    {
        if (!isStepped) return;

        timer += Time.deltaTime;

        if (timer > crackTime && timer < breakTime)
        {
            sr.sprite = crackSprite;
        }
        else if (timer > breakTime)
        {
            sr.sprite = breakSprite;

            // 崩れた後に消えるまでの時間を調整可能
            Destroy(gameObject, destroyDelay);
        }
    }
}
