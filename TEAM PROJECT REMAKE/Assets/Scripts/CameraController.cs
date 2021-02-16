using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveCamera(float xPosition, float yPosition)
    {
        this.transform.position = new Vector2(xPosition, yPosition);
        print("Moved camera!");
    }
}
