using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject[] platforms;
    private int nbr;
    void Start()
    {
        generatePlatform(Vector3.forward * 75, platforms, Vector3.forward * 75, false);
    }

    //Fonction de génération de platforme
    public void generatePlatform(Vector3 position, GameObject[] platforms, Vector3 platformPosition, bool isRandom)
    {
        if (!isRandom)
            nbr = 0;
        else
            nbr = Random.Range(0, platforms.Length);

        Instantiate(platforms[nbr], position, Quaternion.identity);
        generateObstacles(platforms[nbr].GetComponent<Platform>().spawnPoints, platforms[nbr].GetComponent<Platform>().obstacles, platformPosition);
    }

    //Fontion de génération d'obstacles aléatoirement
    void generateObstacles(GameObject[] spawnpoints, GameObject[] obstacles, Vector3 position)
    {
        foreach (GameObject spawnPoint in spawnpoints)
        {
            nbr = Random.Range(0, obstacles.Length);
            Instantiate(obstacles[nbr], spawnPoint.transform.position + position, Quaternion.identity);
        }
    }
}
