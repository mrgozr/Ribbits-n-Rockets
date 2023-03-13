using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevelMusic : MonoBehaviour
{

    [SerializeField] private AudioSource endMusic;

    void Awake()
    {
        Destroy(GameObject.Find("Background Music"));
        endMusic.Play();
    }
}
