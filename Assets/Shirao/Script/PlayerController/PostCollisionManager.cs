// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerCollision : MonoBehaviour
// {
//     public string collisionPhase;
//     public string triggerPhase;
//     //[SerializeField] private int stageLayerNum;
//     [SerializeField] private int waterLayerNum;
//     [SerializeField] private int enemyLayerNum;

//     void Start()
//     {
//         collisionPhase = "air";
//         triggerPhase = "air";
//     }

//     public void OnCollisionStay2D(Collision2D collision)
//     {
//         if(collision.gameObject.layer==enemyLayerNum)
//         {
//             collisionPhase="enemy";
//         }
//     }
    
//     private void OnCollisionExit2D(Collision2D collision)
//     {
//         collisionPhase="air";
//     }

//     public void OnTriggerStay2D(Collider2D collision)
//     {
//         if(collision.gameObject.layer==waterLayerNum)
//         {
//             triggerPhase="water";
//         }
//     }

//     private void OnTriggerExit2D(Collider2D collision)
//     {
//         triggerPhase="air";
//     }
// }
