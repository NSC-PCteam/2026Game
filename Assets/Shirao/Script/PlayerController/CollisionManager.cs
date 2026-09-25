using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    // public TfControlle tfControlle;
    // public SupportItemEffect supportItemEffect;
    public string triggerPhase;
    [SerializeField] private int stageLayerNum;
    [SerializeField] private int enemyLayerNum;
    [SerializeField] private int waterLayerNum;
    public GameObject stepOnEnemy;
    public GameObject item;
    //public GameObject supportItem;
    public int getItemLayerNum;
    //public int getSupportItemLayerNum;
    

    void Start()
    {
        triggerPhase="air";
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer==waterLayerNum)
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
        if(collision.gameObject.layer==enemyLayerNum)
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
        else if(collision.gameObject.layer==stageLayerNum)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector2 normal=contact.normal;
                //Debug.Log($"noraml.x={normal.x}, normal.y={normal.y}");

                if(normal.x>0.5f)
                {
                    triggerPhase="leftWall";
                    if(normal.y>0.5f)
                    {
                        triggerPhase="groundAndLeftWall";
                    }
                    break;
                }
                else if(normal.x<-0.5f)
                {
                    triggerPhase="rightWall";
                    if(normal.y>0.5f)
                    {
                        triggerPhase="groundAndRightWall";
                    }
                    break;
                }
                else
                {
                    triggerPhase="stage";
                }
            }
        }
        else
        {
            getItemLayerNum=collision.gameObject.layer;
            item=collision.gameObject;
            triggerPhase="item";
            // foreach(int tfItemNum in tfControlle.tfItemLayerNum)
            // {
            //     if(collision.gameObject.layer==tfItemNum)
            //     {
            //         getTfItemLayerNum=tfItemNum;
            //         tfItem=collision.gameObject;
                    
            //         break;
            //     }
            // }

            // foreach(int supportItemLayerNum in supportItemEffect.supportItemLayerNum)
            // {
            //     if(collision.gameObject.layer==supportItemLayerNum)
            //     {
            //         getSupportItemLayerNum=supportItemLayerNum;
            //         supportItem=collision.gameObject;
            //         triggerPhase="supportItem";
            //         break;
            //     }
            // }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        triggerPhase="air";
    }
}
