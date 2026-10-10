using UnityEngine;

public class Ishikawabreak : MonoBehaviour
{
    public Sprite normalSprite;      // 通常
    public Sprite crackSprite;       // ひび割れ
    public Sprite breakSprite;       // 崩れる直前

    public float crackTime = 1f;     // ひび割れまでの時間
    public float breakTime = 2f;     // 崩れるまでの時間
    public float destroyTime = 3f;   // 完全に消えるまでの時間

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
        if (collision.gameObject.tag == "Player")
        {
            isStepped = true;
        }
    }

    void Update()
    {
        if (!isStepped) return;

        timer += Time.deltaTime;

        // ひび割れ
        if (timer > crackTime && timer < breakTime)
        {
            sr.sprite = crackSprite;
        }
        // 崩れる直前
        else if (timer > breakTime && timer < destroyTime)
        {
            sr.sprite = breakSprite;
        }
        // 完全に消える
        else if (timer > destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
