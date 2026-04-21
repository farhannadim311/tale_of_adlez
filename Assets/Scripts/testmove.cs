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
    public GameObject bombPrefab;
    public GameObject healParticles;

    public TMP_Text HealthText;
    public int health = 8;
    public int maxHealth = 8;
    public GameObject HealthUI;

    public float damageCooldown = 1f;
    private float invincibleTimer = 0f;

    private GameObject activeBow;

    public float attackCooldown = 0.5f;

    private Rigidbody2D rb;
    private Vector2 input;
    private Vector2 lastMoveDir = Vector2.up;

    private bool currentlyAtkng;
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
        TimersI();
        Movement();
        AttackInputs(); 

        //add bomb
        DropBomb();


        TryDrinkPotion();
        AttackLocking();
    }

    void TimersI()
    {
        if (invincibleTimer > 0f)
            invincibleTimer -= Time.deltaTime;

        attackCooldownTimer -= Time.deltaTime;
    }

    void Movement()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Speed", input.sqrMagnitude);

        if (input.sqrMagnitude > 0.01f && !currentlyAtkng)
            lastMoveDir = input.normalized;

        anim.SetFloat("MoveX", lastMoveDir.x);
        anim.SetFloat("MoveY", lastMoveDir.y);
    }

    void AttackInputs()
    {
        if (currentlyAtkng || attackCooldownTimer > 0f)
            return;

        if (Input.GetKeyDown(KeyCode.M))
        {
            SwordAttack();
            return;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            BowAttack();
        }
    }

    void SwordAttack()
    {
        SoundManager.Instance.PlaySwordSwing();
        currentlyAtkng = true;
        attackLockTimer = 0.35f;
        attackCooldownTimer = attackCooldown;

        bool facingUp = lastMoveDir.y > 0 && Mathf.Abs(lastMoveDir.y) > Mathf.Abs(lastMoveDir.x);

        Vector3 spawnPos = transform.position + (Vector3)lastMoveDir * 0.3f;
        float angle = Mathf.Atan2(lastMoveDir.y, lastMoveDir.x) * Mathf.Rad2Deg - 180f;

        GameObject sword = Instantiate(
            swordPrefab,
            spawnPos,
            Quaternion.Euler(0, 0, angle)
        );

        SpriteRenderer sr = sword.GetComponent<SpriteRenderer>();
            sr.sortingOrder = facingUp ? 4 : sr.sortingOrder; //set sorting order to 4 if facing up to make it look "behind" the player
    }

    void BowAttack()
    {
        if (!InventoryManager.Instance.UseArrow())
            return;

        SoundManager.Instance.PlayArrowShoot();

        bool facingUp = lastMoveDir.y > 0 && Mathf.Abs(lastMoveDir.y) > Mathf.Abs(lastMoveDir.x);

        Vector3 spawnPos = transform.position + (Vector3)lastMoveDir * 0.65f;
        float angle = Mathf.Atan2(lastMoveDir.y, lastMoveDir.x) * Mathf.Rad2Deg - 90f;

        activeBow = Instantiate(
            bowPrefab,
            spawnPos,
            Quaternion.Euler(0, 0, angle)
        );

        SpriteRenderer bowSR = activeBow.GetComponent<SpriteRenderer>();
            bowSR.sortingOrder = facingUp ? 4 : bowSR.sortingOrder;

        GameObject arrow = Instantiate(
            arrowPrefab,
            spawnPos,
            Quaternion.Euler(0, 0, angle)
        );

        ArrowShoot arrowScript = arrow.GetComponent<ArrowShoot>();
            arrowScript.SetDirection(lastMoveDir);

        currentlyAtkng = true;
        attackLockTimer = 0.35f;
        attackCooldownTimer = attackCooldown;
    }

    void DropBomb()
    {
        if (Input.GetKeyDown(KeyCode.C) && Time.timeScale == 1)
            BombPlace();
    }

    void BombPlace()
    {
        if (!InventoryManager.Instance.UseBomb())
            return;

        Instantiate(
            bombPrefab,
            transform.position,
            transform.rotation
        );
    }

    void TryDrinkPotion()
    {
        if (!Input.GetKeyDown(KeyCode.V))
            return;

        if (health >= maxHealth)
            return;

        if (InventoryManager.Instance.potions <= 0)
            return;

        InventoryManager.Instance.potions--;
        Instantiate(healParticles, transform.position, transform.rotation);

        health += 1;
        if (health > maxHealth)
            health = maxHealth;

        SoundManager.Instance.PlayDrinkPotion();
        UpdateHealthUI();

        InventoryManager.Instance.SendMessage("UpdateUI");
    }

    void AttackLocking() //cooldown
    {
        if (!currentlyAtkng) return;

        attackLockTimer -= Time.deltaTime;

        if (attackLockTimer <= 0f)
        {
            currentlyAtkng = false;

            if (activeBow != null)
            {
                Destroy(activeBow);
                activeBow = null;
            }
        }
    }

    void FixedUpdate()
    {
        if (currentlyAtkng)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = input.normalized * speed;
    }

    void UpdateHealthUI()
    {
        HealthText.text = "Health: " + health;
    }

    void TakeDamage(int amount)
    {
        if (invincibleTimer > 0f)
            return;

        health -= amount;
        SoundManager.Instance.PlayTakeDamage();
        UpdateHealthUI();

        invincibleTimer = damageCooldown;

        if (health <= 0)
            Die();
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            TakeDamage(1);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyProj"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("bomb"))
        {
            SoundManager.Instance.PlayItemGet();
            InventoryManager.Instance.AddBomb();
            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("arrow"))
        {
            SoundManager.Instance.PlayItemGet();
            InventoryManager.Instance.AddArrow();
            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("health"))
        {
            SoundManager.Instance.PlayItemGet();
            InventoryManager.Instance.AddPotion();
            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("Coin"))
        {
            SoundManager.Instance.PlayItemGet();
            InventoryManager.Instance.AddCoin();
            Destroy(other.gameObject);
            return;
        }
    }

    void Die()
    {
        SceneManager.LoadScene("GameOver");
    }
}