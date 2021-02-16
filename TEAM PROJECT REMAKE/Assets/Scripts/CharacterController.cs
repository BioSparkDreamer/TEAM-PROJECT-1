using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //...............................................Movement Variables
    public float baseSpeed = 3.0f;
    private float speedVar;
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    


    void Start()
    {
        //...........................................Movement Instantiation
        speedVar = baseSpeed;
        rigidbody2d = GetComponent<Rigidbody2D>();

    }


    void Update()
    {
        //...........................................Movement Update (detect input)
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");  
    }

    void FixedUpdate()
    {
        //...........................................Movement FixedUpdate (react to input)
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speedVar * horizontal * Time.deltaTime;
        position.y = position.y + speedVar * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }
}
