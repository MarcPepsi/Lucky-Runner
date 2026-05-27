using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum level
{
    veryEasy,
    easy,
    normal,
    high,
    veryHigh
}

[CreateAssetMenu(fileName = "New Platform", menuName = "ScriptablesObjects/Platform")]
public class Platform : ScriptableObject
{
    public GameObject prefab; //prefab de la plateforme
    public float height = 20; //Hauteur de la plateforme
    public GameObject[] spawnPoints; //Tableau de tous les point d'apparition d'ostacles sur la plateforme
    public GameObject[] obstacles; //Tableau de tous les obstacles de la plateforme
    public level level = level.easy; //Niveau de difficulté de la plateforme
}


