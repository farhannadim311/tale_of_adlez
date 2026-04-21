using UnityEngine;
//child object of enemy objects to decide to chase player
public class DetectionRadius : MonoBehaviour
{
    private Enemy enemy;

    void Awake() 
    {
        enemy = GetComponentInParent<Enemy>();
    }

    //notifiers to enemy

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.PlayerEntered(other.transform); 
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.PlayerLeft();
        }
    }
}