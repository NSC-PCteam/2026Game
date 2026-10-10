using UnityEngine;
using System.Collections;

public class bkht : MonoBehaviour
{
    [SerializeField] private float expandDuration = 0.5f;
    [SerializeField] private float disappearDuration = 0.2f;

    private void Start()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
        // 0% → 150%
        yield return ScaleTo(Vector3.one * 1.5f, 0.1f);

        // 150% → 100%
        yield return ScaleTo(Vector3.one, 0.6f);

        // 100% → 0%
        yield return ScaleTo(Vector3.zero, 0.1f);

        // 動作終了後に破棄
        Destroy(gameObject);
    }

    private IEnumerator ScaleTo(Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            transform.localScale =
                Vector3.Lerp(startScale, targetScale, t);

            yield return null;
        }

        transform.localScale = targetScale;
    }
}