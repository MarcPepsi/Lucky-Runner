using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//(pour recharger les scenes)
using TMPro;//pour utiliser le TextMeshPro(affichage du texte)


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //reference au texte du score affiché a l'écran
    public TextMeshProUGUI scoreText;
    //reférence au panel Game over qui sera caché au départ
    public GameObject gameOverPanel;
    //reference au panel PM
    public PlayerMovement PM;

    //score de depart
    private float score = 0;
    //vérifie si le jeu est terminé
    public bool isGameOver = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        //si le jeu n'est pas terminé le score augmente avec le temps
        if (!isGameOver) 
        {
            //augmentation du score avec le temps
            score += Time.deltaTime * 10;
            scoreText.text = "Score : " + Mathf.FloorToInt(score);

            //augmentation de la vitesse avec le temps
            PM.speed += Time.deltaTime * 0.1f;
        }
    }

    public void GameOver()
    {
        // Arret du score si le jeu est terminé
        isGameOver = true;
        //Affiche Game over à l'écran
        gameOverPanel.SetActive(true);
    }

    public void Restart(){
        //Recharge la page au Restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
