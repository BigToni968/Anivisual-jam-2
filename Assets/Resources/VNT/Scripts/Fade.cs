using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    [SerializeField]
    [Header("Время")]
    [Range(1, 5)]
    float time = 1;

    Canvas canvas = null;
    CanvasGroup alphaBack = null;


    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
        alphaBack = GetComponentInChildren<CanvasGroup>();
    }

    public void Visisble()
    {
        canvas.enabled = true;
        StopAllCoroutines();
        StartCoroutine(show());
    }

    IEnumerator show()
    {
        for (float i = 0; i <= 1; i += .01f)
        {
            alphaBack.alpha = i;
            yield return new WaitForSeconds(time / 100);
        }
        yield return new WaitForSeconds(time);
        StopAllCoroutines();
        StartCoroutine(hide());
    }

    IEnumerator hide()
    {

        for (float i = 1; i >= 0; i -= .01f)
        {
            alphaBack.alpha = i;
            yield return new WaitForSeconds(1 / 100);
        }
        canvas.enabled = false;
    }
}
