using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadrantEnter : MonoBehaviour
{
    static bool tentativeEnter = false;
    public CameraController cameraController;

    //..................................................ON QUADRANT ENTRY

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.tag == "Player")
        {
            tentativeEnter = true; //Temporarily disables "quadrant return"
            cameraController.MoveCamera(transform.position.x, transform.position.y);
            print("Camera: tentative move");
        }
        
    }

    //...................................................ON QUADRANT EXIT

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            tentativeEnter = false;  //Tells 'quadrant return' below to turn back on
            print("Camera: fully moved or canceled");
        }
    }

    //...................................................ON QUADRANT RETURN

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && tentativeEnter == false)
        {
            cameraController.MoveCamera(transform.position.x, transform.position.y); //recenters the camera on the quadrant player hasn't fully commited to leaving yet
        }
    }
}
