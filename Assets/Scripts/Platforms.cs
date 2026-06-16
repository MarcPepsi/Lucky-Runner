using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Properties;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] PlayerMovement PM; //Variable qui stocke le composant 'PlayerMovement'
    [SerializeField] Quaternion rotate = Quaternion.identity; //Conservation de la rotation du prefab
    [SerializeField] Vector3 nextPosition, decalage; //Position de la g�n�ration de la plateforme suivante
    [SerializeField] GameObject basicPlatform; //Premi�re plateforme
    [SerializeField] GameObject coinPrefab; //Prefab de la piece
    [SerializeField]float laneWidth = 2.15f;
    private GameObject currentPlatform, currentObstacle; 
    public Platform[] platforms; //Tableau de 'platforms'
    private int nbr; //Nombre al�atoire

    void Start()
    {
        //Appel de la fonction de g�n�ration de plateforme lors du lancement de la sc�ne
        generatePlatform(basicPlatform.transform.position, platforms, false);
    }
    private void Update()
    {
        //Si la 'canGenerated' est vrai, il y'a g�n�ration d'une nouvelle plateforme
        if (GameManager.instance.isGameOver) return;

        //Genere quand le joueur est proche de la  prochaine position
        float distance = Vector3.Distance(PM.transform.position, nextPosition);
        if (distance < 30f )
        {
            generatePlatform(nextPosition, platforms, true);
            PM.canGenerated = false;
        }

    }
    //Fonction de g�n�ration de platforme
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
        GenerateCoins();

        //Appel de la fonction de g�n�ration d'obstacles
        generateObstacles(platforms[nbr]);

        //Destruction automatique des plateformes apr�s quelques s�condes
        Destroy(currentPlatform, 15f);    

    }

    //Fontion de g�n�ration d'obstacles al�atoirement
    void generateObstacles(Platform platform)
    {
        //Pour chaque point d'apparition, un obstacle est g�n�r�
        foreach (GameObject spawnPoint in platform.spawnPoints)
        {
            nbr = Random.Range(0, platform.obstacles.Length);
            currentObstacle = Instantiate(platform.obstacles[nbr], spawnPoint.transform.position + currentPlatform.transform.position + decalage, rotate);
            currentObstacle.transform.SetParent(currentPlatform.transform);
        }
    }

    void GenerateCoins()
    {
        
        int lane = Random.Range(0, 3);

        int coinNumber = Random.Range(3, 9);

        float laneX = (lane - 1) * laneWidth;

        for(int i = 0; i < coinNumber; i++)
        {
            Vector3 pos =
                currentPlatform.transform.position;

            pos += new Vector3(
                laneX,
                1f,
                i * 2f
            );

            RaycastHit hit;

            if(Physics.Raycast (pos + Vector3.up * 10f, Vector3.down, out hit, 20f))
            {
                pos.y = hit.point.y + 1.5f;
            }

            GameObject coin =
                Instantiate(coinPrefab, pos, coinPrefab.transform.rotation);

            coin.transform.SetParent(currentPlatform.transform);
        }
    }

    //C'etait tous les gars 
}

