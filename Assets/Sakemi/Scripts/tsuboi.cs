using UnityEngine;

public class tsuboi : MonoBehaviour
{
    float timer = 0f;
    [SerializeField] private GameObject spriteBPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >=0.4f)
        {
            Instantiate(spriteBPrefab, transform.position, Quaternion.identity);
            timer = 0f;
        }
    }
}