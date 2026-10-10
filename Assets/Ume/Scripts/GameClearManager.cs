using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameClearManager : MonoBehaviour
{
    [Header("UI参照")]
    [SerializeField] private GameObject gameClearPanel;   // ゲームクリアパネル
    [SerializeField] private Button nextStageButton;      // 「次のステージに進む」ボタン
    [SerializeField] private Button titleButton;          // 「タイトルに戻る」ボタン

    [Header("シーン遷移設定")]
    [SerializeField] private string nextSceneName = "Stage2";          // 次のステージのシーン名
    [SerializeField] private string titleSceneName = "Ume_StartScene";  // タイトルシーン名

    private bool isGameClear = false;
    public bool IsGameClear => isGameClear; // ポーズ制御などで外部から確認用

    private void Awake()
    {
        // 初期状態は非表示＆時間復元
        if (gameClearPanel != null)
        {
            gameClearPanel.SetActive(false);
        }
        Time.timeScale = 1f;

        // ボタンのクリックイベント登録
        if (nextStageButton != null)
        {
            nextStageButton.onClick.RemoveAllListeners();
            nextStageButton.onClick.AddListener(GoToNextStage);
        }

        if (titleButton != null)
        {
            titleButton.onClick.RemoveAllListeners();
            titleButton.onClick.AddListener(GoToTitle);
        }
    }

    // クリア処理を呼び出す
    public void TriggerGameClear()
    {
        isGameClear = true;

        if (gameClearPanel != null)
        {
            gameClearPanel.SetActive(true);
        }

        Time.timeScale = 0f; // ゲーム内の時間を停止
        Debug.Log("STAGE CLEAR!");
    }

    // 次のステージへ進む
    public void GoToNextStage()
    {
        Time.timeScale = 1f; // 時間を戻してから遷移
        SceneManager.LoadScene(nextSceneName);
    }

    // タイトルへ戻る
    public void GoToTitle()
    {
        Time.timeScale = 1f; // 時間を戻してから遷移
        SceneManager.LoadScene(titleSceneName);
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}