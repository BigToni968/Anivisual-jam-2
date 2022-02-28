using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Logo : MonoBehaviour
{
    VideoPlayer video;
    private void Awake()
    {
        video = GetComponent<VideoPlayer>();
    }
    void Start()
    {
        video.Play();
        StartCoroutine(Playning());
    }

    IEnumerator Playning()
    {
        yield return new WaitForSeconds(2f);
        while (video.isPlaying)
        {
            yield return null;
        }
        StopAllCoroutines();
        SceneManager.LoadScene(1);
    }
}
