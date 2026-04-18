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

    private Vector2 knockbackVelocity;
    private float knockbackTimer;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // =========================
        // KNOCKBACK
        // =========================
        if (knockbackTimer > 0)
        {
            rb.MovePosition(rb.position + knockbackVelocity * Time.fixedDeltaTime);
            knockbackTimer -= Time.fixedDeltaTime;
            return;
        }

        // =========================
        // CHASE PLAYER
        // =========================
        if (chasing && player != null)
        {
            anim.SetBool("chasing", true);

            Vector2 direction = player.position - transform.position;

            if (direction.x > 0)
                transform.localScale = new Vector3(7, 7, 1);
            else if (direction.x < 0)
                transform.localScale = new Vector3(-7, 7, 1);

            Vector2 newPos = Vector2.MoveTowards(
                rb.position,
                player.position,
                speed * Time.fixedDeltaTime
            );

            rb.MovePosition(newPos);
        }
        else
        {
            // =========================
            // ROAM
            // =========================
            anim.SetBool("chasing", false);

            roamTimer -= Time.fixedDeltaTime;

            if (roamTimer <= 0f)
            {
                roamTarget = transform.position + new Vector3(
                    Random.Range(-roamRadius, roamRadius),
                    Random.Range(-roamRadius, roamRadius),
                    0f
                );

                roamTimer = roamChangeTime;
            }

            Vector2 direction = roamTarget - transform.position;

            if (direction.x > 0)
                transform.localScale = new Vector3(7, 7, 1);
            else if (direction.x < 0)
                transform.localScale = new Vector3(-7, 7, 1);

            Vector2 newRoamPos = Vector2.MoveTowards(
                rb.position,
                roamTarget,
                roamSpeed * Time.fixedDeltaTime
            );

            rb.MovePosition(newRoamPos);
        }

        // =========================
        // DEATH
        // =========================
        if (damageTaken >= health)
        {
            GameManager.Instance.RegisterEnemyKill();
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
    if (!other.CompareTag("Weapon") && !other.CompareTag("Arrow")) return;

    SoundManager.Instance.PlayEnemyHit();

    Vector2 dir = (transform.position - other.transform.position).normalized;

    knockbackVelocity = dir * knockbackForce;
    knockbackTimer = knockbackTime;

    damageTaken++;
}
}