using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    //...............................................Movement Variables
    public float baseSpeed = 3.0f;
    private float speedVar;
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    //...............................................Energy Variables
    public Text energyText;
    private int totalEnergy;
    
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
        //...........................................Destroy ET Timer
        Destroy(gameObject, 90);
    }

    void FixedUpdate()
    {
        //...........................................Movement FixedUpdate (react to input)
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speedVar * horizontal * Time.deltaTime;
        position.y = position.y + speedVar * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //...........................................Collectible Collider (collects pickups)
        if (other.gameObject.CompareTag ("Reeses"))
        {
            other.gameObject.SetActive (false);
        }

        if (other.gameObject.CompareTag ("Phone"))
        {
            other.gameObject.SetActive (false);
        }
    }
}
