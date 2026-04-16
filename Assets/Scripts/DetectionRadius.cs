using UnityEngine;

public class DetectionRadius : MonoBehaviour
{
    private Enemy enemy;

    void Awake() 
    {
        enemy = GetComponentInParent<Enemy>();

        if (enemy == null)
        {
            Debug.LogError("Enemy not found on parent!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && enemy != null)
        {
            enemy.PlayerEntered(other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && enemy != null)
        {
            enemy.PlayerLeft();
        }
    }
}