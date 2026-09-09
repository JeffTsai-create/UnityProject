using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        // 程式執行時自動抓取物件上的 Rigidbody 2D 元件
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 讀取鍵盤輸入
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // 使用抓取到的 rb 進行物理移動
        // 備註：若為舊版 Unity，請將 linearVelocity 改為 velocity
        rb.linearVelocity = movement.normalized * speed;
    }
}