using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    private Vector3 position;
    void Start()
    {
        
    }

    void Update()
    {
        position = player.transform.position + offset;
    }
}
