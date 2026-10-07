using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public int CurrentHealth ;
    public int MaxHealth = 100 ;
    public Slider slider;

    void Start()
    {
        CurrentHealth=MaxHealth;
        slider.maxValue=MaxHealth;
        slider.value=CurrentHealth;
    }
    public void ChangeHealth(int amount)
    {
        CurrentHealth +=amount;
        slider.value=CurrentHealth;
        if(CurrentHealth<=0)
        {
            gameObject.SetActive(false);
        }


    }
}
