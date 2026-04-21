using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool proj = false;

    public GameObject pelletPrefab;
    public float shootInterval = 2f;
    public float pelletSpeed = 6f;

    public GameObject BloodPrefab;

    private float shootTimer;

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

    public float knockbackF = 5f;
    public float knockbackTime = 0.2f;

    private Vector2 knockbackV;
    private float knockbackTimer;

    private Rigidbody2D rb;

    // item drops
    public GameObject bombDrop;
    public GameObject arrowDrop;
    public GameObject healthDrop;
    public GameObject coinDrop;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = Vector3.one * 7f;
        shootTimer = shootInterval;
    }

    void FixedUpdate()
    {
        // knockback
        if (knockbackTimer > 0)
        {
            rb.MovePosition(rb.position + knockbackV * Time.fixedDeltaTime);
            knockbackTimer -= Time.fixedDeltaTime;
            return;
        }

        // chasing
        if (chasing && player != null)
        {
            anim.SetBool("chasing", true);

            Vector2 direction = player.position - transform.position;

            Vector3 scale = transform.localScale;
            if (direction.x > 0)
                scale.x = Mathf.Abs(scale.x);
            else if (direction.x < 0)
                scale.x = -Mathf.Abs(scale.x);

            transform.localScale = scale;

            Vector2 newPos = Vector2.MoveTowards(
                rb.position,
                player.position,
                speed * Time.fixedDeltaTime
            );

            rb.MovePosition(newPos);
        }
        else
        {
            // roaming
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

            Vector3 scale = transform.localScale;
            if (direction.x > 0)
                scale.x = Mathf.Abs(scale.x);
            else if (direction.x < 0)
                scale.x = -Mathf.Abs(scale.x);

            transform.localScale = scale;

            Vector2 newRoamPos = Vector2.MoveTowards(
                rb.position,
                roamTarget,
                roamSpeed * Time.fixedDeltaTime
            );

            rb.MovePosition(newRoamPos);
        }

        // shoot projectile
        if (proj && player != null)
        {
            shootTimer -= Time.fixedDeltaTime;

            if (shootTimer <= 0f)
            {
                ShootPellet();
                shootTimer = shootInterval;
            }
        }

        // death
        if (damageTaken >= health)
        {
            GameManager.Instance.RegisterEnemyKill();

            RollDrop();
            Destroy(gameObject);
        }
    }

    void ShootPellet()
    {
        if (player == null) return;

        Vector2 dir = (player.position - transform.position).normalized;

        GameObject pellet = Instantiate(
            pelletPrefab,
            transform.position,
            transform.rotation
        );

        Rigidbody2D prb = pellet.GetComponent<Rigidbody2D>();
        prb.linearVelocity = dir * pelletSpeed;

        Destroy(pellet, 5f);
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
        if (!other.CompareTag("Weapon") && !other.CompareTag("ArrowShot")) return;

        SoundManager.Instance.PlayEnemyHit();

        Vector2 dir = (transform.position - other.transform.position).normalized;

        knockbackV = dir * knockbackF;
        knockbackTimer = knockbackTime;

        damageTaken++;

        if (BloodPrefab != null)
        {
            Instantiate(BloodPrefab, transform.position, transform.rotation);
        }
    }

    void RollDrop()
    {
        int roll = Random.Range(0, 4);
        Vector3 spawnPos = transform.position;

        if (roll == 0)
            Instantiate(arrowDrop, spawnPos, transform.rotation);
        else if (roll == 1)
            Instantiate(bombDrop, spawnPos, transform.rotation);
        else if (roll == 2)
            Instantiate(healthDrop, spawnPos, transform.rotation);
        else if (roll == 3)
            Instantiate(coinDrop, spawnPos, transform.rotation);
    }
}