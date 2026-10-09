using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] public float angle = 60f;

    private float timer = 0f;
    private Rigidbody2D rb;
    private Collider2D myCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        float rad = angle * Mathf.Deg2Rad;

        Vector2 direction = new Vector2(
            Mathf.Cos(rad),
            Mathf.Sin(rad)
        );

        rb.linearVelocity = direction * speed;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        bool isGrounded = Physics2D.OverlapBox(
            myCollider.bounds.center,
            myCollider.bounds.size,
            0f,
            LayerMask.GetMask("Ground")
        );

        if (timer >= 5f || isGrounded)
        {
            Destroy(gameObject);
        }
    }
}
