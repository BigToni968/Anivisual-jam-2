using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Stage : MonoBehaviour
{
    Image[] img;
    List<Image> pos = new List<Image>();

    private void Awake()
    {
        img = GetComponentsInChildren<Image>();
        foreach (Image tmp in img)
            if (tmp.name.ToLower().Equals("left") || tmp.name.ToLower().Equals("centre") || tmp.name.ToLower().Equals("right"))
                pos.Add(tmp);
    }
    
    void Start()
    {
        
    }

    public void Visible(Sprite sprite)
    {
        foreach(Image image in pos)
            if (image.name.ToLower().Equals("centre"))
            {
                image.sprite = sprite;
                image.color = Color.white;
            }
    }

    public void Visible(Sprite sprite,float t)
    {
        foreach (Image image in pos)
            if (image.name.ToLower().Equals("centre"))
            {
                if (image.sprite == null)
                    StartCoroutine(show(image, t));
                image.sprite = sprite;
            }
    }

    public void Visible(string poss,Sprite sprite)
    {
        foreach(Image image in pos)
            if (image.name.ToLower().Equals(poss.ToLower()))
            {
                image.sprite = sprite;
                image.color = Color.white;
            }
    }

    public void Visible(string poss, Sprite sprite,float t)
    {
        foreach (Image image in pos)
            if (image.name.ToLower().Equals(poss.ToLower()))
            {
                if (image.sprite == null)
                    StartCoroutine(show(image, t));
                image.sprite = sprite;
            }
    }

    public void Invisible()
    {
        StopAllCoroutines();
        foreach(Image image in pos)
        {
            image.sprite = null;
            image.color = new Color(1,1,1,0);
        }
    }

    IEnumerator show(Image im,float t)
    {
        float tmp = 0.0f;
        while (tmp != 1f) {
            im.color = new Color(1,1,1,tmp);
            yield return new WaitForSeconds(t/100);
            tmp += 0.01f;
        }
    }
}
