using UnityEngine;

public class SwitchBlockVisual : MonoBehaviour
{
    public GameObject solidVisual;
    public GameObject ghostVisual;
    public Collider2D blockCollider;

    public void SetBlockActive(bool isActive)
    {
        // 実体があるときは通常画像を表示
        if (solidVisual != null)
        {
            Debug.Log(isActive);
            Debug.Log("実態");
            solidVisual.SetActive(isActive);
        }

        // 実体がないときは破線画像を表示
        if (ghostVisual != null)
        {
            Debug.Log("影");
            ghostVisual.SetActive(!isActive);
        }

        // 実体があるときだけ乗れるようにする
        // if (blockCollider != null)
        // {
        //     blockCollider.enabled = isActive;
        // }
    }
}