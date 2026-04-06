using UnityEngine;

public class TestMove : MonoBehaviour
{
    public float speed = 5f;
    public Animator anim;

    private Rigidbody2D rb;

    private Vector2 input;
    private Vector2 lastMoveDir;

  


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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