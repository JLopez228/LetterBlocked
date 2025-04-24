using UnityEngine;

public class PushableObject : MonoBehaviour
{
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        Vector2 vel = rb.linearVelocity;

        // Only allow movement on one axis
        if (Mathf.Abs(vel.x) > Mathf.Abs(vel.y))
            rb.linearVelocity = new Vector2(vel.x, 0);
        else
            rb.linearVelocity = new Vector2(0, vel.y);

        //// Snap to grid when nearly stopped
        //if (rb.linearVelocity.magnitude < 0.01f)
        //{
        //    rb.linearVelocity = Vector2.zero;
        //    rb.linearVelocity = SnapToGrid(rb.position);
        //}
    }
    Vector2 SnapToGrid(Vector2 position, float gridSize = 0.5f)
    {
        return new Vector2(
            Mathf.Round(position.x / gridSize) * gridSize,
            Mathf.Round(position.y / gridSize) * gridSize
        );
    }

}
