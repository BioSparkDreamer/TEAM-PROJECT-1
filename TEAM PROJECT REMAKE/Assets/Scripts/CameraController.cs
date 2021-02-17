using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(1280, 480, true);
    }
    //..................................................MOVE THE CAMERA TO QUADRANT
    public void MoveCamera(float xPosition, float yPosition)
    {
        transform.position = new Vector3(xPosition, yPosition, -10);
    }
}
