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
    private Quaternion rotationNormale; // On stoce deux orientations du player pour les utiliser lors des changements de position(debout/ couché)
    private Quaternion rotationBasse;

    private int coinCount = 0; // Variable pour compter les pièces collectées
    private int jumpCount = 0; // Variable pour compter le nombre de sauts effectués

    void Start()
    {
        //R�cup�ration du rigidbody du player
        playerRB = GetComponent<Rigidbody>();

        rotationNormale = transform.rotation;
        rotationBasse = rotationNormale * Quaternion.Euler(90f, 0f, 0f); //On donne des valeurs à la rotation basse pour que le player soit couché
    }


    void Update()
    {   
        if(GameManager.instance.isGameOver) return;
        //D�placement du player sur l'axe 'z' avec une vitesse d�finie
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World); // Deplacement en coordonnées monde pour etre independant de l'orientation

        // Si on appuie sur Espace et que le Raycast touche le sol
        if (Input.GetKeyDown(KeyCode.Space))
            jump();
        //Appel de la fonction 'Move()'
        Move();

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.rotation = rotationBasse;
        }
        else
        {
            transform.rotation = rotationNormale;
        }
    }

    //Fonction de saut du player
   void jump()
    {
        bool isGrounded =
            Physics.Raycast(
                transform.position,
                Vector3.down,
                out hit,
                2.2f
            );

        if (isGrounded)
        {
            jumpCount = 0;
        }

        // Premier saut gratuit
        if (jumpCount == 0)
        {
            playerRB.velocity = new Vector3(
                playerRB.velocity.x,
                0,
                playerRB.velocity.z
            );

            playerRB.AddForce(
                Vector3.up * jumpForce,
                ForceMode.Impulse
            );

            jumpCount++;
        }
        // Deuxième saut payant
        else if (jumpCount == 1 && coinCount >= 10)
        {
            SetCoinCount(-10);

            playerRB.velocity = new Vector3(
                playerRB.velocity.x,
                0,
                playerRB.velocity.z
            );

            playerRB.AddForce(
                Vector3.up * jumpForce,
                ForceMode.Impulse
            );

            jumpCount++;
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
    private void OnTriggerEnter(Collider other)
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
        if (!collision.gameObject.CompareTag("Obstacles"))
            return;

        ContactPoint contact = collision.contacts[0]; //Récupération du point de contact de la collision pour déterminer la direction de l'impact

        float dot = Vector3.Dot(contact.normal, Vector3.up);

        if (dot > 0.8f)
        {
            return; // Si le point de contact est principalement orienté vers le haut, il n'y a pas de game over.
        }
        if (coinCount >= 100)
        {
            SetCoinCount(-100);
            return; // Si le joueur a au moins 100 pièces, il peut survivre à la collision.
        }
        else
        {
            GameManager.instance.GameOver();
        }
        GameManager.instance.GameOver();
    }

    public void SetCoinCount(int count)
    {
        coinCount += count;
        Debug.Log("Pièces collectées : " + coinCount);
    }

    public int GetCoinCount()
    {
        return coinCount;
    }

    
}
