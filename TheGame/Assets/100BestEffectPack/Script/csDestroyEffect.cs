using UnityEngine;
using System.Collections;

public class csDestroyEffect : MonoBehaviour {

	ParticleSystem[] cs;

    private void Awake()
    {
        cs = GetComponentsInChildren<ParticleSystem>();
    }

    public void Init() 
	{
        foreach (ParticleSystem p in cs) 
        {
            p.Stop();
            p.time = 0;
            p.Play();
        }
    }
}