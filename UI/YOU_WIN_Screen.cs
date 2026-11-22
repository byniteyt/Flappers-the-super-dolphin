using TMPro;
using UnityEngine;

public class YOU_WIN_Screen : MonoBehaviour
{
    public TextMeshProUGUI coinsText;

    private void Awake()
    {
        coinsText.text = "Current Coins: " + SAVE_iNVENTORY.GetCoins().ToString();
    }
}