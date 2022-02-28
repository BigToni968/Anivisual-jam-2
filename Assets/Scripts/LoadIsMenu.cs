using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadIsMenu : MonoBehaviour
{
    [SerializeField]
    [Header("Окнo загрузки:")]
    GameObject panelLoad = null;
    [SerializeField]
    [Header("Here окна сохранить и загрузить")]
    Text hereLoad = null;
    Button[] loadBut = null;

    int hereL = 1;

    Canvas canvas = null;
    Canvas[] canvasTmp;

    [SerializeField]
    AudioClip clip = null;
    AudioSource music = null;

    saveControl saveControl = saveControl.getInstance();
    json json;

    Data datas = new Data();

    MenuAction menuAction = null;

    private void Awake()
    {
        menuAction = GameObject.Find("Buttons").GetComponent<MenuAction>();
    }

    void Start()
    {
        canvasTmp = GetComponentsInChildren<Canvas>();
        foreach (Canvas c in canvasTmp)
        {
            if (c.name.Equals("Canvas"))
                canvas = c;
        }

        music = GameObject.Find("Canva").GetComponent<AudioSource>();

        json = saveControl.getSave();

        if (panelLoad != null)
        {
            loadBut = panelLoad.GetComponentsInChildren<Button>();
            foreach (Button b in loadBut)
                b.onClick.AddListener(() => buttonLoad(b));
        }
        
        refreshButtons(loadBut, hereL);
    }


    private void buttonLoad(Button button)
    {
        switch (button.name)
        {
            case "Back":
                if(clip != null) music.PlayOneShot(clip);
                if (hereL > 1)
                    hereL--;
                break;
            case "Next":
                if(clip != null) music.PlayOneShot(clip);
                if (hereL < 100)
                    hereL++;
                break;
            default:
                string tmpsavecount = null;
                if (hereL < 2)
                    tmpsavecount = (int.Parse(button.name.Split('_')[1]) * hereL).ToString();
                else
                    tmpsavecount = (9 * (hereL - 1) + int.Parse(button.name.Split('_')[1])).ToString();
                if (File.Exists(Application.persistentDataPath + "/Saves/Save_" + tmpsavecount + ".jpg"))
                {
                    datas.setNumberString(json.subIndex[int.Parse(tmpsavecount) - 1] - 1);
                    if (menuAction != null)
                        menuAction.nextScene();
                   // commands.executeCommands(commands.scenario[datas.getNumberString()]);
                    //commands.Renow();
                }
                break;
        }
        refreshButtons(loadBut, hereL);
        hereRehresh(hereLoad, hereL);
    }

    void hereRehresh(Text here, int herecount)
    {
        if (here != null)
            here.text = "Страница  " + herecount;
    }


    void refreshButtons(Button[] buttons, int hereHere)
    {
        List<Button> tmp2 = new List<Button>(buttons);
        int res = 0;
        for (int j = 1; j <= 9; j++)
        {
            if (hereHere < 2)
                res = j * hereHere;
            else
                res = (9 * (hereHere - 1) + j);
            for (int i = 0; i < json.pathScreenSave.Length; i++)
            {
                if (json.pathScreenSave[i] != null && json.pathScreenSave[i] != "")
                    if (File.Exists(Application.persistentDataPath + "/Saves/Save_" + json.pathScreenSave[i].Split('_')[1] + ".jpg"))
                        for (int c = 0; c < tmp2.Count; c++)
                        {
                            if (tmp2[c].name.Equals(tmp2[c].name.Split('_')[0] + "_" + json.pathScreenSave[i].Split('_')[0]) && res == int.Parse(json.pathScreenSave[i].Split('_')[1]))
                            {
                                Texture2D texture2D = new Texture2D(1, 1);
                                byte[] bytes = File.ReadAllBytes(Application.persistentDataPath + "/Saves/Save_" + json.pathScreenSave[i].Split('_')[1] + ".jpg");
                                texture2D.LoadImage(bytes);
                                tmp2[c].GetComponent<Image>().sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(.5f, .5f));
                                tmp2.RemoveAt(c);
                            }
                            else
                            {
                                if (tmp2[c].GetComponent<Image>().name.Trim() != "Back" && tmp2[c].GetComponent<Image>().name.Trim() != "Next")
                                {
                                    tmp2[c].GetComponent<Image>().sprite = Resources.Load("Graphics/GUI/name_box", typeof(Sprite)) as Sprite;
                                }
                            }
                        }
            }

        }
    }


}
