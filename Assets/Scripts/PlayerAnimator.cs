using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement; // Stores x and y values
    public Transform interactor;

    void Update() // Don’t have physics movement here because framerate can change & alter physics
    {
        animator.SetBool("isWalking", true);
        movement.x = Input.GetAxisRaw("Horizontal"); // Gives value from -1 to 1 
        movement.y = Input.GetAxisRaw("Vertical");   // Left arrow key = -1, no input = 0, right arrow key = 1
                                                     // Works with WASD keys, arrow keys & controller input
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            interactor.localRotation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            interactor.localRotation = Quaternion.Euler(0, 0, -90);
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            interactor.localRotation = Quaternion.Euler(0, 0, 180);
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            interactor.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void FixedUpdate()  // Have physics movement here because it's updated on a fixed timer that matches the framerate (by default, it’s called 50/s)
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); // Assures constant movement speed
    }
}
