using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerGroupSettingsVolume : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider sliderMaster;
    [SerializeField] Slider sliderBGM;
    [SerializeField] Slider sliderSFX;

    float master;
    float bgm;
    float sfx;

    private void Update()
    {
        if (master != sliderMaster.value || bgm != sliderBGM.value || sfx != sliderSFX.value)
        {
            audioMixer.SetFloat("Master_Volume", LinearToDecibel(sliderMaster.value));
            audioMixer.SetFloat("BGM_Volume", LinearToDecibel(sliderBGM.value));
            audioMixer.SetFloat("SFX_Volume", LinearToDecibel(sliderSFX.value));

            master = sliderMaster.value;
            bgm = sliderBGM.value;
            sfx = sliderSFX.value;
            SaveData();
        }
    }
    public void SaveData()
    {
        PlayerPrefs.SetFloat("Save_MasterVolume", sliderMaster.value);
        PlayerPrefs.SetFloat("Save_BGMVolume", sliderBGM.value);
        PlayerPrefs.SetFloat("Save_SFXVolume", sliderSFX.value);

        PlayerPrefs.Save();
    }
    private void Start()
    {
        sliderMaster.value = PlayerPrefs.GetFloat("Save_MasterVolume");
        sliderBGM.value = PlayerPrefs.GetFloat("Save_BGMVolume");
        sliderSFX.value = PlayerPrefs.GetFloat("Save_SFXVolume");
    }

    private float LinearToDecibel(float linear)
    {
        linear = Mathf.Clamp(linear, 0.0001f, 1);
        return 20 * Mathf.Log10(linear);
    }
}
