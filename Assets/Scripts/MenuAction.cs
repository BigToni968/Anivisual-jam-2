using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MenuAction : MonoBehaviour
{
    List<Button> buts = null;
    CanvasGroup fade = null;
    AudioSource music = null;

    [SerializeField]
    AudioClip clip = null;

    bool isAuthors = false;
    bool isPanelOpened = false;
    bool isLoad = false;

    PanelScript panelScript = null;

    private void Awake()
    {
        buts =new List<Button>(GetComponentsInChildren<Button>());
        foreach(Button button in buts)
            button.onClick.AddListener(() => onClick(button));

        fade = GameObject.Find("Fade").GetComponent<CanvasGroup>();
        music = GameObject.Find("Canva").GetComponent<AudioSource>();
        panelScript = GameObject.Find("Canva").GetComponent<PanelScript>();
    }

    private void onClick(Button b)
    {
        switch (b.name)
        {
            case "Game":
                if (clip != null) music.PlayOneShot(clip);
                nextScene();
                break;
            case "Load":
                float time = 0;
                if (clip != null) music.PlayOneShot(clip);
                if (!isPanelOpened)
                {
                    panelScript.PanelOpen();
                    isPanelOpened = true;
                    time = 1;
                }

                if (isAuthors)
                {
                    panelScript.Authors_haed_Invible();
                    time++;
                    isAuthors = false;
                }

                if (!isLoad)
                {
                    panelScript.Load_Visible(time);
                    isLoad = true;
                }
                else
                {
                    panelScript.Load_Invible();
                    panelScript.PanelClsoe(1);
                    isPanelOpened = false;
                    isLoad = false;
                }

                break;
            case "Authors":
                time = 0;
                if (clip != null) music.PlayOneShot(clip);

                if (!isPanelOpened)
                {
                    panelScript.PanelOpen();
                    isPanelOpened = true;
                    time = 1;
                }

                if (isLoad)
                {
                    panelScript.Load_Invible();
                    time++;
                    isLoad = false;
                }

                if (!isAuthors)
                {
                    panelScript.Authors_haed_Visible(time);
                    isAuthors = true;
                }
                else
                {
                    panelScript.Authors_haed_Invible();
                    panelScript.PanelClsoe(1);
                    isPanelOpened = false;
                    isAuthors = false;
                }

                break;
            case "Exit":
                if (clip != null) music.PlayOneShot(clip);
                Application.Quit();
                break;
        }
    }

    IEnumerator Visible()
    {
        for (float i = 0; i < 1; i += .01f)
        {
            fade.alpha = i;
            yield return new WaitForSeconds(1 / 100);
        }
        SceneManager.LoadScene(2);
    }

    IEnumerator volumeDown()
    {
        for (float i = 1; i >= 0; i -= .01f)
        {
            music.volume = i;
            yield return new WaitForSeconds(1 / 100);
        }
        music.Stop();
    }

    public void nextScene()
    {
        StartCoroutine(Visible());
        StartCoroutine(volumeDown());
    }

}
