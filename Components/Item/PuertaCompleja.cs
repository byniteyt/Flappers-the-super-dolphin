using UnityEngine;

public class PuertaCompleja : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Candado")) 
        {
            AbrirPuerta();
        }
    }

    public void AbrirPuerta() 
    {
        animator.SetTrigger("open");
        print("PuertaAbierta");
    }
}
