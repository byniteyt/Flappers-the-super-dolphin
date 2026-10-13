using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public void DestroyParent()
    {
        Destroy(transform.parent.gameObject);
    }
}
