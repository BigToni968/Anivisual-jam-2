using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    //AudioClip clip = null;
    GameObject camera = null;
    AudioSource source = null;
    bool isAuthor = false;
    private void Awake ()
    {
        // clip = Resources.Load("Sound/Music/Piano",typeof(AudioClip)) as AudioClip;
        camera = GameObject.Find("Main Camera");
        source = GameObject.Find("MusicPlay").GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(comon());
    }

    public void onClickPlay()
    {
        SceneManager.LoadScene(2);
    }

    public void onClickExit()
    {
        StartCoroutine(back());
    }

    public void authorClicks()
    {
        isAuthor = !isAuthor;
        StopAllCoroutines();
        if (isAuthor)
            StartCoroutine(authorUp());
        else
            StartCoroutine(authoeDown());
    }

    IEnumerator comon()
    {
        camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, -5);
        while ((int)camera.transform.position.z != (int)0)
        {
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z + 0.1f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator authoeDown()
    {
        while ((int)camera.transform.position.z != (int)-1)
        {
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z - 0.1f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator authorUp()
    {
        while ((int)camera.transform.position.z != 6)
        {
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z + 0.1f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator back()
    {
        while (camera.transform.position.z!=- 5) {
            camera.transform.position = new Vector3(camera.transform.position.x,camera.transform.position.y,camera.transform.position.z -0.1f);
            yield return new WaitForSeconds(0.05f);
        }
        Application.Quit();
    }

    public void musicPlay()
    {
        StartCoroutine(musicsPlay());
    }

    IEnumerator musicsPlay()
    {
        float time = 1;
        for (float i = 0; i <= 1.0; i += .1f)
        {
            source.volume = i;
            yield return new WaitForSeconds(time);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
