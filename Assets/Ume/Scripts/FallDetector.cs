using UnityEngine;

public class FallDetector : MonoBehaviour
{
    [Header("落下基準の高さ（Y座標）")]
    [SerializeField] private float fallThresholdY = -10f; // これを下回ったらゲームオーバー

    [Header("ゲームオーバー管理参照")]
    [SerializeField] private GameOverManager gameOverManager;

    private bool hasTriggered = false; // 多重呼び出し防止

    private void Awake()
    {
        // 未割り当ての場合はシーンまたはInGameUISystemから自動取得
        if (gameOverManager == null)
        {
            gameOverManager = FindFirstObjectByType<GameOverManager>();
        }
    }

    private void Update()
    {
        // すでにゲームオーバー済みの場合は判定しない
        if (hasTriggered) return;

        // プレイヤーのY座標が指定の高さを下回ったかチェック
        if (transform.position.y < fallThresholdY)
        {
            TriggerFallGameOver();
        }
    }

    private void TriggerFallGameOver()
    {
        hasTriggered = true;
        Debug.Log($"プレイヤーが高さ {fallThresholdY} を下回ったためゲームオーバーを発動します。");

        if (gameOverManager != null)
        {
            gameOverManager.TriggerGameOver();
        }
        else
        {
            Debug.LogError("GameOverManager が見つかりません！");
        }
    }

    // エディタのSceneビューで落下の基準線を赤色で可視化する
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 left = new Vector3(-1000f, fallThresholdY, 0f);
        Vector3 right = new Vector3(1000f, fallThresholdY, 0f);
        Gizmos.DrawLine(left, right);
    }
}