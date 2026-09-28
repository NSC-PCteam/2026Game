using UnityEngine;

public class RayCastForSmall : MonoBehaviour
{
    public Transform playerTransform;
    public float raycastDistance;
    private float upDistance;
    private float downDistance;
    public float biggerDistance;

    void Update()
    {
        RaycastHit2D upHit = Physics2D.Raycast(playerTransform.position, Vector2.up, raycastDistance);
        RaycastHit2D downHit = Physics2D.Raycast(playerTransform.position, Vector2.down, raycastDistance);
        if (upHit.collider != null)
        {
            //Debug.Log("Hit: " + upHit.collider.name);
            if(upHit.collider.CompareTag("Ground"))
            {
                upDistance = upHit.distance;
                //Debug.Log("Player is on the ground.");
            }
        }

        if(downHit.collider != null)
        {
            //Debug.Log("Hit: " + downHit.collider.name);
            if(downHit.collider.CompareTag("Ground"))
            {
                downDistance = downHit.distance;
                //Debug.Log("Player is on the ground.");
            }
        }

        if(upDistance<downDistance)
        {
            if(upDistance<0.5f)
            {
                biggerDistance = 0.5f-upDistance;
            }
            else
            {
                biggerDistance = 0f;
            }
        }
        else if(downDistance<upDistance)
        {
            if(downDistance<0.5f)
            {
                biggerDistance = downDistance-0.5f;
            }
            else
            {
                biggerDistance = 0f;
            }
        }
        else
        {
            if(upDistance<0.5f)
            {
                biggerDistance = 0.5f-upDistance;
            }
            else
            {
                biggerDistance = 0f;
            }
        }
    }
}
