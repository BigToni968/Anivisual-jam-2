using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    [Header("Имя персонажа")]
    string names = "Character";

    [SerializeField]
    [Header("Цвет имени")]
    Color color = Color.white;

    [SerializeField]
    [Header("Спрайты персонажа")]
    Sprite[] sprites = null;

    [SerializeField]
    [Header("Переход")]
    bool isFade = false;

    [SerializeField]
    [Header("Время")]
    [Range(1,5)]
    float timeFade = 1;

    Data data = new Data();
    
    private void Awake()
    {
        data.addCharacter(this);
    }

    public string getName()
    {
        return names;
    }

    public Color getColorName()
    {
        return color;
    }

    public Sprite GetSprite(string nameSprite)
    {
        if (sprites != null)
            if (sprites.Length > 0)
                foreach (Sprite sprite in sprites)
                    if (sprite.name.ToLower().Equals(nameSprite.ToLower().Trim()))
                        return sprite;
        return null;
    }

    public Sprite GetSprite()
    {
        if (sprites != null)
           if( sprites.Length > 0)
                return sprites[0];
        return null;
    }

    public bool Fade()
    {
        return isFade;
    }

    public float Time()
    {
        return timeFade;
    }

}
