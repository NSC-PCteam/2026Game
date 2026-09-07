using UnityEngine;

public class Ishikawatest: MonoBehaviour
{
    public float speed = 5.0f;
    void Update()
    {
        transform.Translate(Vector3.right*speed*Time.deltaTime);
    }
}
