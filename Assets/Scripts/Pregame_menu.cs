using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Pregame_menu : MonoBehaviour
{
    [SerializeField]
    [Header("Трек на фон")]
    AudioClip clip = null;

    CanvasGroup fade = null;
    AudioSource music = null;

    private void Awake()
    {
        fade = GameObject.Find("Fade").GetComponent<CanvasGroup>();
        music = GetComponent<AudioSource>();
        music.clip = clip;
    }

    private void Start()
    {
        StartCoroutine(Invisible());
        StartCoroutine(volumeUp());
    }

    IEnumerator Visible()
    {
        for (float i = 0; i < 1; i += .01f)
        {
            fade.alpha = i;
            yield return new WaitForSeconds(1/100);
        }
    }

    IEnumerator Invisible()
    {
        for (float i = 1; i> .1f; i -= .01f)
        {
            fade.alpha = i;
            yield return new WaitForSeconds(1 / 100);
        }
    }

    IEnumerator volumeUp()
    {
        music.Play();
        for (float i = 0; i < 1; i += .01f)
        {
            music.volume = i;
            yield return new WaitForSeconds(1 / 100);
        }
    }

    IEnumerator volumeDown()
    {
        for (float i = 1; i > .1f; i -= .01f)
        {
            music.volume = i;
            yield return new WaitForSeconds(1 / 100);
        }
        music.Stop();
    }

}
