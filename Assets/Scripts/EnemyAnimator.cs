using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator animator;
    Vector2 movement;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isWalking", true);
        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
