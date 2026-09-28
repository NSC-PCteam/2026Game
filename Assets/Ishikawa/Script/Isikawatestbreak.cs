using System.Collections;
using UnityEngine;

public class EnemySimple : MonoBehaviour
{
    [Header("アニメーション")]
    public Animator anim;              // 敵の Animator
    public string hitTriggerName = "Hit";  // 踏まれたときのトリガー名

    [Header("消滅設定")]
    public float destroyDelay = 0.5f;  // アニメーション再生後に消えるまでの時間

    private bool isDead = false;       // 踏まれたフラグ

    private void Start()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // プレイヤーが上から踏んだ判定
        if (!isDead && collision.CompareTag("Player") && collision.transform.position.y > transform.position.y)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        // Hitアニメーションを再生
        if (anim != null && !string.IsNullOrEmpty(hitTriggerName))
        {
            anim.SetTrigger(hitTriggerName);
        }

        // アニメーション後に敵を消す
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        gameObject.SetActive(false);  // 非表示にする（または Destroy(gameObject)）
    }
}
