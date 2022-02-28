using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class BG : MonoBehaviour
{
    [SerializeField]
    [Header("Цвет фона по стандарту.")]
    Color zero = new Color(0.1921569f, 0.1921569f, 0.1921569f,1);

    [SerializeField]
    [Header("Переход")]
    bool isFade = false;

    [SerializeField]
    [Header("Время перехода")]
    [Range(1,5)]
    float time = 1;

    CanvasGroup alpfa = null;
    Image bg = null;

    private void Awake()
    {
        bg = GetComponentInChildren<Image>();
        alpfa = bg.GetComponent<CanvasGroup>();
    }

    void Start()
    {
        
    }

    public void Visible(string spriteName)
    {
        Sprite sprite = Resources.Load("Graphics/Backgrounds/"+spriteName.ToLower().Trim(),typeof(Sprite)) as Sprite;
        if (sprite != null)
        {
            StopAllCoroutines();
            bg.color = new Color(1,1,1,1);
            bg.sprite = sprite;
            if (isFade)
                StartCoroutine(Show());
            else
                alpfa.alpha = 1;
        }
        else
            Visible();
    }

    public void Visible()
    {
        StopAllCoroutines();
        bg.color = zero;
        if (isFade)
            StartCoroutine(Show());
        else
            alpfa.alpha = 1;
    }

    public void Invisible()
    {
        StopAllCoroutines();
        if (isFade)
            StartCoroutine(Hide());
        else
            alpfa.alpha = 0;
    }

    IEnumerator Show()
    {
        for (float i = 0; i <= 1; i += 0.01f)
        {
            alpfa.alpha = i;
            yield return new WaitForSeconds(time/100);
        }
    }

    IEnumerator Hide()
    {
        for (float i = 1; i >= 0; i -= 0.01f)
        {
            alpfa.alpha = i;
            yield return new WaitForSeconds(time / 100);
        }
    }
}
