using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Rigidbody2D rb;

    // このステージを開始したときの位置
    private Vector2 stageStartPosition;

    // 同じ接触で繰り返しリスポーンするのを防ぐ
    private bool isRespawning;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError(
                "PlayerRespawnを付けたオブジェクトに" +
                "Rigidbody2Dがありません。",
                this
            );

            return;
        }

        // ステージを開いたときのPlayerの位置を保存
        stageStartPosition = rb.position;

        Debug.Log(
            $"ステージ開始位置を保存しました: {stageStartPosition}",
            this
        );
    }

    public void Respawn()
    {
        if (rb == null || isRespawning)
        {
            return;
        }

        isRespawning = true;

        // 移動速度を完全に止める
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // ステージ開始時の位置へ戻す
        rb.position = stageStartPosition;

        Debug.Log(
            $"プレイヤーを開始位置へ戻しました: {stageStartPosition}",
            this
        );

        StartCoroutine(ResetRespawnState());
    }

    private IEnumerator ResetRespawnState()
    {
        // 同じ接触による連続リスポーンを防ぐ
        yield return new WaitForFixedUpdate();

        isRespawning = false;
    }
}