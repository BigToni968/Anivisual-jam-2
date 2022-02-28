using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Flashing : MonoBehaviour
{
    Color[] colors = new Color[2];

    private void Awake()
    {
        colors[0] = Color.blue;
//        colors[1] = Color.red;
//        colors[2] = Color.yellow;
 //       colors[3] = Color.green;
        colors[1] = new Color(128, 0, 255);
        //       colors[5] = new Color(255, 91, 0);
    }

    public void setColor()
    {
        if (GetComponentInChildren<Image>().color == colors[0])
            GetComponentInChildren<Image>().color = colors[1];
        else
            GetComponentInChildren<Image>().color = colors[0];
    }

}
