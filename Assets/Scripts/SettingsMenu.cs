using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private AudioMixer musicMixer;

    [SerializeField]
    private AudioMixer soundFXMixer;

    [SerializeField]
    private Slider musicSlider;

    [SerializeField]
    private Slider soundSlider;

    [SerializeField]
    private RenderPipelineAsset[] qualityLevels;
    [SerializeField]
    private Dropdown dropdown;

    [SerializeField]
    private GameObject settingsMenu;

    [SerializeField]
    private GameObject powerupMenu;

    // Start is called before the first frame update
    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol", musicSlider.value);
        soundSlider.value = PlayerPrefs.GetFloat("SoundVol", soundSlider.value);
        dropdown.value = PlayerPrefs.GetInt("Settings", dropdown.value);
        dropdown.value = QualitySettings.GetQualityLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable() 
    {
        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
        PlayerPrefs.SetFloat("SoundVol", soundSlider.value);
        PlayerPrefs.SetInt("Settings", dropdown.value);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        QualitySettings.renderPipeline = qualityLevels[qualityIndex];
    }

    public void SetLevel(float sliderValue)
    {
        musicMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20f);
    }

    public void SetSoundLevel(float sliderValue)
    {
        soundFXMixer.SetFloat("SoundVol", Mathf.Log10(sliderValue) * 20f);
    }

    public void Fullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void onPowerupMenu()
    {
        settingsMenu.gameObject.SetActive(false);
        powerupMenu.gameObject.SetActive(true);
    }

    public void onClosePowerupMenu()
    {
        settingsMenu.gameObject.SetActive(true);
        powerupMenu.gameObject.SetActive(false);
    }
}
