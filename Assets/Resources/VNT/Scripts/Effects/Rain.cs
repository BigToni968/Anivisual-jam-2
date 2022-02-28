using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Rain : MonoBehaviour
{
    CanvasGroup alpha = null;
    Animator rain = null;
    Image img = null; 

    [SerializeField]
    [Header("Скорость дождя.")]
    [Range(0,5)]
    float speedRain = 1;

    [SerializeField]
    [Header("Цвет дождя.")]
    Color color = new Color(1,1,1,1);

    AudioClip clip = null;
    AudioSource source = null;

    private void Awake()
    {
        alpha = GetComponent<CanvasGroup>();
        rain = GetComponent<Animator>();
        img = GetComponent<Image>();
        alpha.alpha = 0;
        rain.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
        clip = Resources.Load("Sound/Effects/rain",typeof(AudioClip)) as AudioClip;
        source = GameObject.Find("Effect").GetComponent<AudioSource>();
        source.clip = clip;
    }

    public void Show()
    {
        source.Play();
        StartCoroutine(Visible());
    }

    public void Hide()
    {
       source.Stop();
        StartCoroutine(InVisible());
    }

    IEnumerator Visible()
    {
        rain.cullingMode = AnimatorCullingMode.AlwaysAnimate;

        for (float i = 0; i <= 1; i += .01f)
        {
            alpha.alpha = i;
            source.volume = i;
            yield return new WaitForSeconds(2 / 100);
        }
    }

    IEnumerator InVisible()
    {
        for (float i = 1; i >= 0; i -= .01f)
        {
            alpha.alpha = i;
            source.volume = i;
            yield return new WaitForSeconds(2 / 100);
        }

        rain.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
    }

    private void FixedUpdate()
    {
        rain.speed = speedRain;
        img.color = color;
    }
}
