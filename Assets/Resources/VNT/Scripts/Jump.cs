using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump
{
    static Dictionary<string, int> label = new Dictionary<string, int>();
    public void addLabel(string gt,int numberLabel)
    {
        label.Add(gt,numberLabel);
    }

    public int getLabel(string gt)
    {
        if (label != null)
            if (label.Count > 0)
                foreach (KeyValuePair<string, int> tmp in label)
                    if (tmp.Key.ToLower().Equals(gt.ToLower()))
                        return tmp.Value;
        return 0;
    }
}
