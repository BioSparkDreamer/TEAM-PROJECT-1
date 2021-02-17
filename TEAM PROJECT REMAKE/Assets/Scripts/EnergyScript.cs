using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyScript : MonoBehaviour
{
    //...............................................Variables
    public static int totalEnergy = 9999;
    public Text energyText;

    //...............................................Instantiate
    void Start()
    {
        energyText = GetComponent<Text>();
        //energyText = GetComponent<Text>();
        //SetEnergyText ();
    }

   
    void Update()
    {
        energyText.text = totalEnergy.ToString();
    }
}
