using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Select : MonoBehaviour
{
    Button[] MyButtons = new Button[5];
    int activebut = 0;

    Jump jump = new Jump();
    Data datas = new Data();
    Commands commandsScript = null;

    private void Awake()
    {
        MyButtons = GetComponentsInChildren<Button>();
        commandsScript = GameObject.Find("VNT").GetComponent<Commands>();
        foreach (Button b in MyButtons)
            b.enabled = false;
    }

    public void Setup(string conf)
    {
        if (conf.Contains("|"))
            activebut = conf.Split('|').Length;
        else
            activebut = 0;
        if (activebut > 5)
            activebut = 5;
        string[] subtmp = conf.Split('|');
        GetComponent<Canvas>().enabled = true;
        for (int i = 0; i < activebut; i++)
        {
            string[] tmp = subtmp[i].Split('=');
//            Debug.Log(tmp.Length);
//            Debug.Log(tmp[0]+" == "+ tmp[1]+" == "+ tmp[2]);
            MyButtons[i].name = tmp[3];
            MyButtons[i].GetComponentInChildren<Text>().text = tmp[1];
            MyButtons[i].GetComponent<CanvasGroup>().alpha = 1;
            MyButtons[i].enabled = true;
        }
        foreach (Button b in MyButtons)
            b.onClick.AddListener(()=>onClick(b));
    }

    public void onClick(Button but)
    {
        datas.setNumberString(jump.getLabel(but.name));
        datas.setClick(true);
        for (int i = 0; i < activebut; i++)
        {
            MyButtons[i].GetComponent<CanvasGroup>().alpha = 0;
            MyButtons[i].enabled = false;
        }
        GetComponent<Canvas>().enabled = false;
        commandsScript.executeCommands(commandsScript.scenario[datas.getNumberString()]);
    }

    public void Invisible()
    {
        datas.setClick(true);
        for (int i = 0; i < activebut; i++)
        {
            MyButtons[i].GetComponent<CanvasGroup>().alpha = 0;
            MyButtons[i].enabled = false;
        }
        GetComponent<Canvas>().enabled = false;
    }
}
