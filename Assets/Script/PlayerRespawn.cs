using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Vector3 respawnPoint;
    public float fallThreshold = -10f;

    void Start()
    {
        respawnPoint = transform.position;
    }

    void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            transform.position = respawnPoint;
        }
    }
}
