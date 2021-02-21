using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTeleporter : MonoBehaviour
{
    //...............................................Variables
    public Transform teleportToLocation;
    public ETController etController;
    private float xPos;
    private float yPos;

    void Start()
    {
        xPos = 40;
        yPos = 40;
    }

   
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D playerCollision)
    {
        if (playerCollision.gameObject.CompareTag("Player"))
        {
            etController.TeleportToHole(xPos, yPos);
        }
    }

    
}
