using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Properties;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] PlayerMovement PM; //Variable qui stocke le composant 'PlayerMovement'
    [SerializeField] Quaternion rotate = Quaternion.identity; //Conservation de la rotation du prefab
    [SerializeField] Vector3 nextPosition, decalage; //Position de la génération de la plateforme suivante
    [SerializeField] GameObject basicPlatform; //Première plateforme
    private GameObject currentPlatform, currentObstacle; 
    public Platform[] platforms; //Tableau de 'platforms'
    private int nbr; //Nombre aléatoire

    void Start()
    {
        //Appel de la fonction de génération de plateforme lors du lancement de la scène
        generatePlatform(basicPlatform.transform.position, platforms, false);
    }
    private void Update()
    {
        //Si la 'canGenerated' est vrai, il y'a génération d'une nouvelle plateforme
        if (PM.canGenerated)
        {
            generatePlatform(nextPosition, platforms, true);
            PM.canGenerated = false;
        }

    }
    //Fonction de génération de platforme
    public void generatePlatform(Vector3 position, Platform[] platforms, bool isRandom)
    {
        if (!isRandom)
        {
            nbr = 0;
            nextPosition = position + new Vector3(0, 0, platforms[nbr].height);
        }
        else
        {
            nbr = Random.Range(0, platforms.Length);

            nextPosition += new Vector3(0, 0, platforms[nbr].height);
        }

        //Instantiation d'une plateforme
        currentPlatform = Instantiate(platforms[nbr].prefab, nextPosition, rotate);

        //Appel de la fonction de génération d'obstacles
        generateObstacles(platforms[nbr]);

        //Destruction automatique des plateformes après quelques sécondes
        Destroy(currentPlatform, 15f);    

    }

    //Fontion de génération d'obstacles aléatoirement
    void generateObstacles(Platform platform)
    {
        //Pour chaque point d'apparition, un obstacle est généré
        foreach (GameObject spawnPoint in platform.spawnPoints)
        {
            nbr = Random.Range(0, platform.obstacles.Length);
            currentObstacle = Instantiate(platform.obstacles[nbr], spawnPoint.transform.position + currentPlatform.transform.position + decalage, rotate);
            currentObstacle.transform.SetParent(currentPlatform.transform);
        }
    }

    //C'etait tous les gars 
}

