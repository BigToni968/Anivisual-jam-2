using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Switch_Butt : MonoBehaviour
{
    Animator animator = null;

    bool isBut = false;
    bool isSave = false;
    bool isLoad = false;
    bool isSett = false;

    Data datas = new Data();

    [SerializeField]
    AudioSource clicks = null;

    [SerializeField]
    AudioSource music = null;

    [SerializeField]
    Scrollbar ScrollbarMusic = null;

    [SerializeField]
    Scrollbar ScrollbaAuto = null;

    [SerializeField]
    Text text = null;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animator.speed = 0;
        ScrollbarMusic.value = datas.getVolume();
        ScrollbaAuto.value = datas.getTimeAuto() / 5;
        if (Mathf.RoundToInt(datas.getTimeAuto() / 5)<1)
        text.text = "Авто-чтение = " +1;
        else
        text.text = "Авто-чтение = " + Mathf.RoundToInt(datas.getTimeAuto() / 5);
    }

    public void Menu_Open()
    {
        animator.speed = 1;
        animator.Play("Menu_Open");
        Button_Visible(1);
    }

    public void Menu_Close()
    {

        float timening = 0;
        if (isBut)
            timening++;

        if (isLoad)
        {
            timening++;
            Load_Close();
        }
        else if (isSave)
        {
            timening++;
            Save_Close();
        }
        else if (isSett)
        {
            timening++;
            Setting_Close();
        }
        StopAllCoroutines();
        Button_Invisible(timening-1);
        StartCoroutine(_1(timening));
    }

    public void Button_Visible(float time)
    {
        isBut = true;
        StopAllCoroutines();
        StartCoroutine(_2(time));
    }

    public void Button_Invisible(float time)
    {
        isBut = false;
        StopAllCoroutines();
        StartCoroutine(_3(time));
    }

    public void Setting()
    {
        float timening = 0;
        if (isLoad)
        {
            Load_Close();
            timening++;
        }
        else if (isSave)
        {
            Save_Close();
            timening++;
        }
        StopAllCoroutines();
        StartCoroutine(_4(timening));
    }

    public void Load()
    {
        float timening = 0;
        if (isLoad)
        {
            Load_Close();
            timening++;
        }
        else if (isSett)
        {
            Setting_Close();
            timening++;
        }
        StopAllCoroutines();
        StartCoroutine(_5(timening));
    }

    public void Save()
    {
        float timening = 0;
        if (isSave)
        {
            Save_Close();
            timening++;
        }
        else if (isSett)
        {
            Setting_Close();
            timening++;
        }
        StopAllCoroutines();
        StartCoroutine(_6(timening));
    }

    void Setting_Open()
    {
        isSett = true;
        animator.Play("Setting_Open");
    }

    void Setting_Close()
    {
        isSett = false;
        animator.Play("Setting_Close");
    }

    void Load_Open()
    {
        isLoad = true;
        animator.Play("Load_Open");
    }

    void Load_Close()
    {
        isLoad = false;
        animator.Play("Load_Close");
    }

    void Save_Open()
    {
        isSave = true;
        animator.Play("Save_Open");
    }

    void Save_Close()
    {
        isSave = false;
        animator.Play("Save_Close");
    }

    IEnumerator _1(float time)
    {
        yield return new WaitForSeconds(time);
        animator.Play("Menu_close");
    }

    IEnumerator _2(float time)
    {
        yield return new WaitForSeconds(time);
        animator.Play("Button_Visible");
    }

    IEnumerator _3(float time)
    {
        yield return new WaitForSeconds(time);
        animator.Play("Button_Invisible");
    }

    IEnumerator _4(float time)
    {
        yield return new WaitForSeconds(time);
        if (!isSett)
            Setting_Open();
        else
            Setting_Close();
    }

    IEnumerator _5(float time)
    {
        yield return new WaitForSeconds(time);
        if (!isSave)
            Save_Open();
        else
            Save_Close();
    }

    IEnumerator _6(float time)
    {
        yield return new WaitForSeconds(time);
        if (!isLoad)
            Load_Open();
        else
            Load_Close();
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void setVolume(float v)
    {
        datas.setVolume(v);
        if (music != null)
            music.volume = v;
    }

    public void setAuto(float t)
    {
        int values = Mathf.RoundToInt(t * 5);
        if (values <1)
            values = 1;
        datas.setTimeAuto(values);
        text.text = "Авто-чтение = " + values;
    }

    public void clickSound()
    {
        if (clicks != null)
            clicks.Play();
    }
}
