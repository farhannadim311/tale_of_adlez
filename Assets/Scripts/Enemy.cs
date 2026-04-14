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
    // apply knockback
    if (knockbackTimer > 0)
    {
        transform.position += knockbackVelocity * Time.deltaTime;
        knockbackTimer -= Time.deltaTime;
        return; // skip normal movement while being knocked back
    }

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

    if (damageTaken >= health)
        {
            Destroy(gameObject);
            //Instantiate(BloodSplatter, transform.position, transform.rotation);
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
    // ignore hits on child colliders (like detection radius)
    if (other.CompareTag("Weapon") && player != null && other.IsTouching(GetComponent<Collider2D>()))
    {
        float facing = Mathf.Sign(player.localScale.x);
        Vector3 dir = new Vector3(facing, 0, 0).normalized;

        knockbackVelocity = dir * knockbackForce;
        knockbackTimer = knockbackTime;
        damageTaken++;
    }
}
}