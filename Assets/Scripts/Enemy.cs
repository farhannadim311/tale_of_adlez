using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float roamSpeed = 1.5f;

    public float roamRadius = 3f;
    public float roamChangeTime = 2f;

    public int health;
    public int damageTaken = 0;

    private Transform player;
    private bool chasing = false;

    private Vector3 roamTarget;
    private float roamTimer;

    public Animator anim;

    public float knockbackForce = 5f;
    public float knockbackTime = 0.2f;

    private Vector3 knockbackVelocity;
    private float knockbackTimer;

    void Update()
    {
        // =========================
        // KNOCKBACK OVERRIDE
        // =========================
        if (knockbackTimer > 0)
        {
            transform.position += knockbackVelocity * Time.deltaTime;
            knockbackTimer -= Time.deltaTime;
            return;
        }

        // =========================
        // CHASE PLAYER
        // =========================
        if (chasing && player != null)
        {
            anim.SetBool("chasing", true);

            Vector3 direction = player.position - transform.position;

            if (direction.x > 0)
                transform.localScale = new Vector3(7, 7, 1);
            else if (direction.x < 0)
                transform.localScale = new Vector3(-7, 7, 1);

            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );
        }
        else
        {
            // =========================
            // ROAM
            // =========================
            anim.SetBool("chasing", false);

            roamTimer -= Time.deltaTime;

            if (roamTimer <= 0f)
            {
                roamTarget = transform.position + new Vector3(
                    Random.Range(-roamRadius, roamRadius),
                    Random.Range(-roamRadius, roamRadius),
                    0f
                );

                roamTimer = roamChangeTime;
            }

            Vector3 direction = roamTarget - transform.position;

            if (direction.x > 0)
                transform.localScale = new Vector3(7, 7, 1);
            else if (direction.x < 0)
                transform.localScale = new Vector3(-7, 7, 1);

            transform.position = Vector3.MoveTowards(
                transform.position,
                roamTarget,
                roamSpeed * Time.deltaTime
            );
        }

        // =========================
        // DEATH
        // =========================
        if (damageTaken >= health)
        {
            Destroy(gameObject);
        }
    }

    public void PlayerEntered(Transform playerTransform)
    {
        player = playerTransform;
        chasing = true;
    }

    public void PlayerLeft()
    {
        chasing = false;
        player = null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weapon") && player != null && other.IsTouching(GetComponent<Collider2D>()))
        {
            // FIXED KNOCKBACK DIRECTION
            Vector3 dir = (transform.position - player.position).normalized;

            knockbackVelocity = dir * knockbackForce;
            knockbackTimer = knockbackTime;

            damageTaken++;
        }
    }
}