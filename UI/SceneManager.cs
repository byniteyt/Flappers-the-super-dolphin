using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerUI : MonoBehaviour 
{
    static string Level;
    bool additive;
    bool cursorOn;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void SetAdditive(bool additive) 
    {
        this.additive = additive;
    }

    public void Replay() 
    {
        if (Level != null && SceneManager.GetSceneByName(Level) != null) 
            SceneManager.LoadScene(Level, LoadSceneMode.Single);
    }

    public void SetCursorON(bool c) 
    {
        cursorOn = c;
    }

    public void OpenScene(string scene)  
    {
        if (scene != "GameOver" && !additive) Level = scene;
        if (cursorOn)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = cursorOn;
        SceneManager.LoadScene(scene, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
    }

    public void CloseScene(string scene) 
    {
        if (IsSceneLoaded(scene)) 
            SceneManager.UnloadSceneAsync(scene);
    }

    bool IsSceneLoaded(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName) == null) return false;
        return SceneManager.GetSceneByName(sceneName).isLoaded;
    }
}