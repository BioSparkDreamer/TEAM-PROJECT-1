using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTeleporter : MonoBehaviour
{
    //...............................................Variables
    public Transform teleportToLocation;
    public ETController etController;

    //determines if this trigger causes the player to be switched to or from perspective after teleporting
    public bool isometricPerspective; 

    private float xPos;
    private float yPos;

    void Start()
    {
        //...............................................Pull position for teleport function
        xPos = teleportToLocation.transform.position.x;
        yPos = teleportToLocation.transform.position.y;
    }

   
    void Update()
    {
        
    }

    //...............................................Call teleport function on player collision
    void OnTriggerEnter2D(Collider2D playerCollision)
    {
        if (playerCollision.gameObject.CompareTag("Player"))
        {
            etController.TeleportToHole(xPos, yPos, isometricPerspective);
        }
    }

    
}
