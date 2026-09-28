using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("UI参照")]
    [SerializeField] private GameObject gameOverPanel; // ゲームオーバーパネルの参照
    [SerializeField] private Button retryButton; // ゲームオーバーパネルの「リトライ」ボタンの参照
    [SerializeField] private Button titleButton; // ゲームオーバーパネルの「タイトルに戻る」ボタンの参照

    [Header("シーン名設定")]
    [SerializeField] private string titleSceneName = "Ume_StartScene"; // タイトルシーンの名前

    private bool isGameOver = false; // ゲームオーバー状態のフラグ
    public bool IsGameOver => isGameOver; // ゲームオーバー状態を外部から取得するためのプロパティ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if(gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); // ゲーム開始時にゲームオーバーパネルを非表示にする
        }
        Time.timeScale = 1f; // ゲームの時間を通常に戻す

        if(retryButton != null)
        {
            retryButton.onClick.RemoveAllListeners(); // 既存のリスナーを削除
            retryButton.onClick.AddListener(RetryGame); // リトライボタンのクリックイベントにメソッドを割り当て
        }
        if(titleButton != null)
        {
            titleButton.onClick.RemoveAllListeners(); // 既存のリスナーを削除
            titleButton.onClick.AddListener(GoToTitle); // タイトルボタンのクリックイベントにメソッドを割り当て
        }
    }

    public void TriggerGameOver()// ゲームオーバーをトリガーするメソッド
    {
        isGameOver = true; // ゲームオーバー状態に設定
        if(gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // ゲームオーバーパネルを表示
        }
        Time.timeScale = 0f; // ゲームの時間を停止
    }
    
    public void RetryGame()// ゲームをリトライするメソッド
    {
        Time.timeScale = 1f; // ゲームの時間を通常に戻す
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 現在のシーンを再読み込み
    }

    public void GoToTitle()// タイトル画面に戻るメソッド
    {
        Time.timeScale = 1f; // ゲームの時間を通常に戻す
        SceneManager.LoadScene(titleSceneName); // タイトルシーンに移動
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f; // ゲームの時間を通常に戻す
    }
}
