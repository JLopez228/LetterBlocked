using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Transform movePoint;
    public LayerMask whatStopsMovement;
    public float gridSize = 0.5f;

    [SerializeField]
    private int lives = 3;

    [Header("Heart UI")]
    public Image heart1;
    public Image heart2;
    public Image heart3;

    [Header("Game Over")]
    public GameObject gameOverPanel; 

    private void Start()
    {
        movePoint.parent = null;
        UpdateHeartsUI();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false); 
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);

            if (Mathf.Abs(input.x) == 1f) input.y = 0f;
            else if (Mathf.Abs(input.y) == 1f) input.x = 0f;

            if (input != Vector3.zero)
            {
                Vector3 targetPos = movePoint.position + input * gridSize;
                Collider2D hit = Physics2D.OverlapCircle(targetPos, .2f, whatStopsMovement);

                if (hit == null)
                {
                    movePoint.position = targetPos;
                }
                else if (hit.CompareTag("Pushable"))
                {
                    Vector3 pushTarget = targetPos + input * gridSize;
                    Collider2D pushHit = Physics2D.OverlapCircle(pushTarget, .2f, whatStopsMovement);

                    if (pushHit == null)
                    {
                        hit.transform.position = pushTarget;
                        movePoint.position = targetPos;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            lives--;
            UpdateHeartsUI();
            Debug.Log("Player hit! Lives left: " + lives);

            if (lives <= 0)
            {
                Debug.Log("Game Over!");
                enabled = false;

                if (gameOverPanel != null)
                    gameOverPanel.SetActive(true); 

                Time.timeScale = 0f; // Pause the game
            }
        }
    }

    private void UpdateHeartsUI()
    {
        if (heart1 != null) heart1.enabled = lives >= 1;
        if (heart2 != null) heart2.enabled = lives >= 2;
        if (heart3 != null) heart3.enabled = lives >= 3;
    }
}