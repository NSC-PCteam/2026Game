using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlle : MonoBehaviour
{
    public TfControlle tfControlle;
    public SupportItemEffect supportItemEffect;
    public CollisionManager downCollision;
    public CollisionManager upAndSideCollision;
    public CollisionManager waterTrigger;
    public BulletAttack bulletAttack;
    public Rigidbody2D rb;
    public int lifePoint;
    [SerializeField] private float speed;
    [SerializeField] private float airSpeed;
    [SerializeField] private float waterSpeed;
    [SerializeField] private float waterAcceleration;
    [SerializeField] private float waterDeceleration;
    [SerializeField] private float floatSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float invincibleTime;
    private Vector2 targetVelocity;
    private Vector2 waterVelocity;
    [SerializeField] private bool isInvincible;

    void Start()
    {
        tfControlle.tfPhase = "idle";
        isInvincible=false;
    }

    void Update()
    {
        //移動処理
        if((waterTrigger.triggerPhase=="water") && tfControlle.tfPhase!="swim")
        {
            targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal")*waterSpeed, targetVelocity.y);
            float acceleration;

            if (targetVelocity.sqrMagnitude > 0f)
            {
                acceleration = waterAcceleration;
            }
            else
            {
                acceleration = waterDeceleration;
            }

            if(upAndSideCollision.triggerPhase=="rightWall")
            {
                if(targetVelocity.x>0f)
                {
                    waterVelocity.x=0f;
                }
            }
            else if(upAndSideCollision.triggerPhase=="leftWall")
            {
                if(targetVelocity.x<0f)
                {
                    waterVelocity.x=0f;
                }
            }

            waterVelocity = Vector2.MoveTowards(waterVelocity,targetVelocity,acceleration*Time.fixedDeltaTime);
            rb.linearVelocity = new Vector2(waterVelocity.x, waterVelocity.y+floatSpeed);
        }
        else if(downCollision.triggerPhase=="air")
        {
            waterVelocity = rb.linearVelocity;
            rb.linearVelocity=new Vector2(Input.GetAxisRaw("Horizontal")*airSpeed,rb.linearVelocity.y);
        }
        else if(waterTrigger.triggerPhase!="water" && downCollision.triggerPhase!="air")
        {
            waterVelocity = rb.linearVelocity;
            rb.linearVelocity=new Vector2(Input.GetAxisRaw("Horizontal")*speed,rb.linearVelocity.y);
        }

        //ジャンプ処理
        if(Input.GetKey(KeyCode.Space))
        {
            if(downCollision.triggerPhase=="stage")
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
            }
        }

        //攻撃処理（銃）
        if(Input.GetKey(KeyCode.Return) || Input.GetMouseButton(0))
        {
            if(bulletAttack.bulletPhase=="waitCreate")
            {
                StartCoroutine(bulletAttack.CreateBullet());
            }
        }

        //攻撃処理（踏みつけ）
        if(downCollision.triggerPhase=="stepOnEnemy")
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 1)*jumpSpeed;
            downCollision.stepOnEnemy.SetActive(false);
        }

        //攻撃処理（無敵）
        if(supportItemEffect.invinciblePhase=="invincible" && downCollision.triggerPhase=="stepOnEnemy")
        {
            downCollision.stepOnEnemy.SetActive(false);
        }
        else if(supportItemEffect.invinciblePhase=="invincible" && upAndSideCollision.triggerPhase=="damaged")
        {
            upAndSideCollision.stepOnEnemy.SetActive(false);
        }

        //被攻撃処理
        if((upAndSideCollision.triggerPhase=="damaged" && !isInvincible && supportItemEffect.invinciblePhase!="invincible"))
        {
            StartCoroutine(InvincibleTime());
            Debug.Log("U a damaged by enemy");
            lifePoint--;
            if(lifePoint<=0)
            {
                lifePoint=0;
                Debug.Log("game is over");
            }
        }

        //アイテム取得処理
        if(upAndSideCollision.triggerPhase=="item")
        {
            foreach(int itemNum in tfControlle.tfItemLayerNum)
            {
                if(upAndSideCollision.getItemLayerNum==itemNum)
                {
                    upAndSideCollision.item.SetActive(false);
                    tfControlle.TfItemGet(itemNum);
                    break;
                }
            }

            foreach(int itemNum in supportItemEffect.supportItemLayerNum)
            {
                if(upAndSideCollision.getItemLayerNum==itemNum)
                {
                    upAndSideCollision.item.SetActive(false);
                    StartCoroutine(supportItemEffect.SupportEffect(itemNum));
                    break;
                }
            }
        }
        else if(downCollision.triggerPhase=="item")
        {
            foreach(int itemNum in tfControlle.tfItemLayerNum)
            {
                if(downCollision.getItemLayerNum==itemNum)
                {
                    downCollision.item.SetActive(false);
                    tfControlle.TfItemGet(itemNum);
                    break;
                }
            }

            foreach(int itemNum in supportItemEffect.supportItemLayerNum)
            {
                Debug.Log($"downCollision.getItemLayerNum={downCollision.getItemLayerNum}");
                if(downCollision.getItemLayerNum==itemNum)
                {
                    downCollision.item.SetActive(false);
                    StartCoroutine(supportItemEffect.SupportEffect(itemNum));
                    break;
                }
            }
        }

        //壁引っかかり処理
        if(upAndSideCollision.triggerPhase=="rightWall")
        {
            if(rb.linearVelocity.x>0f) rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        else if(upAndSideCollision.triggerPhase=="leftWall")
        {
            if(rb.linearVelocity.x<0f) rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    private IEnumerator InvincibleTime()
    {
        isInvincible=true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible=false;
    }
}
