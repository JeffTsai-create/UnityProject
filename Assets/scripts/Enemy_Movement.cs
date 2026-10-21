using UnityEngine;
using System.Collections;
public class Enemy_Movement : MonoBehaviour
{   
    [Header("移動與攻擊數值設定")]
    public float normalSpeed = 5f;    // 正常追逐速度
    public float lungeSpeed = 15f;    // 撲向玩家的衝刺速度
    public float attackRange = 2f;    // 攻擊範圍（必須小於你 Collider2D 的半徑）
    public float lungeDuration = 0.5f;// 撲擊持續的時間
    private Rigidbody2D rb;
    private Transform player;
    private Vector2 startPosition;    // 紀錄敵人的初始位置
    // 狀態判斷
    private bool isChasing = false;
    private bool isAttacking = false;
    private bool isReturning = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position; // 在遊戲開始時紀錄初始座標
    }

    // Update is called once per frame
    void Update()
    {
        // 1. 如果正在攻擊(衝刺中)，讓協程處理速度，Update不干涉
        if (isAttacking) return; 

        // 2. 如果狀態是「回歸原位」
        if (isReturning)
        {
            ReturnToStart();
        }
        // 3. 如果正在追逐玩家，且還沒回歸
        else if (isChasing && player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // 如果玩家進入了攻擊範圍，觸發撲擊
            if (distanceToPlayer <= attackRange)
            {
                StartCoroutine(LungeAttack());
            }
            else
            {
                // 還沒進攻擊範圍，正常追逐
                ChasePlayer();
            }
        }
        // 4. 沒事做的時候停在原地
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
    private void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * normalSpeed;
    }
    private void ReturnToStart()
    {
        Vector2 direction = (startPosition - (Vector2)transform.position).normalized;
        rb.linearVelocity = direction * normalSpeed;

        // 計算距離，如果已經非常靠近原位了，就停止回歸
        if (Vector2.Distance(transform.position, startPosition) < 0.1f)
        {
            transform.position = startPosition; // 確保位置精確對齊
            rb.linearVelocity = Vector2.zero;
            isReturning = false;                // 回歸結束，可以再次追擊玩家了
        }
    }

    // 撲擊協程
    private IEnumerator LungeAttack()
    {
        isAttacking = true; // 進入攻擊狀態
        startPosition=transform.position;
        // 鎖定當下玩家的方向進行衝刺
        Vector2 lungeDirection = (player.position - transform.position).normalized;
        rb.linearVelocity = lungeDirection * lungeSpeed;

        // 等待衝刺時間結束
        yield return new WaitForSeconds(lungeDuration);

        // 衝刺結束，強制進入回歸原位狀態
        rb.linearVelocity = Vector2.zero;
        isAttacking = false;
        isReturning = true; 
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag=="Player"){
            if(player==null)
            {
                player = collision.transform;
            }
            isChasing=true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.tag=="Player"){
            rb.linearVelocity=Vector2.zero;
            isChasing=false;
        }
    }
}

