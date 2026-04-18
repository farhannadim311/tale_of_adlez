using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TestMove : MonoBehaviour
{
    public float speed = 5f;
    public Animator anim;

    public GameObject swordPrefab;
    public GameObject arrowPrefab;
    public GameObject bowPrefab;
    public TMP_Text HealthText;
    public int health = 5;
    public GameObject HealthUI;

    public float damageCooldown = 1f;
    private float invincibleTimer = 0f; 

    private GameObject activeBow;

    public float attackCooldown = 0.5f;

    private Rigidbody2D rb;
    private Vector2 input;
    private Vector2 lastMoveDir = Vector2.up;

    private bool isAttacking;
    private float attackLockTimer;
    private float attackCooldownTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
    HealthUI.SetActive(true);
    UpdateHealthUI();
    }

    void Update()
    {
        if (invincibleTimer > 0f)
{
    invincibleTimer -= Time.deltaTime;
}
       
        attackCooldownTimer -= Time.deltaTime;

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Speed", input.sqrMagnitude);

        if (input.sqrMagnitude > 0.01f && !isAttacking)
        {
            lastMoveDir = input.normalized;
        }

        anim.SetFloat("MoveX", lastMoveDir.x);
        anim.SetFloat("MoveY", lastMoveDir.y);

        // =========================
        // SWORD ATTACK
        // =========================
        if (Input.GetKeyDown(KeyCode.M) && !isAttacking && attackCooldownTimer <= 0f)
        {
            SoundManager.Instance.PlaySwordSwing();

            isAttacking = true;
            attackLockTimer = 0.35f;
            attackCooldownTimer = attackCooldown;

            int dir = GetAttackDir();

            Vector3 spawnPos = transform.position + (Vector3)lastMoveDir * 0.3f;
            float angle = Mathf.Atan2(lastMoveDir.y, lastMoveDir.x) * Mathf.Rad2Deg - 180f;

            GameObject sword = Instantiate(
                swordPrefab,
                spawnPos,
                Quaternion.Euler(0, 0, angle)
            );

            SpriteRenderer sr = sword.GetComponent<SpriteRenderer>();
            if (sr != null && dir == 3)
            {
                sr.sortingOrder = 4;
            }
        }

        // =========================
        // BOW ATTACK
        // =========================
        if (Input.GetKeyDown(KeyCode.N) && !isAttacking && attackCooldownTimer <= 0f)
        {
            SoundManager.Instance.PlayArrowShoot();
            int dir = GetAttackDir();

            Vector3 spawnPos = transform.position + (Vector3)lastMoveDir * 0.65f;
            float angle = Mathf.Atan2(lastMoveDir.y, lastMoveDir.x) * Mathf.Rad2Deg - 90f;

            activeBow = Instantiate(
                bowPrefab,
                spawnPos,
                Quaternion.Euler(0, 0, angle)
            );

            SpriteRenderer bowSR = activeBow.GetComponent<SpriteRenderer>();
            if (bowSR != null)
            {
                bowSR.sortingOrder = (dir == 3) ? 4 : 10;
            }

            GameObject arrow = Instantiate(
                arrowPrefab,
                spawnPos,
                Quaternion.Euler(0, 0, angle)
            );

            ArrowShoot arrowScript = arrow.GetComponent<ArrowShoot>();
            if (arrowScript != null)
            {
                arrowScript.SetDirection(lastMoveDir);
            }

            isAttacking = true;
            attackLockTimer = 0.35f;
            attackCooldownTimer = attackCooldown;
        }

        // =========================
        // ATTACK TIMER
        // =========================
        if (isAttacking)
        {
            attackLockTimer -= Time.deltaTime;

            if (attackLockTimer <= 0f)
            {
                isAttacking = false;

                if (activeBow != null)
                {
                    Destroy(activeBow);
                    activeBow = null;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (isAttacking)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = input.normalized * speed;
    }

    int GetAttackDir()
    {
        if (Mathf.Abs(lastMoveDir.x) > Mathf.Abs(lastMoveDir.y))
            return lastMoveDir.x > 0 ? 2 : 1;

        return lastMoveDir.y > 0 ? 3 : 0;
    }
    void UpdateHealthUI()
{
    HealthText.text = "Health: " + health;
}

void OnCollisionStay2D(Collision2D other)
{
    if (!other.gameObject.CompareTag("Enemy")) return;
    if (invincibleTimer > 0f) return;

    Debug.Log("Player damaged by: " + other.gameObject.name);

    health--;
    SoundManager.Instance.PlayTakeDamage();
    UpdateHealthUI();

    invincibleTimer = damageCooldown;

    if (health <= 0)
    {
        Debug.Log("Player dead");
        Die();
    }
}

void Die()
    {
        SceneManager.LoadScene("GameOver");

    }

}