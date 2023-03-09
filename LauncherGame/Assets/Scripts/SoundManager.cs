using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            LoadSetting();
        }
        else
        {
            LoadSetting();
        }
    }
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }

    private void LoadSetting()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    private void SaveSetting()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
