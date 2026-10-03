using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class Tornado : MonoBehaviour
{
    [SerializeField] GameObject tornadoObj;

    [SerializeField] int MaxObj;
    [SerializeField] MovableObjects[] objs;

    [SerializeField] float radio;

    [SerializeField] Transform dir1, dir2;

    [SerializeField] float y_var;

    private void Awake()
    {
        objs = new MovableObjects[MaxObj];
    }

    public void Activate() 
    {
        tornadoObj.SetActive(true);
        tornadoObj.transform.GetComponentInChildren<VisualEffect>().Play();
    }

    public void DeActivate() 
    {
        tornadoObj.SetActive(false);

        SoltarObjetos();
    }

    private void Update()
    {
        if (tornadoObj.activeSelf) 
        {
            RecogerObjeto();
            TornadoHability();
        }
    }

    void RecogerObjeto() 
    {
        Collider[] objetos = Physics.OverlapSphere(transform.position, radio);

        MovableObjects mv;

        foreach (Collider col in objetos)
        {
            mv = col.GetComponent<MovableObjects>();
            if (mv != null && !objs.Contains(mv)) 
            {
                MeterObjeto(mv);
            }
        }
    }

    void MeterObjeto(MovableObjects mv)  
    {
        for (int i = 0; i < objs.Length; i++) 
        {
            if (objs[i] == null) 
            {
                bool a = Random.value > 0.5f;
                objs[i] = mv;
                objs[i].InTornado(a ? dir1.up : dir2.up);

                Vector3 newPos = new Vector3(transform.position.x, transform.position.y + radio * y_var, transform.position.z);
                newPos.x += a ? -radio : radio;

                objs[i].transform.position = newPos;
                return;
            }
        }
    }

    void TornadoHability() 
    {
        foreach (var obj in objs) 
        {
            if (obj != null) 
            {
                obj.Rotar(transform.position);
            }
        }
    }

    void SoltarObjetos() 
    {
        StartCoroutine(Lanzar());
    }

    IEnumerator Lanzar() 
    {
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i] == null) continue;

            objs[i].transform.position = transform.position + new Vector3(0, 0, radio / 2);
            objs[i].Lanzar(transform.forward);
            objs[i] = null;

            yield return new WaitForSeconds(0.3f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
