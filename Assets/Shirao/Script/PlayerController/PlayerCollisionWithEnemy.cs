// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerCollisionWithEnemy : MonoBehaviour
// {
//     [SerializeField] private Transform player;
//     [SerializeField] private Rigidbody2D rb;
//     [SerializeField] private float jumpSpeed;
//     [SerializeField] private LayerMask enemyLayer;
//     [SerializeField] private float stepOnDistance;
//     [SerializeField] private float distance;

//     void Update()
//     {
//         RaycastHit2D stepOnHit1 = Physics2D.Raycast(new Vector2(player.position.x-0.5f, player.position.y-0.5f), Vector2.down, stepOnDistance, enemyLayer);
//         RaycastHit2D stepOnHit2 = Physics2D.Raycast(new Vector2(player.position.x, player.position.y-0.5f), Vector2.down, stepOnDistance, enemyLayer);
//         RaycastHit2D stepOnHit3 = Physics2D.Raycast(new Vector2(player.position.x+0.5f, player.position.y-0.5f), Vector2.down, stepOnDistance, enemyLayer);
        
//         RaycastHit2D rightHit1 = Physics2D.Raycast(new Vector2(player.position.x+0.5f, player.position.y+0.5f), Vector2.right, distance, enemyLayer);
//         RaycastHit2D rightHit2 = Physics2D.Raycast(new Vector2(player.position.x+0.5f, player.position.y), Vector2.right, distance, enemyLayer);
//         RaycastHit2D rightHit3 = Physics2D.Raycast(new Vector2(player.position.x+0.5f, player.position.y-0.5f), Vector2.right, distance, enemyLayer);

//         RaycastHit2D leftHit1 = Physics2D.Raycast(new Vector2(player.position.x-0.5f, player.position.y+0.5f), Vector2.left, distance, enemyLayer);
//         RaycastHit2D leftHit2 = Physics2D.Raycast(new Vector2(player.position.x-0.5f, player.position.y), Vector2.left, distance, enemyLayer);
//         RaycastHit2D leftHit3 = Physics2D.Raycast(new Vector2(player.position.x-0.5f, player.position.y-0.5f), Vector2.left, distance, enemyLayer);

//         RaycastHit2D upHit1 = Physics2D.Raycast(new Vector2(player.position.x+0.5f, player.position.y+0.5f), Vector2.up, distance, enemyLayer);
//         RaycastHit2D upHit2 = Physics2D.Raycast(new Vector2(player.position.x, player.position.y+0.5f), Vector2.up, distance, enemyLayer);
//         RaycastHit2D upHit3 = Physics2D.Raycast(new Vector2(player.position.x-0.5f, player.position.y+0.5f), Vector2.up, distance, enemyLayer);

//         if(stepOnHit1.collider != null)
//         {
//             rb.velocity = new Vector2(rb.velocity.x, 1)*jumpSpeed;
//             Destroy(stepOnHit1.collider.gameObject);
//         }
//         else if(stepOnHit2.collider != null)
//         {
//             rb.velocity = new Vector2(rb.velocity.x, 1)*jumpSpeed;
//             Destroy(stepOnHit2.collider.gameObject);
//         }
//         else if(stepOnHit3.collider != null)
//         {
//             rb.velocity = new Vector2(rb.velocity.x, 1)*jumpSpeed;
//             Destroy(stepOnHit3.collider.gameObject);
//         }

//         if(rightHit1.collider != null || rightHit2.collider != null || rightHit3.collider != null || leftHit1.collider != null || leftHit2.collider != null || leftHit3.collider != null || upHit1.collider != null || upHit2.collider != null || upHit3.collider != null)
//         {
//             Debug.Log("ゲームオーバー");
//         }
//     }
// }
