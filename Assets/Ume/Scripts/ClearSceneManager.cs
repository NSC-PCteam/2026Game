using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClearSceneController : MonoBehaviour
{
    [Header("ボタン参照")]
    [SerializeField] private Button titleButton;
    // [SerializeField] private Button nextStageButton;

    [Header("遷移先シーン名")]
    [SerializeField] private string titleSceneName = "Ume_StartScene";
    // [SerializeField] private string nextSceneName = "Stage2"; // 次のステージまたはStage1

    private void Start()
    {
        // 前のシーンでTime.timeScaleが止まっていた場合に備えて確実に1に戻す
        Time.timeScale = 1f;

        if (titleButton != null)
        {
            titleButton.onClick.AddListener(OnTitleClicked);
        }

        // if (nextStageButton != null)
        // {
        //     nextStageButton.onClick.AddListener(OnNextStageClicked);
        // }
    }

    public void OnTitleClicked()
    {
        SceneManager.LoadScene(titleSceneName);
    }

    // public void OnNextStageClicked()
    // {
    //     SceneManager.LoadScene(nextSceneName);
    // }
}