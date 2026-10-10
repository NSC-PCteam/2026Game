using System.Collections.Generic;
using UnityEngine;

public class FloorSwitch : MonoBehaviour
{
    [Header("スイッチの見た目")]
    [SerializeField] private SpriteRenderer switchRenderer;
    [SerializeField] private Color offColor = Color.red;
    [SerializeField] private Color onColor = Color.green;

    [Header("ONのときに表示するブロック")]
    [SerializeField] private GameObject[] showWhenOn;

    [Header("ONのときに消すブロック")]
    [SerializeField] private GameObject[] hideWhenOn;

    [Header("開始時の設定")]
    [SerializeField] private bool switchIsOn = false;

    // 現在スイッチに触れているPlayerのColliderを記録する
    private readonly HashSet<Collider2D> playerColliders =
        new HashSet<Collider2D>();

    void Start()
    {
        UpdateBlocks();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");
        // 接触したColliderの親からPlayerRespawnを探す
        PlayerRespawn playerRespawn =
            other.GetComponentInParent<PlayerRespawn>();

        // PlayerRespawnが見つからなければPlayerではない
        if (playerRespawn == null)
        {
            return;
        }

        // 同じColliderがすでに登録されている場合は何もしない
        if (!playerColliders.Add(other))
        {
            return;
        }

        // 最初のPlayer Colliderが入ったときだけ切り替える
        if (playerColliders.Count == 1)
        {
            switchIsOn = !switchIsOn;
            UpdateBlocks();

            Debug.Log(
                $"FloorSwitchを切り替えました: " +
                $"{(switchIsOn ? "ON" : "OFF")}",
                this
            );
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        PlayerRespawn playerRespawn =
            other.GetComponentInParent<PlayerRespawn>();

        if (playerRespawn == null)
        {
            return;
        }

        playerColliders.Remove(other);
    }

    void UpdateBlocks()
    {
        // ONのときに表示するブロック
        if (showWhenOn != null)
        {
            foreach (GameObject block in showWhenOn)
            {
                if (block != null)
                {
                    block.SetActive(switchIsOn);
                }
            }
        }

        // ONのときに消すブロック
        if (hideWhenOn != null)
        {
            foreach (GameObject block in hideWhenOn)
            {
                if (block != null)
                {
                    block.SetActive(!switchIsOn);
                }
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