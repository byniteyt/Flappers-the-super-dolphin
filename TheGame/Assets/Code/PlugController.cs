using System.Collections;
using UnityEngine;

public class PlugController : MonoBehaviour // Codigo para el enchufe
{
    private bool connected = false;
    [SerializeField] private float timeToRelease;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<EnemyStateManager>().enabled == false)
            {
                GlobalCoroutiner.Instance.EndAllCoroutines();
                StartCoroutine(ConnectEnemy(collision.gameObject.GetComponent<EnemyStateManager>()  ));
            }
        }
    }
    IEnumerator ConnectEnemy(EnemyStateManager enemy)
    {
        Debug.Log("Enemy connected");
        connected = true;
        yield return new WaitForSeconds(timeToRelease);
        Debug.Log("Enemy released");
        enemy.enabled = true;
        connected = false;
    }
}
