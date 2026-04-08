using UnityEngine;

public class TestMove : MonoBehaviour
{
    public float speed = 5f;
    public Animator anim;
    public GameObject swordPrefab;

    private Rigidbody2D rb;
    private Vector2 input;
    private Vector2 lastMoveDir = Vector2.up; // START FACING UP

    private bool isAttacking;
    private float attackLockTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Input
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Speed", input.sqrMagnitude);

        // Update facing direction ONLY when moving
        if (input.sqrMagnitude > 0.01f && !isAttacking)
        {
            lastMoveDir = input.normalized;
        }

        // ALWAYS update animator with last direction
        anim.SetFloat("MoveX", lastMoveDir.x);
        anim.SetFloat("MoveY", lastMoveDir.y);

        // Attack
        if (Input.GetKeyDown(KeyCode.M) && !isAttacking)
        {
            isAttacking = true;
            attackLockTimer = 0.35f;

            int dir = GetAttackDir();
            anim.SetInteger("AttackDir", dir);
            anim.SetTrigger("Attack");

            Vector3 spawnPos = transform.position + (Vector3)lastMoveDir * 0.3f;
            float angle = Mathf.Atan2(lastMoveDir.y, lastMoveDir.x) * Mathf.Rad2Deg - 180f;

            GameObject sword = Instantiate(swordPrefab, spawnPos, Quaternion.Euler(0, 0, angle));

            // Render in front if attacking up
            SpriteRenderer sr = sword.GetComponent<SpriteRenderer>();
            if (sr != null && dir == 3)
            {
                sr.sortingOrder = 4;
            }
        }

        // Attack cooldown
        if (isAttacking)
        {
            attackLockTimer -= Time.deltaTime;
            if (attackLockTimer <= 0f)
            {
                isAttacking = false;
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
            return lastMoveDir.x > 0 ? 2 : 1; // right / left

        return lastMoveDir.y > 0 ? 3 : 0; // up / down
    }
}//