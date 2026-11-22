using System;
using UnityEngine;
using UnityEngine.Playables;

public class MobRush : MonoBehaviour
{
    PlayableDirector m_Director;

    private void Awake()
    {
        m_Director = GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            m_Director.Play();
            //gameObject.SetActive(false);
            Destroy(GetComponent<BoxCollider>());
        }
    }
}