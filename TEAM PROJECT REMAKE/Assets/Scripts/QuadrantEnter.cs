using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadrantEnter : MonoBehaviour
{

    static bool isOnBorder = false;
    public CameraController cameraController;

    //..................................................ON QUADRANT ENTRY
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && isOnBorder == false)
        {
            isOnBorder = true;
            cameraController.MoveCamera(this.transform.position.x, this.transform.position.y);
            print("Entered Quadrant");
            
        }

        print("Object Detected!");
    }

    //...................................................ON QUADRANT EXIT

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            isOnBorder = false;
            print("Left Quadrant");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
