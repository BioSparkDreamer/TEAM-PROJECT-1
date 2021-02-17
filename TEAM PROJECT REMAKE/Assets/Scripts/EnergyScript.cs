using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyScript : MonoBehaviour
{
<<<<<<< HEAD
    //...............................................VARIABLES
    public static int totalEnergy = 9999;
    public Text energyText;

    //...............................................INSTANTIATE
=======
    public int totalEnergy;
    // Start is called before the first frame update
>>>>>>> parent of aaad97e (Energy Counter Hooked up)
    void Start()
    {
        totalEnergy = 9999;
        energyText = GetComponent<Text>();
<<<<<<< HEAD
    }


    //...............................................UPDATE
=======
        SetEnergyText ();
    }

    // Update is called once per frame
>>>>>>> parent of aaad97e (Energy Counter Hooked up)
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
