using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] public bool isGodMode;
    [SerializeField] public bool isFullscreen;
    public Toggle godmodeToggleSwitch;
    public Toggle fullscreenToggleSwitch;
    // Start is called before the first frame update

    void Awake()
    {
        if(PlayerPrefs.GetInt("godmode") == 1)
        {
            godmodeToggleSwitch.isOn = true;
        }
        else if(PlayerPrefs.GetInt("godmode") == 0)
        {
            // godmodeToggleSwitch.isOn = false;
        }
    }
    public void godmodeToggle()
    {
        if (!isGodMode)
        {
            PlayerPrefs.SetInt("godmode", 1);
            isGodMode = true;
        }
        else if (isGodMode)
        {
            PlayerPrefs.SetInt("godmode", 0);
            isGodMode = false;
        }
    }
    public void fullscreenToggle()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
