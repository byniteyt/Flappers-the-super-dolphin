using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    bool isPaused = false;
    List<GameObject> openedScenes = new List<GameObject>(); // servirá para contar qué panel abrimos y qué hijo de este desplegamos
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Time.timeScale = isPaused ? 0f : 1f;
        transform.GetChild(0).gameObject.SetActive(isPaused);
    }
    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            EscPressed();
        }
    }
    void EscPressed()
    {
        if (openedScenes.Count > 0)
        {
            CloseScene();
            return;
        }
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale =  0f;
        OpenScene(transform.GetChild(0).gameObject);
    }
    public void OpenScene(GameObject scene)
    {
        if (!openedScenes.Contains(scene))
        {
            openedScenes.Add(scene);
            scene.SetActive(true);
        }
    }
    public void CloseScene()
    {
        openedScenes[openedScenes.Count - 1].SetActive(false);
        openedScenes.RemoveAt(openedScenes.Count - 1);
        if (openedScenes.Count == 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isPaused = false;
            Time.timeScale = 1f;
        }
    }
}
