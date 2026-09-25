using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;      // プレイヤー
    public float smoothSpeed = 0.1f; // カメラの追従速度
    public Vector3 offset;        // カメラのオフセット

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPos = player.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, smoothSpeed);
        transform.position = smoothPos;
    }
}
