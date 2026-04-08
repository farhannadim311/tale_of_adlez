using UnityEngine;

public class sword : MonoBehaviour
{

    public float speed = 80f;
    public float attackDuration = .4f;

    private float attackTimer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(0f, 0f, speed*Time.deltaTime);

        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDuration)
        {
            Destroy(gameObject);

        }

        
    }
}
