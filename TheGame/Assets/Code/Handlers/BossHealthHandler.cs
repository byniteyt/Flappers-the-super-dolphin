using UnityEngine;
using UnityEngine.UI;

public class BossHealthHandler : MonoBehaviour
{
    [SerializeField] int MaxHealth;
    [SerializeField] int health;

    [SerializeField] Slider healthSlider;

    void Start()
    {
        health = MaxHealth;
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    private void OnCollisionEnter(Collision collision)
    {
        MovableObjects mv = collision.gameObject.GetComponent<MovableObjects>();
        if (mv != null) 
        {
            health -= mv.Damage;

            healthSlider.value = health;

            mv.gameObject.SetActive(false);

            if (health <= 0) 
            {
                health = 0;
                DIE();
            }
        }
    }

    public void DIE() 
    {
        Destroy(GetComponent<CrabintonController>());
        Destroy(GetComponent<CrabintonReciever>());
    }
}