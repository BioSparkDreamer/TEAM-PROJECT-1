using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyScript : MonoBehaviour
{
    public int totalEnergy;
    // Start is called before the first frame update
    void Start()
    {
        totalEnergy = 9999;
        energyText = GetComponent<Text>();
        SetEnergyText ();
    }

    // Update is called once per frame
    void Update()
    {
        if ( WHAT DO I PUT HERE)
        {
            totalEnergy = totalEnergy - 1 * dmgFreq;
            SetEnergyText ();
        }
    }

    void SetEnergyText()
    {
        energyText.text = "" + totalEnergy;
    }
}
