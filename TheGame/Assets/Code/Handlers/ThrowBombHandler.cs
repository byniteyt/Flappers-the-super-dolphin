using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowBombHandler : MonoBehaviour
{
    CrabBomb[] bombs;

    [SerializeField] int cuantity;

    [SerializeField] float timeSpan;

    Timer coolDown = new Timer(4);

    [SerializeField] Transform cannonPos;

    private void Awake()
    {
        bombs = new CrabBomb[cuantity];

        for (int i = 0; i < bombs.Length; i++) 
        {
            bombs[i] = Instantiate(Resources.Load<GameObject>("Bomb").gameObject).GetComponent<CrabBomb>();
            bombs[i].gameObject.SetActive(false);
        }
    }

    public void ThrowBombs() 
    {
        StopAllCoroutines();
        StartCoroutine(ThrowBomb());
    }

    IEnumerator ThrowBomb() 
    {
        for (int i = 0; i < bombs.Length; i++)
        {
            bombs[i].gameObject.SetActive(true);
            bombs[i].transform.position = cannonPos.position;
            bombs[i].transform.rotation = cannonPos.rotation;
            bombs[i].Throw();

            yield return new WaitForSeconds(timeSpan);
        }
    }
}
