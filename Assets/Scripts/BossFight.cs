using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFight : MonoBehaviour
{
    public int health = 10;

    public float moveSpeed = 2f;
    public float moveRange = 5f;

    private Vector3 startPosition;
    private int movingDirection = 1;
    private float changeDirecTimer;

    public GameObject projectilePrefab;
    public float projSpeed = 6f;
    public float shootInterval = 1.5f;

    private float timerShoot;

    public Transform player; // drag player here in Inspector
    private Rigidbody2D rb;

    public GameObject winPanel;
    public string nextSceneName = "WinScene";
    private bool isDead = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        timerShoot = shootInterval;
        changeDirecTimer = Random.Range(1f, 3f);
    }

    void Start()
    {
        SoundManager.Instance.SetMusicState(5);
    }

    void Update()
    {
        if (isDead && Input.GetKeyDown(KeyCode.B))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;

        Patrol();
        ShootPlayer();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) 
        {
        return;
        }

        if (!other.CompareTag("ArrowShot")) 
        {
        return;
        }

        health--;
        SoundManager.Instance.PlayEnemyHit();

        if (health <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;

        winPanel.SetActive(true); //end message active

        Time.timeScale = 0f;
    }

    void Patrol()
{
    changeDirecTimer -= Time.fixedDeltaTime;

    float leftMost = startPosition.x - moveRange; // the movement boundaries of boss
    float rightMost = startPosition.x + moveRange;

    Vector3 bossPosition = transform.position;

    if (changeDirecTimer <= 0f)
    {
        movingDirection = Random.value > 0.5f ? 1 : -1; // Randomly chooses left (-1) or right (1) movement direction
        changeDirecTimer = Random.Range(1f, 3f);
    }

    bossPosition.x += movingDirection * moveSpeed * Time.fixedDeltaTime;

    if (bossPosition.x <= leftMost)
    {
        bossPosition.x = leftMost;
        movingDirection = 1;
    }
    else if (bossPosition.x >= rightMost)
    {
        bossPosition.x = rightMost;
        movingDirection = -1;
    }

    rb.MovePosition(bossPosition);
}

    void ShootPlayer()
    {

        timerShoot -= Time.fixedDeltaTime;

        if (timerShoot <= 0f)
        {
            ShootPellet();
            timerShoot = shootInterval;
        }
    }

    void ShootPellet()
    {
        Vector2 dir = (player.position - transform.position).normalized;

        GameObject pellet = Instantiate(
            projectilePrefab,
            transform.position,
            transform.rotation
        );

        Rigidbody2D prb = pellet.GetComponent<Rigidbody2D>();

        if (prb != null)
        {
            prb.linearVelocity = dir * projSpeed;
        }

        Destroy(pellet, 5f);
    }
}