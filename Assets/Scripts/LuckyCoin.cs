using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyCoin : MonoBehaviour
{

    public float rotationSpeed = 80f; //Vitesse de rotation des piece
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if(player != null)
        {
            player.SetCoinCount(1);

            Destroy(gameObject);
        }
    }

    public void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
