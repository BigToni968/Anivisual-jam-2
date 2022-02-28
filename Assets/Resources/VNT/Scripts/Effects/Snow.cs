using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Snow : MonoBehaviour
{
    CanvasGroup alpha = null;
    Animator snow = null;
    Image img = null;

    [SerializeField]
    [Header("Скорость снега.")]
    [Range(0, 5)]
    float speedSnow = 1;

    [SerializeField]
    [Header("Цвет снега.")]
    Color color = new Color(1, 1, 1, 1);

//    AudioClip clip = null;
//    AudioSource source = null;

    private void Awake()
    {
        alpha = GetComponent<CanvasGroup>();
        snow = GetComponent<Animator>();
        img = GetComponent<Image>();
        alpha.alpha = 0;
        snow.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
  //      clip = Resources.Load("Sound/Effects/rain", typeof(AudioClip)) as AudioClip;
  //      source = GameObject.Find("Effect").GetComponent<AudioSource>();
 //       source.clip = clip;
    }

    public void Show()
    {
   //     source.Play();
        StartCoroutine(Visible());
    }

    public void Hide()
    {
//        source.Stop();
        StartCoroutine(InVisible());
    }

    IEnumerator Visible()
    {
        snow.cullingMode = AnimatorCullingMode.AlwaysAnimate;

        for (float i = 0; i <= 1; i += .01f)
        {
            alpha.alpha = i;
     //       source.volume = i;
            yield return new WaitForSeconds(2 / 100);
        }
    }

    IEnumerator InVisible()
    {
        for (float i = 1; i >= 0; i -= .01f)
        {
            alpha.alpha = i;
       //     source.volume = i;
            yield return new WaitForSeconds(2 / 100);
        }

        snow.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
    }

    private void FixedUpdate()
    {
        snow.speed = speedSnow;
        img.color = color;
    }
}
