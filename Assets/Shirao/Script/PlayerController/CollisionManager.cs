using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    // public TfControlle tfControlle;
    // public SupportItemEffect supportItemEffect;
    public string triggerPhase;
    // [SerializeField] private int stageLayerNum;
    // [SerializeField] private int enemyLayerNum;
    // [SerializeField] private int waterLayerNum;
    public GameObject stepOnEnemy;
    public GameObject item;
    //public GameObject supportItem;
    //public int getItemLayerNum;
    //public int getSupportItemLayerNum;
    

    void Start()
    {
        triggerPhase="air";
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Stage"))
        {
            triggerPhase="Stage";
        }
        else if(collision.gameObject.CompareTag("Water"))
        {
            triggerPhase="water";
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        triggerPhase="air";
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector2 normal=contact.normal;

                if(normal.y>0.5f)
                {
                    stepOnEnemy=collision.gameObject;
                    triggerPhase="stepOnEnemy";
                    break;
                }
                else
                {
                    stepOnEnemy=collision.gameObject;
                    triggerPhase="damaged";
                }
            }
        }
        else if(collision.gameObject.CompareTag("SuperEnemy"))
        {
            triggerPhase="damaged";
        }
        else if(collision.gameObject.CompareTag("Stage"))
        {
            bool isGrounded = false;
            bool isRightWall = false;
            bool isLeftWall = false;
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector2 normal=contact.normal;
                //Debug.Log($"noraml.x={normal.x}, normal.y={normal.y}");

                if(normal.y>0.5f)
                {
                    isGrounded=true;
                }
                else if(normal.x>0.5f)
                {
                    isLeftWall = true;
                }
                else if (normal.x<-0.5f)
                {
                    isRightWall=true;
                }
                // if((normal.x<0.5f && normal.y>0.5f) || (normal.x>-0.5f && normal.y>0.5f))
                // {
                //     triggerPhase="stage";
                //     break;
                // }
                // if(normal.x>0.5f)
                // {
                //     triggerPhase="leftWall";
                //     if(normal.y>0.5f)
                //     {
                //         triggerPhase="groundAndLeftWall";
                //     }
                //     break;
                // }
                // else if(normal.x<-0.5f)
                // {
                //     triggerPhase="rightWall";
                //     if(normal.y>0.5f)
                //     {
                //         triggerPhase="groundAndRightWall";
                //     }
                //     break;
                // }
            }
            if (isGrounded && isLeftWall)
            {
                triggerPhase = "groundAndLeftWall";
            }
            else if (isGrounded && isRightWall)
            {
                triggerPhase = "groundAndRightWall";
            }
            else if (isGrounded)
            {
                triggerPhase = "stage";
            }
            else if (isLeftWall)
            {
                triggerPhase = "leftWall";
            }
            else if (isRightWall)
            {
                triggerPhase = "rightWall";
            }
        }
        else
        {
            //getItemLayerNum=collision.gameObject.layer;
            item=collision.gameObject;
            triggerPhase="item";
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        triggerPhase="air";
    }
}
