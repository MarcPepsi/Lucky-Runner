using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] float jumpForce = 3f, playerHeight;
    RaycastHit hit;
    Rigidbody playerRB;
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }


    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        Debug.DrawRay(transform.position, Vector3.down * playerHeight / 2, Color.green);
        // Si on appuie sur Espace et que le Raycast touche le sol
        if (Input.GetKeyDown(KeyCode.Space))
            jump();
    }

    void jump()
    {
        // Si le Raycast touche le sol on applique le saut en fonction de jumpForce
        if (Physics.Raycast(transform.position, Vector3.down * playerHeight / 2, out hit,  2.1f))
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
