using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public int FacingDirection=1;
    public Animator animator;
    void Start()
    {
        // 程式執行時自動抓取物件上的 Rigidbody 2D 元件
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 讀取鍵盤輸入
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("movement.x", movement.x);
        animator.SetFloat("movement.y", movement.y);
        animator.SetFloat("Speed", movement.magnitude);
    }

    void FixedUpdate()
    {
        // 使用抓取到的 rb 進行物理移動
        // 備註：若為舊版 Unity，請將 linearVelocity 改為 velocity
        if(movement.x >0&& transform.localScale.x>0 || 
        movement.x<0 && transform.localScale.x<0   )
        {
            Flip();
        }
        rb.linearVelocity = movement.normalized * speed;
    }
    void Flip(){
        FacingDirection *=-1;
        transform.localScale= new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
    }
}