using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    // 遷移先のシーン名（インスペクターから変更可能）
    [SerializeField] private string gameSceneName = "MainScene";

    // スタートボタンが押されたときに呼ぶメソッド
    public void OnStartButtonClicked()
    {
        // ゲームシーンに遷移
        SceneManager.LoadScene(gameSceneName);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
