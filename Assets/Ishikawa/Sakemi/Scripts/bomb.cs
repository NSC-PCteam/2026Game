using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileLauncher : MonoBehaviour
{
    float timer = 0f;

    [SerializeField] private float speed = 20f;

    public float angle = 60f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        speed = 20f;

        float rad = angle * Mathf.Deg2Rad;

        Vector2 direction = new Vector2(
            Mathf.Cos(rad),
            Mathf.Sin(rad)
        );

        rb.linearVelocity = direction * speed;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 5f)
        {
            Destroy(gameObject);
        }
    }
}