using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [Header("判定タグ")]
    [SerializeField] private string targetTag = "Player";

    [Header("参照")]
    [SerializeField] private GameClearManager gameClearManager;

    private bool hasTriggered = false; // 多重発火防止

    private void Awake()
    {
        // 未割り当ての場合はシーンから自動取得
        if (gameClearManager == null)
        {
            gameClearManager = FindFirstObjectByType<GameClearManager>();
        }
    }

    // 2Dゲームの場合
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (!hasTriggered && collision.CompareTag(targetTag))
        {
            Debug.Log("ゴール判定");
            ClearGoal();
        }
    }

    private void ClearGoal()
    {
        hasTriggered = true;
        if (gameClearManager != null)
        {
            gameClearManager.TriggerGameClear();
        }
        else
        {
            Debug.LogError("GameClearManager が見つかりません！");
        }
    }
}