using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [Header("UI参照")]
    [SerializeField] private GameObject pausePanel; // ポーズパネルの参照
    [SerializeField] private Button backButton; // ポーズパネルの「戻る」ボタンの参照
    [SerializeField] private Button titleButton; // ポーズパネルの「タイトルに戻る」ボタンの参照

    [Header("シーン名設定")]
    [SerializeField] private string titleSceneName = "Ume_StartScene"; // タイトルシーンの名前

    [Header("他システム参照")]
    [SerializeField] private GameOverManager gameOverManager; // ゲームオーバー管理スクリプトの参照

    private bool isPaused = false; // ポーズ状態のフラグ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(backButton != null)
        {
            // ボタンのクリックイベントにメソッドを割り当て
            backButton.onClick.RemoveAllListeners(); // 既存のリスナーを削除
            backButton.onClick.AddListener(BackGame);
        }
        if(titleButton != null)
        {
            // ボタンのクリックイベントにメソッドを割り当て
            titleButton.onClick.RemoveAllListeners(); // 既存のリスナーを削除
            titleButton.onClick.AddListener(GoToTitle);
        }
        if(pausePanel != null)
        {
            pausePanel.SetActive(false); // ゲーム開始時にポーズパネルを非表示にする
        }
        Time.timeScale = 1f; // ゲームの時間を通常に戻す

        if(backButton == null || titleButton == null || pausePanel == null)
        {
            Debug.LogError("UI参照が設定されていません。PauseControllerのインスペクターを確認してください。");
            return;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(gameOverManager != null && gameOverManager.IsGameOver)
        {
            return; // ゲームオーバー状態の場合はポーズの切り替えを無効化
        }

        if(Input.GetKeyDown(KeyCode.Escape)) // Escapeキーが押されたとき
        {
            if(isPaused)
            {
                BackGame(); // ポーズ解除
            }
            else
            {
                PauseGame(); // ポーズ
            }
        }
    }


    public void PauseGame()
    {
        pausePanel.SetActive(true); // ポーズパネルを表示
        Time.timeScale = 0f; // ゲームの時間を停止
        isPaused = true; // ポーズ状態に設定
    }

    public void BackGame()
    {
        pausePanel.SetActive(false); // ポーズパネルを非表示
        Time.timeScale = 1f; // ゲームの時間を再開
        isPaused = false; // ポーズ状態を解除
    }

    public void GoToTitle()
    {
        Time.timeScale = 1f; // ゲームの時間を再開（タイトルに戻る前に）
        SceneManager.LoadScene(titleSceneName); // タイトルシーンに遷移
    }
    private void OnDestroy()
    {
        // シーン遷移やオブジェクト破棄時に確実に時間を戻しておく
        Time.timeScale = 1f;
    }
}