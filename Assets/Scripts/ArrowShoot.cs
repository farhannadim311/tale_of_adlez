using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    public float speed = 10f;
    public float durationExist = 5f;

    private Vector2 direction;
    private float lifeTimer;

    void Start()
    {
        lifeTimer = durationExist;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        // move arrow
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // destroy arrow
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }
//destroy arrow
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}