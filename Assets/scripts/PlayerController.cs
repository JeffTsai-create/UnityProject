using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public int FacingDirection=1;
    public Animator animator;
    public Player_Combat playerCombat;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        // 程式執行時自動抓取物件上的 Rigidbody 2D 元件
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("movement.x", movement.x);
        animator.SetFloat("movement.y", movement.y);
        animator.SetFloat("Speed", movement.magnitude);

        // 記住最後面向
        if (movement.x > 0)
        {
            FacingDirection = 1;
        }
        else if (movement.x < 0)
        {
            FacingDirection = -1;
        }

        bool isRunning = movement.sqrMagnitude > 0.01f;

        if (isRunning)
        {
            // Run 素材原本朝左
            spriteRenderer.flipX = FacingDirection == 1;
        }
        else
        {
            // Idle 素材原本朝右
            spriteRenderer.flipX = FacingDirection == -1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerCombat.Attack();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * speed;
    }

}