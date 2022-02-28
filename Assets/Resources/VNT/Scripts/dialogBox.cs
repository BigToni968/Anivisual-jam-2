using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class dialogBox : MonoBehaviour
{
    [SerializeField]
    [Header("Переход")]
    bool isFade = false;
    [SerializeField]
    [Header("Время перехода")]
    [Range(1,5)]
    float time = 1;
    [SerializeField]
    [Header("Посимвольный вывод")]
    bool isChar = false;
    [SerializeField]
    [Header("Время вывода")]
    [Range(1, 5)]
    float timeChar = 1;

    CanvasGroup Storyalpfa = null;
    CanvasGroup NameAlpfa = null;

    Text story = null;
    Text names = null;

    Data datas = new Data();
    Character character;

    private void Awake()
    {
        Storyalpfa = GameObject.Find("Box").GetComponent<CanvasGroup>();
        NameAlpfa = GameObject.Find("NameBox").GetComponent<CanvasGroup>();
        story = GameObject.Find("Story").GetComponent<Text>();
        names = GameObject.Find("Name").GetComponent<Text>();
    }

    void Start()
    {
        
    }

    public void Say(string str)
    {
            StopAllCoroutines();
            StartCoroutine(Draw(str));
    }

    IEnumerator ShowBox()
    {
        for (float i = 0; i <= 1; i += 0.01f)
        {
            Storyalpfa.alpha = i;
            yield return new WaitForSeconds(time/100);
        }
    }

    IEnumerator ShowNameBox()
    {
        for (float i = 0; i <= 1; i += 0.01f)
        {
            NameAlpfa.alpha = i;
            yield return new WaitForSeconds(time / 100);
        }
    }

    IEnumerator HideNameBox()
    {
        for (float i = 1; i >= 0; i -= 0.01f)
        {
            NameAlpfa.alpha = i;
            yield return new WaitForSeconds(time / 100);
        }
    }

    IEnumerator Draw(string str)
    {
        if (isFade && Storyalpfa.alpha == 0)
            StartCoroutine(ShowBox());
        else
            Storyalpfa.alpha = 1;

        if (str.Contains("/"))
        {
            if (isFade && NameAlpfa.alpha == 0)
                StartCoroutine(ShowNameBox());
            else
                NameAlpfa.alpha = 1;
            character = datas.getCharacter(str.Split('/')[0]);
            names.text = character.getName();
            names.color = character.getColorName();
            str = str.Split('/')[1];
        }
        else
            NameAlpfa.alpha = 0;

        datas.setEndString(false);

        if (isChar)
            for (int i = 0; i <= str.Length; i++)
            {
                story.text = str.Substring(0, i);
                yield return new WaitForSeconds(timeChar / str.Length);
            }
        else
            story.text = str;

        datas.setEndString(true);
    }

}
