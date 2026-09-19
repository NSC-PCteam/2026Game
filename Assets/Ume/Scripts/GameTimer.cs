using UnityEngine;
using TMPro;
public class GameTimer : MonoBehaviour
{
    [Header("タイマー設定")]
    [SerializeField] private float totalTime =300f; // タイマーの総時間（秒）
    [SerializeField] private TextMeshProUGUI timerText; // タイマー表示用のTextMeshProUGUI
    
    [Header("ゲームオーバー設定")]
    [SerializeField] private GameOverManager gameOverManager; // ゲームオーバー管理スクリプトの参照

    private float remainingTime; // 残り時間
    private bool isTimeUp = false; // 時間切れフラグ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        remainingTime = totalTime; // 残り時間を総時間で初期化
        UpdateTimerDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimeUp)
        {
            return; // 時間切れの場合はタイマーの更新を停止
        }
        
        remainingTime -= Time.deltaTime; // 残り時間を減少させる
        if (remainingTime <= 0f)
        {
            remainingTime = 0f; // 残り時間が0未満にならないようにする
            isTimeUp = true; // 時間切れフラグを立てる
            TimeUp(); // 時間切れ処理を呼び出す
        }

        UpdateTimerDisplay(); // タイマー表示を更新
    }
    private void UpdateTimerDisplay()
    {
        if(timerText != null)
        {
            int displayTime = Mathf.CeilToInt(remainingTime); // 残り時間を整数に変換
            timerText.text = $"TIME:{displayTime:D3}"; // タイマー表示を更新
        }
    }

    private void TimeUp()
    {
        Debug.Log("Time's up!"); // デバッグログに時間切れを表示
        // ここでゲームオーバー処理やシーン遷移などを行うことができます
        if(gameOverManager != null)
        {
            gameOverManager.TriggerGameOver(); // ゲームオーバー処理を呼び出す
        }
    }
    public float RemainingTime => remainingTime; // 残り時間を外部から取得するためのプロパティ
    public bool IsTimeUp => isTimeUp; // 時間切れフラグを外部から取得するためのプロパティ
}
