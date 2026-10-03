using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emmiter : MonoBehaviour
{
    [SerializeField] MovableObjects objBase;

    [SerializeField] int cuant;
    MovableObjects[] Objects;

    int index = 0;

    [SerializeField] float time;
    [SerializeField] float MaxDist;


    private void Awake()
    {
        Objects = new MovableObjects[cuant];

        for (int i = 0; i < Objects.Length; i++)
        {
            Objects[i] = Instantiate(objBase, transform);
            Objects[i].gameObject.SetActive(false);
            Objects[i].name = i.ToString();
        }
    }

    void Start()
    {
        StartCoroutine(StartFall());
    }

    IEnumerator StartFall() 
    {
        while (true)
        {
            SetIndex();
            Objects[index].gameObject.SetActive(true);
            Objects[index].transform.position = transform.position;
            Objects[index].Reset();
            yield return new WaitForSeconds(time);
        }
    }

    void SetIndex() 
    {
        if (index < Objects.Length -1) index++;
        else index = 0;
    }
}