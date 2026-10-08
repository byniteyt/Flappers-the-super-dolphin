using System.Collections;
using UnityEngine;

public class GlobalCoroutiner : MonoBehaviour
{
    private static GlobalCoroutiner instance;

    public static GlobalCoroutiner Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("GlobalCoroutiner");
                instance = obj.AddComponent<GlobalCoroutiner>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }
    
    public void RunCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    public void EndCoroutine(IEnumerator coroutine)
    {
        StopCoroutine(coroutine);
    }

    public void EndAllCoroutines()
    {
        StopAllCoroutines();
    }
}
