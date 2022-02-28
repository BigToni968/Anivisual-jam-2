using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{

   
    List<AudioSource> audios = null;
    saveControl saveControl = saveControl.getInstance();
    json json;
    Data datas = new Data();

    private void Awake()
    {
        audios = new List<AudioSource>(GetComponentsInChildren<AudioSource>());
        json = saveControl.getSave();
        datas.setVolume(json.Volume);
    }

    public void playBG(AudioClip clip)
    {
        StopAllCoroutines();
        foreach (AudioSource tmp in audios)
            if (tmp.name.ToLower().Equals("back"))
            {
                tmp.clip = clip;
                tmp.Play();
                StartCoroutine(upVolume(tmp));
            }
    }

    public void stopBG()
    {
        StopAllCoroutines();
        foreach (AudioSource tmp in audios)
            if (tmp.name.ToLower().Equals("back"))
                StartCoroutine(downVolume(tmp));
    }

    public void playSound(AudioClip clip)
    {
        foreach (AudioSource tmp in audios)
            if (tmp.name.ToLower().Equals("sound"))
                tmp.PlayOneShot(clip);
            
    }

    public void playVoice(AudioClip clip)
    {
        foreach (AudioSource tmp in audios)
            if (tmp.name.ToLower().Equals("voice"))
            {
                if (tmp.isPlaying)
                {
                    tmp.Stop();
                    tmp.clip = clip;
                    tmp.Play();
                }
                else
                {
                    tmp.clip = clip;
                    tmp.Play();
                }
            }
    }

    IEnumerator upVolume(AudioSource au)
    {
        au.volume = 0;
        while (au.volume != datas.getVolume())
        {
            au.volume += 0.01f;
            yield return new WaitForSeconds(1 / 100);
        }
    }

    IEnumerator downVolume(AudioSource au)
    {
        au.volume = datas.getVolume();
        while(au.volume != 0.00f)
        {
            au.volume -= 0.01f;
            yield return new WaitForSeconds(1/100);
        }
        au.Stop();
    }

}
