using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyScript : MonoBehaviour
{
    //...............................................VARIABLES
    public static int totalEnergy = 9999;
    public Text energyText;

    //...............................................INSTANTIATE
    void Start()
    {
        energyText = GetComponent<Text>();
    }


    //...............................................UPDATE
    void Update()
    {
        energyText.text = totalEnergy.ToString();
    }
}
