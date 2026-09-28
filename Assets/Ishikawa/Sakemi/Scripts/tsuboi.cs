using UnityEngine;

public class tsuboi : MonoBehaviour
{
    float timer = 0f;
    float bombang = 0f;
    [SerializeField] private GameObject spriteBPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >=0.05f)
        {
            GameObject clone = Instantiate(spriteBPrefab, transform.position, Quaternion.identity);

            ProjectileLauncher projectile =
            clone.GetComponent<ProjectileLauncher>();

            projectile.angle = 90f + 45f * Mathf.Sin(bombang * Mathf.Deg2Rad);

            bombang += 12f;

            timer = 0f;
        }
    }
}