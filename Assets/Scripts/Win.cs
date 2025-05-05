using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameObject winPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pushable"))
        {
            winPanel.SetActive(true); 
            Time.timeScale = 0f; 
        }
    }
}