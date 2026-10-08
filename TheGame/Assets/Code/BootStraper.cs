
using UnityEngine;

public class BootStraper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init() 
    {
        //ServiceLocator.Register<I_DeathEventSystem>(new DeathEvent());

        //ServiceLocator.Register<I_ListenEvent>(new ListenEvent());
    }
} 