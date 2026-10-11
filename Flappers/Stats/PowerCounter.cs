using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerCounter : MonoBehaviour
{
    [Header("Power")]
    [SerializeField] private int maxPower = 100;
    static int currentPower = 100;
    [SerializeField] private int rechargePerSecond = 5;

    private static readonly HashSet<object> activeAbilities = new();

    public float CurrentPower => currentPower;
    public float MaxPower => maxPower;
    public bool HasActiveAbilities => activeAbilities.Count > 0;

    Timer timer;
    [SerializeField] float rechargeTime;

    [SerializeField] Slider slider;

    private void Awake()
    {
        if (slider!= null) slider.maxValue = maxPower;

        timer = new Timer(rechargeTime);
        currentPower = maxPower;
    }

    private void Update()
    {
        if (slider != null) slider.value = currentPower;

        if (!HasActiveAbilities)
        {
            timer.UpdateTimer();

            if (!timer.isRunning()) 
            {
                currentPower += rechargePerSecond;
                timer.StartTimer();
            }
        }
    }

    public static bool TryConsume(int amount)
    {
        if (currentPower > amount)
        {
            currentPower -= amount;
            return true;
        }
        return false;
    }

    public static void RegisterAbility(object ability)
    {
        if (ability != null)
            activeAbilities.Add(ability);
    }

    public static void UnregisterAbility(object ability)
    {
        if (ability != null)
            activeAbilities.Remove(ability);
    }
}