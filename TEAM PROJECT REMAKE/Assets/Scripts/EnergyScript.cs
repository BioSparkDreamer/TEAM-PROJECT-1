using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyScript : MonoBehaviour
{
    //...............................................Variables
    public static int totalEnergy = 9999;
    public Text energyText;
    public GameObject Player;

    //...............................................Instantiate
    void Start()
    {
        energyText = GetComponent<Text>();
    }

   
    void Update()
    {
        energyText.text = totalEnergy.ToString();

        if(TimerScript.timeLeft <= 0)
        {
            Destroy(gameObject);
        }
        

        if (totalEnergy <= 0)
        {
            gameObject.SetActive (false);
            Destroy (Player);
        }
        else
        {
            gameObject.SetActive (true);
        }
    }
}
