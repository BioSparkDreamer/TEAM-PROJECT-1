using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ETController : MonoBehaviour
{
    //...............................................Movement Variables
    public float baseSpeed = 3.0f;
    private float speedVar;
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    bool isometricPerspective = true;
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
    //...............................................Audio Variables
    AudioSource audioSource;
    public AudioClip background;
    public AudioClip win;
    public AudioClip lose;
    //...............................................Collectible Variables
    private int count;


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
        //...........................................Collectible Count
        count = 0;
        //...........................................Audio Instantiation
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = background;
        audioSource.Play();
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

        //...........................................Destroy ET Timer/Lose Condition
        if (TimerScript.timeLeft <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(3);

            //Swtiching to lose audio
            audioSource.clip = lose;
            audioSource.Play();
        }
    }

    void FixedUpdate()
    {
        //...........................................Movement FixedUpdate (react to input)
        Vector2 newPosition = rigidbody2d.position;
        Vector2 oldPosition = rigidbody2d.position;        

        //...........................................Normal Movement (hole, side to side only)
        if (isFloating == false && isometricPerspective == false)
        {
            newPosition.x = oldPosition.x + speedVar * horizontal * Time.deltaTime;
        }

        //...........................................Float action (hole, up only)
        //calculate float time remaining and float distance
        if (isFloating == true && isometricPerspective == false)
        {
            newPosition.y = oldPosition.y + floatSpeed * Time.deltaTime * 1;
            floatDown = true;
        }

        //...........................................Normal Movement (overworld)
        if (isFloating == false && isometricPerspective == true)
        {
            newPosition.x = oldPosition.x + speedVar * horizontal * Time.deltaTime;
            newPosition.y = oldPosition.y + speedVar * vertical * Time.deltaTime;
        }

        //...........................................Float action (overworld)
        //calculate float time remaining and float distance
        if (isFloating == true && isometricPerspective == true)
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

        //...........................................Collectible Win Condition
        if (count == 3)
        {
            SceneManager.LoadScene(4);

            //Switch to win audio
            audioSource.clip = win;
            audioSource.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //...........................................Collectible Collider (collects pickups)
        if (other.gameObject.CompareTag("Reeses"))
        {
            other.gameObject.SetActive(false);
            EnergyScript.totalEnergy = EnergyScript.totalEnergy + 390;
        }

        if (other.gameObject.CompareTag("Phone"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

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
    public void TeleportToHole(float teleportLocationX, float teleportLocationY, bool isoPers)
    {
        transform.position = new Vector2(teleportLocationX, teleportLocationY);
        isometricPerspective = isoPers;
    }

    //...........................................Sound switching function for one clip audio 
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}

