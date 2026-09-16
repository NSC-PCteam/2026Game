using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    public GameObject bullet;
    public Transform player;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletCreateTime;
    public string bulletPhase;
    [SerializeField] private float distance;
    [SerializeField] private int enemyLayerNum;
    [SerializeField] private int stageLayerNum;
    private string playerDirection;
    private Rigidbody2D rb;
    private GameObject cloneBullet;
    private RaycastHit2D hit;

    void Start()
    {
        bulletPhase="waitCreate";
        playerDirection="right";
    }

    void Update()
    {
        if(bulletPhase=="create")
        {
            if(JudgeAttack())
            {
                int layerNum = hit.collider.gameObject.layer;

                if(layerNum==enemyLayerNum)
                {
                    Destroy(hit.collider.gameObject);
                    cloneBullet.SetActive(false);
                }
                else if(layerNum==stageLayerNum)
                {
                    cloneBullet.SetActive(false);
                }
            }
        }

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            playerDirection="right";
        }
        else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            playerDirection="left";
        }
    }

    public IEnumerator CreateBullet()
    {
        while(Input.GetKey(KeyCode.Return) || Input.GetMouseButton(0))
        {
            cloneBullet = Instantiate(bullet, new Vector3(player.position.x, player.position.y, 0), Quaternion.identity);
            rb = cloneBullet.GetComponent<Rigidbody2D>();
            cloneBullet.SetActive(true);
            bulletPhase="create";

            if(playerDirection=="right")
            {
                rb.linearVelocity = new Vector2(bulletSpeed,0);
            }
            else if(playerDirection=="left")
            {
                rb.linearVelocity = new Vector2(-bulletSpeed,0);
            }

            yield return new WaitForSeconds(bulletCreateTime);

            bulletPhase="notCreate";
            Destroy(cloneBullet);

        }
        bulletPhase="waitCreate";
    }

    public bool JudgeAttack()
    {
        if(playerDirection=="right")
        {
            hit = Physics2D.Raycast(cloneBullet.transform.position, Vector2.right, distance);
        }
        else if(playerDirection=="left")
        {
            hit = Physics2D.Raycast(cloneBullet.transform.position, Vector2.left, distance);
        }

        return hit.collider != null;
    }
}
