    using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlappertsStats : MonoBehaviour
{
    static uint coins;

    [SerializeField] uint initMaxHealth;
    static uint MaxHealth;
    static uint health;

    public TextMeshProUGUI coinsText;
    public GameObject[] lifes;

    private void Start()
    {
        if (initMaxHealth > 0) MaxHealth = initMaxHealth;
        health = MaxHealth;

        UpdateCoinsText();
        UpdateHealthUI();
    }

    public void AddCoin() 
    {
        coins++;
        UpdateCoinsText();
    }

    void UpdateCoinsText() 
    {
        if (coinsText != null) coinsText.text = coins.ToString();
    }

    void UpdateHealthUI() 
    {
        for (int i = 0; i < lifes.Length; i++)
        {
            lifes[i].SetActive(i < health);
        }
    }

    public void GetHit(uint damage) 
    {
        if (health > damage + 1)
        {
            UpdateHealthUI();
            health -= damage;
        }
        else 
        {
            SceneManager.LoadScene("GameOver");
            print("DEADDDD");
        }
    }
}