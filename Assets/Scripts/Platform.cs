using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] obstacles;
    [SerializeField] private GameObject GameManager;
    private GameObject[] platforms;

    private void Start()
    {
        platforms = GameManager.GetComponent<PlatformGenerator>().platforms;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
            GameManager.GetComponent<PlatformGenerator>().generatePlatform(transform.position + new Vector3(0, 0, 100), platforms, transform.position, true);
    }
}
