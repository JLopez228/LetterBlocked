using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Transform movePoint;

    public LayerMask whatStopsMovement;
    public float gridSize = 0.5f;

    void Start()
    {
        movePoint.parent = null;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);

            if (Mathf.Abs(input.x) == 1f) input.y = 0f; // only allow one axis at a time
            else if (Mathf.Abs(input.y) == 1f) input.x = 0f;

            if (input != Vector3.zero)
            {
                Vector3 targetPos = movePoint.position + input * gridSize;

                Collider2D hit = Physics2D.OverlapCircle(targetPos, .2f, whatStopsMovement);

                if (hit == null)
                {
                    // Nothing blocking, just move
                    movePoint.position = targetPos;
                }
                else if (hit.CompareTag("Pushable"))
                {
                    Vector3 pushTarget = targetPos + input * gridSize;
                    Collider2D pushHit = Physics2D.OverlapCircle(pushTarget, .2f, whatStopsMovement);

                    if (pushHit == null)
                    {
                        // Pushable object can be moved
                        hit.transform.position = pushTarget;
                        movePoint.position = targetPos;
                    }
                }
            }
        }
    }
}
