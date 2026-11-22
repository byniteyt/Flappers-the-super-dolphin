using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWinCondition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            other.GetComponent<FlappertsWallet>().AddREALCoin();
            SceneManager.LoadScene("WIN", LoadSceneMode.Single);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}