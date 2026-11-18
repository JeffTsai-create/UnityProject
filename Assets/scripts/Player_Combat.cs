using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayer;
    public int attackDamage = 1;
    public Animator animator;
    public float cooldown = 2f;
    private float timer;
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
    public void Attack()
    {
        if (timer <= 0)
        {
            animator.SetBool("isAttacking",true);
            timer = cooldown;
        }
    }
    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.isTrigger)
        {
            continue;
        }
            Enemy_Health enemyHealth = enemy.GetComponent<Enemy_Health>();
            if (enemyHealth != null)
            {
                enemyHealth.ChangeHealth(-attackDamage);
            }
        }
        
    }
    public void finishAttacking()
    {
        animator.SetBool("isAttacking",false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
