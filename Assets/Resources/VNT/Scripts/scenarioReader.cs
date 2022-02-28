using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scenarioReader : MonoBehaviour
{
    [SerializeField]
    [Header("Файлы сценария")]
    TextAsset[] scenarioFiles = null;

    List<string>[] temp = null;
    List<string> scenario = new List<string>();

    Jump jump = new Jump();

    private void Awake()
    {
        read();
        clearScenario();
        setLabel();
    }

    void read()
    {
        int index = countFilesScenario();
        List<string> a;
        for(int i = 0; i < index; i++)
        {
            a = temp[i];
            for (int j = 0; j < a.Count; j++)
                scenario.Add(a[j]);
        }
    }

    int countFilesScenario()
    {
        int index = 0;
        if (scenarioFiles != null)
            foreach (TextAsset a in scenarioFiles)
                if (a != null)
                    index++;
        temp = new List<string>[index];
        index = 0;
        if (scenarioFiles != null)
            foreach (TextAsset a in scenarioFiles)
                if (a != null)
                {
                    temp[index] = new List<string>(a.text.Split(new char[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries));
                    index++;
                }
        return index;
    }

    void clearScenario()
    {
        List<string> b = new List<string>();
        if (scenario != null)
        {
            foreach (string a in scenario)
                if (!a.Contains("//") && !a.Contains("///") && !a.Contains("*") && !a.Contains("[")) 
                    b.Add(a);
            scenario = b;
            b = null;
        }
        GetComponent<Commands>().setScenario(scenario);
    }

    void setLabel()
    {
        string[] tmp = new string[scenario.Count];
        if (scenario != null)
            for (int i = 0; i < scenario.Count - 1; i++) {
                if (scenario[i].Contains("@"))
                {
                    tmp = scenario[i].ToLower().Split(new char[] { '@', ' ' });
                    if (("@"+tmp[1]).ToLower().Equals("@label"))
                        jump.addLabel(tmp[2], i);
                }
            }
        tmp = null;
    }
}
