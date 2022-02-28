using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Weather : MonoBehaviour
{
    Rain Rain = null;
    Snow Snow = null;

    private void Awake()
    {
        Rain = GetComponentInChildren<Rain>();
        Snow = GetComponentInChildren<Snow>();
    }

    public void showRain()
    {
        Rain.Show();
    }

    public void hideRain()
    {
        Rain.Hide();
    }

    public void showSnow()
    {
        Snow.Show();
    }

    public void hideShow()
    {
        Snow.Hide();
    }

    private void FixedUpdate()
    {

    }
}
