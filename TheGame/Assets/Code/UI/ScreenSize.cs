using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSize : MonoBehaviour
{
    [SerializeField] TMP_Dropdown resolutionDropdown;
     
    [SerializeField] Toggle fullScreen;
    Resolution[] resolutions;
    int resolutionIndex;
    // Start is called before the first frame update
    void Start()
    {
        Revisar();
        AplicarFullScreen(PlayerPrefs.GetInt("PantallaCompleta", 0)==1);
        AplicarConfiguracionInicial();
        gameObject.SetActive(false);
    }

    void Revisar()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height) // Check for fullscreen resolution
            {
                resolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = resolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("valorResolucion", resolutionIndex);
        //Debug.Log("Resolución cambiada a: " + resolution.width + " x " + resolution.height);
    }

    private void AplicarConfiguracionInicial()
    {
        int pantallaCompleta = PlayerPrefs.GetInt("PantallaCompleta", 0);
        if (pantallaCompleta == 1)
        {
            fullScreen.isOn = true;
            Screen.fullScreen = true;
            resolutionDropdown.interactable = false;
        }
        else
        {
            fullScreen.isOn = false;
            Screen.fullScreen = false;
            resolutionDropdown.interactable = true;
            int valorResolucion = PlayerPrefs.GetInt("valorResolucion", resolutionIndex);
            SetResolution(valorResolucion);
            resolutionDropdown.value = valorResolucion;
        }
    }

    public void AplicarFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("PantallaCompleta", isFullScreen ? 1 : 0);
        resolutionDropdown.interactable = !isFullScreen;
        //Debug.Log("Pantalla completa: " + isFullScreen);
    }

}
