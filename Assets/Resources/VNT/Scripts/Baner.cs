using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Baner : MonoBehaviour
{
    [SerializeField]
    [Header("Время")]
    [Range(1, 5)]
    float time = 1;

    Canvas canvas = null;
    CanvasGroup alphaBack = null;
    CanvasGroup alphaText = null;
    Text banerText = null;
    string str = null;

    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
        alphaBack = GetComponentsInChildren<CanvasGroup>()[0];
        alphaText = GetComponentsInChildren<CanvasGroup>()[1];
        banerText = GetComponentInChildren<Text>();
        banerText.text = "";
    }

    void Start()
    {
        
    }

    public void Visible(string str)
    {
        this.str = str;
        canvas.enabled = true;
        if (alphaBack.alpha < 1)
            StartCoroutine(showBack());
        else
            StartCoroutine(showText());
    }

    IEnumerator showBack()
    {
        for(float i = 0;i<=1; i += .01f)
        {
            alphaBack.alpha = i;
            yield return new WaitForSeconds(time/100);
        }
        StopAllCoroutines();
        StartCoroutine(showText());
    }

    IEnumerator showText()
    {
        banerText.text = this.str;
        for (float i = 0; i <= 1; i += .01f)
        {
            alphaText.alpha = i;
            yield return new WaitForSeconds(time / 100);
        }
        StopAllCoroutines();
        StartCoroutine(hideText());
    }

    IEnumerator hideText()
    {
        for (float i = 1; i >= 0; i -= .01f)
        {
            alphaText.alpha = i;
            yield return new WaitForSeconds(time / 100);
        }
        banerText.text = "";
        canvas.enabled = false;
    }

}
