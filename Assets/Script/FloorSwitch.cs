using UnityEngine;

public class FloorSwitch : MonoBehaviour
{
    [Header("スイッチの見た目")]
    public SpriteRenderer switchRenderer;
    public Color offColor = Color.red;
    public Color onColor = Color.green;

    [Header("ONのときに表示するブロック")]
    public GameObject[] showWhenOn;

    [Header("ONのときに消すブロック")]
    public GameObject[] hideWhenOn;

    [Header("開始時の設定")]
    public bool switchIsOn = false;

    private bool playerIsOnSwitch = false;

    void Start()
    {
        UpdateBlocks();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (playerIsOnSwitch)
        {
            return;
        }

        playerIsOnSwitch = true;

        // ONとOFFを切り替える
        switchIsOn = !switchIsOn;

        // ブロックとスイッチの見た目を更新
        UpdateBlocks();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsOnSwitch = false;
        }
    }

    void UpdateBlocks()
    {
        // ONのときに表示するブロック
        foreach (GameObject block in showWhenOn)
        {
            if (block != null)
            {
                block.SetActive(switchIsOn);
            }
        }

        // ONのときに消すブロック
        foreach (GameObject block in hideWhenOn)
        {
            if (block != null)
            {
                block.SetActive(!switchIsOn);
            }
        }

        // スイッチの色を変更
        if (switchRenderer != null)
        {
            switchRenderer.color =
                switchIsOn ? onColor : offColor;
        }
    }
}