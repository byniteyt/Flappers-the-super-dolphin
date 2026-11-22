using UnityEditor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void UnPause() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;

        FindFirstObjectByType<PauseInvoker>().ClosePauseMenu();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}