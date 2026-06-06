using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float jumpForce = 3f, playerHeight;
    Rigidbody playerRB;
    RaycastHit hit;
    public float speed = 5f; //Variable de vitesse
    public bool canGenerated = false;
    int score = 0, side = 0; // Score du player / Variable qui sert � d�terminer ou se trouve le joueur sur l'axe x

    void Start()
    {
        //R�cup�ration du rigidbody du player
        playerRB = GetComponent<Rigidbody>();
    }


    void Update()
    {   
        if(GameManager.instance.isGameOver) return;
        //D�placement du player sur l'axe 'z' avec une vitesse d�finie
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Si on appuie sur Espace et que le Raycast touche le sol
        if (Input.GetKeyDown(KeyCode.Space))
            jump();
        //Appel de la fonction 'Move()'
        Move();
    }

    //Fonction de saut du player
    void jump()
    {
        // Si le Raycast touche le sol on applique le saut en fonction de jumpForce
        if (Physics.Raycast(transform.position, Vector3.down * playerHeight / 2, out hit,  2.2f))
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    //Fonction de d�placement du player sur l'axe 'x'
    void Move()
    {
        Vector3 decalage = new Vector3(2.15f, 0, 0);

        //Si le joueur appui sur une touche de direction et n'est pas d�j� � sur cette direction, alors il va � vers cette direction

        if (Input.GetKeyDown(KeyCode.LeftArrow) && side > -5)
        {
            transform.position -= decalage;
            side -= 5;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) && side < 5)
        {
            transform.position += decalage;
            side += 5;
        }
    }


    //Si le player entre en collision avec une plateforme, alors il y'a g�n�ration d'une nouvelle
    private void OntriggerEnter(Collider other)
    {   
        Debug.Log("tag touché: " + other.tag);
        if(other.tag == "Plateforme")
        {
            canGenerated = true;
        }
    }


    //affichage game over apres collisions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacles")
        {
            GameManager.instance.GameOver();
        }
            
    }
}
