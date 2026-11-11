using UnityEngine;


public class Enemy_Combat : MonoBehaviour
{
    public int damage = 1;
    [Header("擊退設定")]
    public float knockbackForce = 10f;    
    public float knockbackDuration = 0.2f;
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.CompareTag("Player"))
    {
        PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();
       
        if (playerScript != null)
        {
            // 傳入：敵人的Transform, 擊退力道(例如10f), 擊退持續時間(例如0.2秒)
            playerScript.ApplyKnockback(transform, knockbackForce, knockbackDuration);
        }
       
        // 這裡可以繼續寫你原本扣玩家血量的程式碼
    }
        collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
    }
}
