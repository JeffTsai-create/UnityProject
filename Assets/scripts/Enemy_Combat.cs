using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public int damage = 1;
    private void OncollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(damage);
    }
}
