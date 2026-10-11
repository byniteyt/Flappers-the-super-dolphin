using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            other.GetComponent<FlappertsStats>().AddCoin();
            Destroy(gameObject);
        }
    }
}