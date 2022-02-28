using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subBox : MonoBehaviour
{

    Data datas = new Data();

    public void setAuto()
    {
        StopAllCoroutines();
        datas.setSkip(false);
        datas.setAuto(!datas.getAuto());
    }

    public void setSkip()
    {
        StopAllCoroutines();
        datas.setAuto(false);
        datas.setSkip(!datas.getSkip());
    }

    public void setMenu()
    {
        StopAllCoroutines();
        datas.setSkip(false);
        datas.setAuto(false);
    }

}
