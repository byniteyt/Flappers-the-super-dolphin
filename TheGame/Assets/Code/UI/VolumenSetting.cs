using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumenSetting : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider generalSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    public void SetGlobalVolume(float volume)
    {
        audioMixer.SetFloat("GeneralVol",
            volume <= 0.0001f ? -80f : Mathf.Log10(volume) * 20f);

        PlayerPrefs.SetFloat("GeneralVol", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVol",
            volume <= 0.0001f ? -80f : Mathf.Log10(volume) * 20f);

        PlayerPrefs.SetFloat("MusicVol", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        audioMixer.SetFloat("EffectsVol",
            volume <= 0.0001f ? -80f : Mathf.Log10(volume) * 20f);

        PlayerPrefs.SetFloat("EffectsVol", volume);
    }

    void LoadGeneralVolume() 
    {
        generalSlider.value = PlayerPrefs.GetFloat("GeneralVol", 1f);
        SetGlobalVolume(generalSlider.value);
    }

    void LoadMusicVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol", 1f);
        SetMusicVolume(musicSlider.value);
    }

    void LoadSfxVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("EffectsVol", 1f);
        SetEffectsVolume(sfxSlider.value);
    }
    private void Awake()
    {
        LoadGeneralVolume();
        LoadMusicVolume();
        LoadSfxVolume();
        gameObject.SetActive(false);
    }
}
