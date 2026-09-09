using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public int CurrentHealth ;
    public int MaxHealth ;
    public void ChangeHealth(int amount)
    {
        CurrentHealth +=amount;
        if(CurrentHealth<=0)
        {
            gameObject.SetActive(false);
        }


    }
}
