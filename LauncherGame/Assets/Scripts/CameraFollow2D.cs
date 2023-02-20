using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] float timeOffset;
    [SerializeField] Vector2 posOffset;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPos = transform.position;
    }
}
