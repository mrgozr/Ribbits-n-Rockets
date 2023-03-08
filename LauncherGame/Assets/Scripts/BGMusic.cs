using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    /* I'll be honest, not my best code within this file. The purpose is to make the music loop even when the level is reset.
    Essentially this creates a varaible, instance, and then checks if it already exists. 
    If it does, it doesn't play the music again and 'destroys' the file.
    Otherwise, the music is played. */


    static BGMusic instance = null;
    [SerializeField] private AudioSource bgMusic;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            bgMusic.Play();
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }
}
