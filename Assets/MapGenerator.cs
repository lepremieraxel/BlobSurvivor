using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // 7/24.9
    // 14/49.8
    public Transform playerCamera;
    public Vector2 latestCameraPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        latestCameraPos = transform.position;
    }
}
