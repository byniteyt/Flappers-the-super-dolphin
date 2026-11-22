using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseInvoker : MonoBehaviour
{
    [SerializeField] bool pauseActive = false;
    [SerializeField] bool LevelSelection = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (LevelSelection) SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            else if (!pauseActive) 
            {
                SceneManager.LoadSceneAsync("Pause", LoadSceneMode.Additive);
                pauseActive = true;
                Time.timeScale = 0;
            }
        }
    }

    public void ClosePauseMenu() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        pauseActive = false;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Escena cargada: " + scene.name);
        SceneManager.SetActiveScene(scene);
    }
}