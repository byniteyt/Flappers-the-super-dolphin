using System;
using System.Collections;
using UnityEngine;

//Object Pool Implementation
public class Spawner : MonoBehaviour
{
    [SerializeField] AgentGeneric[] enemies;
    [SerializeField] float spawnTime;

    [SerializeField] Transform spawnTransform;

    private void Start() 
    {
        if (spawnTransform != null)
        {
            TrySpawn();
            StartCoroutine(SpawnWait());
        }
    }

    public void TrySpawn() 
    {
        foreach (var mob in enemies) 
        {
            if (!mob.gameObject.activeSelf) 
            {
                mob.gameObject.SetActive(true);
                mob.enabled = true;
                mob.Spawn(spawnTransform == null ? null : spawnTransform.position);
                return;
            }
        }
    }

    IEnumerator SpawnWait() 
    {
        yield return new WaitForSeconds(spawnTime);
        TrySpawn();
        StartCoroutine(SpawnWait());
    }
}
