
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    [Header("Камеры сцены")]
    Camera camera = null;
    [SerializeField]
    [Header("GameManager на сцене")]
    GameObject VNT = null;
    Commands commands = null;
    [SerializeField]
    [Header("Окна настройки, сохранения и загрузки:")]
    GameObject panelSet = null, panelSave = null, panelLoad = null;
    [SerializeField]
    [Header("Here окна сохранить и загрузить")]
    Text hereSave = null, hereLoad = null;
    Button[] saveBut = null,loadBut = null;
  
    string tmpnamebut = null;
    int hereS = 1;
    int hereL = 1;
    Texture2D TD;

    saveControl saveControl = saveControl.getInstance();
    json json;

    Data datas = new Data();

    void Start()
    {
        if (VNT != null)
            commands = VNT .GetComponent<Commands>();

        json = saveControl.getSave();

        if (panelSave != null)
        {
            saveBut = panelSave.GetComponentsInChildren<Button>();
            foreach (Button b in saveBut)
                b.onClick.AddListener(() => buttonSave(b));
        }

        if (panelLoad != null)
        {
            loadBut = panelLoad.GetComponentsInChildren<Button>();
            foreach (Button b in loadBut)
                b.onClick.AddListener(()=>buttonLoad(b));
        }

        refreshButtons(saveBut,hereS);
        refreshButtons(loadBut,hereL);
    }

    private void buttonSave(Button button)
    {
        switch (button.name)
        {
            case "Back":
                if (hereS > 1)
                    hereS--;
                break;
            case "Next":
                if (hereS < 100)
                    hereS++;
                break;
            default:
                button.interactable = false;
                tmpnamebut = button.name.Split('_')[1];
                screenCapt(tmpnamebut);
                tmpnamebut = null;
                refreshButtons(loadBut,hereL);
                button.interactable = true;
                break;
        }
        refreshButtons(saveBut,hereS);
        hereRehresh(hereSave,hereS);
    }

    private void buttonLoad(Button button)
    {
        switch (button.name)
        {
            case "Back":
                if (hereL > 1)
                    hereL--;
                break;
            case "Next":
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
                    commands.executeCommands(commands.scenario[datas.getNumberString()]);
                    commands.Renow();
                }
                break;
        }
        refreshButtons(loadBut,hereL);
        hereRehresh(hereLoad, hereL);
    }

    void hereRehresh(Text here,int herecount)
    {
        if (here != null)
        here.text = "Страница " + herecount;
    }

    void screenCapt(string save_count)
    {
        string tmpsavecount = null;
        if (hereS<2)
        tmpsavecount = (int.Parse(save_count) * hereS).ToString();
        else
            tmpsavecount = (9*(hereS-1)+int.Parse(save_count)).ToString();
        if (camera != null)
        {
            RenderTexture RT = new RenderTexture (Screen.width,Screen.height,0);
            RenderTexture cur = RenderTexture.active;
            RenderTexture.active = RT;
            camera.targetTexture = RT;
            camera.Render();
            TD = new Texture2D(RT.width,RT.height);
            TD.ReadPixels(new Rect(0,0,TD.width,TD.height),0,0);
            TD.Apply();
            RenderTexture.active = cur;
            byte[] bytes = TD.EncodeToJPG();
            if (!File.Exists(Application.persistentDataPath+ "/Saves"))
                Directory.CreateDirectory(Application.persistentDataPath + "/Saves");
            File.WriteAllBytes(Application.persistentDataPath+ "/Saves/Save_"+tmpsavecount+".jpg",bytes);
            camera.targetTexture = null;
            json.pathScreenSave[int.Parse(tmpsavecount) - 1] = (save_count + "_" + tmpsavecount.ToString());
            json.subIndex[int.Parse(tmpsavecount) - 1] = datas.getNumberString();
            saveControl.setSave(json);
        }
        tmpsavecount = null;
    }

    void refreshButtons(Button[] buttons,int hereHere)
    {
       List<Button> tmp2 =new List<Button>(buttons);
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
                        for (int c=0;c<tmp2.Count;c++)
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
                                if (!tmp2[c].GetComponent<Image>().name.Equals("Back") && !tmp2[c].GetComponent<Image>().name.Equals("Next"))
                                tmp2[c].GetComponent<Image>().sprite = null;
                            }
                        }
                }
            
         }
    }


}
