using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    void Awake()
    {
    QualitySettings.vSyncCount = 0;
     Application.targetFrameRate = 60;
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
        UnityEditor.EditorApplication.isPlaying = false;
    }
    
}
