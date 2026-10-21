using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Animator animator;
    public void Attack()
    {
        animator.SetBool("isAttacking",true);
    }
    public void finishAttacking()
    {
        animator.SetBool("isAttacking",false);
    }
}
