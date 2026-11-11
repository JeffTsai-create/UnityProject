using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public int FacingDirection=1;
    public Animator animator;
    public Player_Combat playerCombat;
    private SpriteRenderer spriteRenderer;
    private bool isKnockedBack = false;
    void Start()
    {
        // 程式執行時自動抓取物件上的 Rigidbody 2D 元件
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 如果正在被擊退，直接 return，暫停玩家的移動控制
        if (isKnockedBack) return;
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
        if (isKnockedBack) return;
        rb.linearVelocity = movement.normalized * speed;
        // 新增：如果正在被擊退，不要執行一般移動，否則會覆蓋掉擊退力道
        

    }
    public void ApplyKnockback(Transform attackerTransform, float knockbackForce, float knockbackDuration)
    {
        StartCoroutine(KnockbackRoutine(attackerTransform, knockbackForce, knockbackDuration));
    }
    private IEnumerator KnockbackRoutine(Transform attackerTransform, float force, float duration)
    {
        isKnockedBack = true;


        // 計算擊退方向：玩家座標減去敵人座標 = 遠離敵人的方向
        Vector2 knockbackDirection = (transform.position - attackerTransform.position).normalized;


        // 給予瞬間的擊退速度
        rb.linearVelocity = knockbackDirection * force;


        // 等待擊退時間結束
        yield return new WaitForSeconds(duration);


        // 恢復正常狀態，把速度歸零避免滑行
        rb.linearVelocity = Vector2.zero;
        isKnockedBack = false;
    }


}