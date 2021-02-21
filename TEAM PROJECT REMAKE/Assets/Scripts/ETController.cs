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
    //...............................................Movement Tracking Variables
    private int newPosx = 0;
    private int newPosy = 0;
    private int oldPosx = 0;
    private int oldPosy = 0;
    private int distanceTraveled = 0;
    //...............................................Energy Variables
    public int energyDepletionMultiplier = 78;
    //...............................................Float Action Variables
    private bool isFloating = false;
    public float floatActionDuration = 0.75f;
    public float floatActionSpeed = 3.0f;
    private float floatDuration;
    private float floatSpeed;
    private bool floatDown = false;
    //...............................................Animation Variables
    Animator anim;
    private bool facingRight = true;


    void Start()
    {
        //...........................................Movement Instantiation
        speedVar = baseSpeed;
        rigidbody2d = GetComponent<Rigidbody2D>();

        //Animation initialization
        anim = GetComponent<Animator>();

        //float action instantiation
        floatDuration = floatActionDuration;
        floatSpeed = floatActionSpeed;
    }


    void Update()
    {
        //...........................................Movement Detection
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //...........................................Float Action Detection
        if (Input.GetKeyDown(KeyCode.Space) && isFloating == false)
        {
            isFloating = true;
        }

        //...........................................Destroy ET Timer
        if (TimerScript.timeLeft <= 0)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        //...........................................Movement FixedUpdate (react to input)
        Vector2 newPosition = rigidbody2d.position;
        Vector2 oldPosition = rigidbody2d.position;

        //...........................................Normal Movement (overworld)
        if(isFloating == false)
        {
            newPosition.x = oldPosition.x + speedVar * horizontal * Time.deltaTime;
            newPosition.y = oldPosition.y + speedVar * vertical * Time.deltaTime;
        }      

        //...........................................Float action
        //calculate float time remaining and float distance
        if (isFloating == true)
        {
            if (floatDown == false)
            {
                floatDuration = floatDuration - Time.deltaTime;

                newPosition.y = oldPosition.y + floatSpeed * Time.deltaTime * 1;

                if (floatDuration < 0)
                {
                    floatDown = true;
                }
            }

            if (floatDown == true)
            {
                floatDuration = floatDuration + Time.deltaTime;

                newPosition.y = oldPosition.y + floatSpeed * Time.deltaTime * -1;

                if (floatDuration > floatActionDuration)
                {
                    floatDuration = floatActionDuration;
                    floatDown = false;
                    isFloating = false;
                }
            }
        }

        //...........................................Move Character
        rigidbody2d.MovePosition(newPosition);

        //...........................................Track Movement
        //convert to integers, because Mathf.Abs needs to for some reason
        newPosx = (int)newPosition.x;
        newPosy = (int)newPosition.y;
        oldPosx = (int)oldPosition.x;
        oldPosy = (int)oldPosition.y;

        //make the new position data from this frame into "old" for next frame
        oldPosition.x = newPosition.x;
        oldPosition.y = newPosition.y;

        //find distance traveled
        distanceTraveled = Mathf.Abs(newPosx - oldPosx) + Mathf.Abs(newPosy - oldPosy);
        print("Distance Traveled:" + distanceTraveled);

        //...........................................Adjust Energy (based on distance traveled)
        EnergyScript.totalEnergy = EnergyScript.totalEnergy - distanceTraveled * energyDepletionMultiplier / 2;
        print("Energy:" + EnergyScript.totalEnergy);

        //...........................................Animation
        //Flipping the sprite
        if (facingRight == false && horizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && horizontal < 0)
        {
            Flip();
        }

        //Animation states (0 = idle, 1 = walking, 2 = floating)
        if(distanceTraveled > 0 && isFloating == false)
        {
            anim.SetInteger("State", 1);
        }
        if(distanceTraveled == 0 && isFloating == false)
        {
            anim.SetInteger("State", 0);
        }
        if(isFloating == true)
        {
            anim.SetInteger("State", 2);
        }
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

    //...........................................Flip animation
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    //...........................................Teleport to Hole (called by HoleTeleporter Script)
    public void TeleportToHole(float teleportLocationX, float teleportLocationY)
    {
        transform.position = new Vector2(teleportLocationX, teleportLocationY);
    }
}
