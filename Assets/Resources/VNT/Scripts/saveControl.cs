using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveControl
{
    string dbName = "TCOAM";
    private static saveControl instance;
    private static readonly object _lock = new object();
    json json = new json();
    public static saveControl getInstance()
    {
        if (instance == null)
        {
            lock (_lock)
            {
                if (instance == null)
                    instance = new saveControl();
            }
        }
        return instance;
    }

    public void setSave(json json)
    {
        this.json = json;
        string tmp = JsonUtility.ToJson(json);
        PlayerPrefs.SetString(dbName,tmp);
        PlayerPrefs.Save();
    }

    public json getSave()
    {
        if (PlayerPrefs.HasKey(dbName))
        {
            string tmp = PlayerPrefs.GetString(dbName);
            json = JsonUtility.FromJson<json>(tmp);
            return this.json;
        }
        return json;
    }

}
