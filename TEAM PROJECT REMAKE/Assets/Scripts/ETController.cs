using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ETController : MonoBehaviour
{
    //...............................................Movement Variables
    public float baseSpeed = 3.0f;
    private float speedVar;
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    //...............................................Energy Variables
    private int newPosx = 0;
    private int newPosy = 0;
    private int oldPosx = 0;
    private int oldPosy = 0;
    private int distanceTraveled = 0;
    public int energyDepletionMultiplier = 10;




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
        Vector2 newPosition = rigidbody2d.position;
        Vector2 oldPosition = rigidbody2d.position;
        newPosition.x = oldPosition.x + speedVar * horizontal * Time.deltaTime;
        newPosition.y = oldPosition.y + speedVar * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(newPosition);

        //convert to integers, because Mathf.Abs needs to for some reason
        newPosx = (int)newPosition.x;
        newPosy = (int)newPosition.y;
        oldPosx = (int)oldPosition.x;
        oldPosy = (int)oldPosition.y;

        //make the new positions into old
        oldPosition.x = newPosition.x;
        oldPosition.y = newPosition.y;

        //find distance traveled
        distanceTraveled = Mathf.Abs(newPosx - oldPosx) + Mathf.Abs(newPosy - oldPosy);
        print("Distance Traveled:" + distanceTraveled);

        //adjust energy based on distance traveled
        EnergyScript.totalEnergy = EnergyScript.totalEnergy - distanceTraveled * energyDepletionMultiplier / 2;
        print("Energy:" + EnergyScript.totalEnergy);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //...........................................Collectible Collider (collects pickups)
        if (other.gameObject.CompareTag("Reeses"))
        {
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Phone"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
