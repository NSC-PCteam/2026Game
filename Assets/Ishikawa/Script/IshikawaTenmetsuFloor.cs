using UnityEngine;
using System.Collections;//追加

public class IshikawaTenmetsuFloor : MonoBehaviour
{
    public float interval = 1.5f;

    public Sprite activeSprite;   // 乗れるときの画像
    public Sprite inactiveSprite; // 乗れないときの画像

    private Collider2D col;
    private SpriteRenderer sr;

    void Start()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(SwitchRoutine());
    }

    IEnumerator SwitchRoutine()
    {
        while (true)
        {
            // 乗れる状態
            col.enabled = true;
            sr.sprite = activeSprite;
            yield return new WaitForSeconds(interval);

            // 乗れない状態（画像だけ変える）
            col.enabled = false;
            sr.sprite = inactiveSprite;
            yield return new WaitForSeconds(interval);
        }
    }
}
