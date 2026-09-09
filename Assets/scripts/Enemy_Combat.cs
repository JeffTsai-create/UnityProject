using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public int damage = 1;
    private void OncollisionEnter2D(Collion2D collision)
    {
        collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-1);
    }
}
