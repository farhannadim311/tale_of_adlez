using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;

    private Vector2 direction;
    private float lifeTimer;

    void Start()
    {
        lifeTimer = lifetime;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        // movement
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // lifetime
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}