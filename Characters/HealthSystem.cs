using UnityEngine;

public abstract class HealthSystem : MonoBehaviour
{
    [SerializeField] protected int MaxHealth;
    [SerializeField] protected int health;

    private void Awake()
    {
        health = MaxHealth;
    }

    public virtual void GetHit(int damage)
    {
        print("got hit");
        if (health - damage <= 0)
        {
            GetKilled();
            return;
        }

        health -= damage;
        GetHit();
    }

    public abstract void GetHit();

    public abstract void GetKilled();

    public abstract void UpdateHealth();

    public bool TryHeal(int cuantity)  
    {
        if (health >= MaxHealth) return false;

        health += cuantity;
        if (health > MaxHealth) health = MaxHealth;
        UpdateHealth(); 
        return true;
    }
}