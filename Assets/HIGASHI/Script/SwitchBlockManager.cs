using UnityEngine;

public class SwitchBlockVisual : MonoBehaviour
{
    [Header("通常状態の見た目")]
    public GameObject solidVisual;

    [Header("非実体状態の破線画像")]
    public GameObject ghostVisual;

    [Header("足場の当たり判定")]
    public Collider2D blockCollider;

    public void SetBlockActive(bool isActive)
    {
        // 実体があるときだけ通常画像を表示
        if (solidVisual != null)
        {
            solidVisual.SetActive(isActive);
        }

        // 実体がないときだけ破線画像を表示
        if (ghostVisual != null)
        {
            ghostVisual.SetActive(!isActive);
        }

        // 実体があるときだけ当たり判定を有効化
        if (blockCollider != null)
        {
            blockCollider.enabled = isActive;
        }
    }
}