using UnityEngine;

public class TestMove : MonoBehaviour
{
    public float speed = 5f;
    public Animator anim;

    private Rigidbody2D rb;

    private Vector2 input;
    private Vector2 lastMoveDir;

    // teleport lock (INSIDE PLAYER)
    public bool canTeleport = true;
    public float lockTimer = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // teleport cooldown
        if (!canTeleport)
        {
            lockTimer -= Time.deltaTime;

            if (lockTimer <= 0f)
                canTeleport = true;
        }

        // input
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        bool isMoving = input.sqrMagnitude > 0.01f;

        anim.SetFloat("Speed", input.sqrMagnitude);

        if (isMoving)
        {
            lastMoveDir = input.normalized;
            anim.SetFloat("MoveX", lastMoveDir.x);
            anim.SetFloat("MoveY", lastMoveDir.y);
        }
    }

    void FixedUpdate()
    {
        Vector2 move = input.normalized * speed;
        rb.linearVelocity = move;
    }
}