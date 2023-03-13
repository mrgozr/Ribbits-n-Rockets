using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{

    public Settings settings;
    public bool testBool;

    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        if(PlayerPrefs.GetInt("godmode") == 1)
            Debug.Log("Godmode on!");
        else if (PlayerPrefs.GetInt("godmode") == 0)
            Debug.Log("Godmode off!");
    }
    void Update()
    {

    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadLevelSelect()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadSettings()
    {
        SceneManager.LoadScene(2);
    }
    public void LevelOne()
    {
        SceneManager.LoadScene(3);
    }
    public void LevelTwo()
    {
        SceneManager.LoadScene(4);
    }
    public void LevelThree()
    {
        SceneManager.LoadScene(5);
    }
    public void LevelFour()
    {
        SceneManager.LoadScene(6);
    }
    public void LevelFive()
    {
        SceneManager.LoadScene(7);
    }
    public void ExitGame()
    {
        Application.Quit();
        // UnityEditor.EditorApplication.isPlaying = false;
    }
    
}
