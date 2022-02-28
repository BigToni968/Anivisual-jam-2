using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class Commands : MonoBehaviour
{
    [HideInInspector]
    public List<string> scenario = null;

    GameObject BG_Prefabs = null;
    GameObject DialogBox_Prefabs = null;
    GameObject Characters_Prefabs = null;
    GameObject Stage_Prefabs = null;
    GameObject Baner_Prefabs = null;
    GameObject Weather_Prefabs = null;
    GameObject SoundControl_Preffabs = null;
    GameObject Select_Preffabs = null;
    GameObject Fade_Preffabs = null;

    BG bgScript = null;
    dialogBox DialogBoxScript = null;
    Stage StageScript = null;
    Baner BanerScript = null;
    Weather WeatherScript = null;
    SoundControl SoundControlScript = null;
    Select SelectScript = null;
    Fade FadeScript = null;
    VideoPlayer videoPlayer = null;

    Data datas = new Data();
    Jump jump = new Jump();

    private void Awake()
    {
        setBackground();
        setDialogBox();
        setCharacters();
        setStage();
        setBaner();
        setWeather();
        setSound();
        setSelect();
        setFade();
        videoPlayer = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
    }

   void setBackground()
   {
        BG_Prefabs = GameObject.Find("Background");
        if (BG_Prefabs == null)
            BG_Prefabs = instanTiateObject("Background");
        if (BG_Prefabs != null)
        {
            bgScript = BG_Prefabs.GetComponent<BG>();
            setClickTrigger(BG_Prefabs);
        }
    }

    void setDialogBox()
    {
        DialogBox_Prefabs = GameObject.Find("DialogBox");
        if (DialogBox_Prefabs == null)
            DialogBox_Prefabs = instanTiateObject("DialogBox");
        if (DialogBox_Prefabs != null)
        {
            DialogBoxScript = DialogBox_Prefabs.GetComponent<dialogBox>();
//            setClickTrigger(DialogBox_Prefabs);
        }

    }

    void setCharacters()
    {
        Characters_Prefabs = GameObject.Find("Characters");
        if (Characters_Prefabs == null)
            Characters_Prefabs = instanTiateObject("Characters");
    }

    void setStage()
    {
        Stage_Prefabs = GameObject.Find("Stage");
        if (Stage_Prefabs == null)
            Stage_Prefabs = instanTiateObject("Stage");
        if (Stage_Prefabs != null)
            StageScript = Stage_Prefabs.GetComponent<Stage>();
    }

    void setBaner()
    {
        Baner_Prefabs = GameObject.Find("Baner");
        if (Baner_Prefabs == null)
            Baner_Prefabs = instanTiateObject("Baner");
        if (Baner_Prefabs != null)
            BanerScript = Baner_Prefabs.GetComponent<Baner>();
    }

    void setWeather()
    {
        Weather_Prefabs = GameObject.Find("Weather");
        if (Weather_Prefabs == null)
            Weather_Prefabs = instanTiateObject("Weather");
        if (Weather_Prefabs != null)
            WeatherScript = Weather_Prefabs.GetComponent<Weather>();
    }

    void setSound()
    {
        SoundControl_Preffabs = GameObject.Find("SoundControl");
        if (SoundControl_Preffabs == null)
            SoundControl_Preffabs = instanTiateObject("SoundControl");
        if (SoundControl_Preffabs != null)
            SoundControlScript = SoundControl_Preffabs.GetComponent<SoundControl>();
    }

    void setSelect()
    {
        Select_Preffabs = GameObject.Find("Select");
        if (Select_Preffabs == null)
            Select_Preffabs = instanTiateObject("Select");
        if (Select_Preffabs != null)
            SelectScript = Select_Preffabs.GetComponent<Select>();
    }
    
    void setFade()
    {
        Fade_Preffabs = GameObject.Find("Fade");
        if (Fade_Preffabs == null)
            Fade_Preffabs = instanTiateObject("Fade");
        if (Fade_Preffabs != null)
            FadeScript = Fade_Preffabs.GetComponent<Fade>();
    }

    void setClickTrigger(GameObject here)
    {
        EventTrigger trigger = here.GetComponentInChildren<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => {
            if (datas.getClick())
                clickDown();
            datas.setSkip(false); 
            datas.setAuto(false);
        });
        trigger.triggers.Add(entry);
    }

    void Start()
    {
        if (scenario != null)
        {
            if (datas.getNumberString() > 0)
                Renow();
            else
                executeCommands(scenario[0]);
        }
    }

    GameObject instanTiateObject(string nameObject)
    {
        GameObject here = null;
        here = Resources.Load("VNT/Prefabs/"+nameObject, typeof(GameObject)) as GameObject;
        if (here != null)
        {
            here = Instantiate(here);
            here.name = here.name.Split('(')[0];
        }
        return here;
    }

    void FixedUpdate()
    {
        if (datas.getClick())
        {
            if (datas.getSkip())
                StartCoroutine(Skip());
            else if (datas.getAuto() && datas.getEndString())
                StartCoroutine(Auto());
        }
    }

    void clickDown() {
        datas.setNumberString(datas.getNumberString()+1);
        if (scenario.Count-1 >= datas.getNumberString())
            executeCommands(scenario[datas.getNumberString()]);
    }

    public void setScenario(List<string> list)
    {
        scenario = list;
    }

    public void executeCommands(string here_str)
    {
        if (here_str.Contains("@"))
        {
            string[] tmp = here_str.ToLower().Split(new char[] {'@',' ' });
            switch ("@"+tmp[1])
            {
                case "@bg":
                    if (tmp.Length == 2)
                        bgScript.Visible();
                    else
                    {
                        if (tmp.Length > 2)
                        {
                            if (tmp[2].ToLower().Trim().Equals("hide"))
                                bgScript.Invisible();
                            else
                                bgScript.Visible(tmp[2]);
                        }
                    }
                    break;
                case "@char":
                    if (tmp.Length > 2)
                    {
                        if (tmp[2].Equals(""))
                            Debug.Log("Была вызвана команда char но не найдено аргументов.");
                        else
                        {
                            if (tmp[2].ToLower().Trim().Equals("with"))
                            {
                                if (tmp[4].Contains("."))
                                {
                                    if (datas.getCharacter(tmp[4].Split('.')[0]).Fade())
                                        StageScript.Visible(tmp[3].Trim(), (datas.getCharacter(tmp[4].Split('.')[0])).GetSprite(tmp[4].Split('.')[1]), datas.getCharacter(tmp[4].Split('.')[0]).Time());
                                    else
                                        StageScript.Visible(tmp[3].Trim(), (datas.getCharacter(tmp[4].Split('.')[0])).GetSprite(tmp[4].Split('.')[1]));
                                }
                                else
                                {
                                    if (datas.getCharacter(tmp[4].Split('.')[0]).Fade())
                                        StageScript.Visible(tmp[3].Trim(), (datas.getCharacter(tmp[4])).GetSprite(), datas.getCharacter(tmp[4].Split('.')[0]).Time());
                                    else
                                        StageScript.Visible(tmp[3].Trim(), (datas.getCharacter(tmp[4])).GetSprite());
                                }
                            }
                            else if (tmp[2].ToLower().Trim().Equals("hide"))
                                StageScript.Invisible();
                            else
                            {
                                if (tmp[2].Contains("."))
                                {
                                    if (datas.getCharacter(tmp[2].Split('.')[0]).Fade())
                                        StageScript.Visible((datas.getCharacter(tmp[2].Split('.')[0])).GetSprite(tmp[2].Split('.')[1]), datas.getCharacter(tmp[2].Split('.')[0]).Time());
                                    else
                                        StageScript.Visible((datas.getCharacter(tmp[2].Split('.')[0])).GetSprite(tmp[2].Split('.')[1]));
                                }
                                else
                                {
                                    if (datas.getCharacter(tmp[2]).Fade())
                                        StageScript.Visible((datas.getCharacter(tmp[2])).GetSprite(), datas.getCharacter(tmp[2]).Time());
                                    else
                                        StageScript.Visible((datas.getCharacter(tmp[2])).GetSprite());
                                }
                            }
                        }
                    }
                    else
                        Debug.Log("Была вызвана команда char но не найдено аргументов.");
                    break;
                case "@baner":
                    if (tmp.Length > 2)
                    {
                        datas.setSkip(false);
                        datas.setAuto(false);
                            if (BanerScript != null)
                                BanerScript.Visible(here_str.Substring((tmp[0] + tmp[1]).Length + 1));
                            else
                                Debug.Log("Не найден скрипт на объекте Банер.");
                    }
                    else
                        Debug.Log("Была вызвана команда baner но не найдено аргументов.");
                    break;
                case "@fade":
                    datas.setSkip(false);
                    datas.setAuto(false);
                    if (FadeScript != null)
                        FadeScript.Visisble();
                    else
                        Debug.Log("Команада Fade,не найден скрипт на объекте Fade.");
                    break;
                case "@weather":
                    if (tmp.Length > 2)
                    {
                        if (tmp[2].ToLower().Equals("hide"))
                        {
                            if (tmp[3].ToLower().Equals("rain"))
                                WeatherScript.hideRain();
                            else if (tmp[3].ToLower().Equals("snow"))
                                WeatherScript.hideShow();
                        }
                        else
                        {
                            if (tmp[2].ToLower().Equals("rain"))
                                WeatherScript.showRain();
                            else if (tmp[2].ToLower().Equals("snow"))
                                WeatherScript.showSnow();
                        }
                    }
                    break;

                case "@music":
                    if (tmp.Length > 2)
                    {
                        if (tmp[2].ToLower().Equals("stop"))
                            if (SoundControlScript != null)
                                SoundControlScript.stopBG();
                            else
                                Debug.Log("скрипт SoundControl не найден на объекте SoundControl");
                        else
                        {
                            AudioClip au = Resources.Load("Sound/Music/" + tmp[2], typeof(AudioClip)) as AudioClip;
                            if (au != null)
                                if (SoundControlScript != null)
                                    SoundControlScript.playBG(au);
                                else
                                    Debug.Log("скрипт SoundControl не найден на объекте SoundControl");
                        }
                    }
                    else
                        Debug.Log("Была вызвана команда music но не найдено аргументов.");
                    break;
                case "@select":
                    if (tmp.Length > 2)
                    {
                        datas.setClick(false);
                        datas.setSkip(false);
                        datas.setAuto(false);
                        SelectScript.Setup(here_str.Substring((tmp[0]+tmp[1]).Length+2));
                    }else
                        Debug.Log("Была вызвана команда select но не найдено аргументов.");
                    break;
                case "@goto":
                    if (tmp.Length > 2)
                    {
                        datas.setNumberString(jump.getLabel(tmp[2]));
                    }
                    else
                        Debug.Log("Была вызвана команда goto но не найдено аргументов.");
                    break;
                case "@video":
                    if (tmp.Length > 2)
                    {
                        if (tmp[2].ToLower().Equals("stop"))
                            videoPlayer.Stop();
                        else
                        {
                            videoPlayer.clip = Resources.Load("Video/"+tmp[2].ToLower(),typeof(VideoClip)) as VideoClip;
                            videoPlayer.Play();
                        }
                    }else
                        Debug.Log("Была вызвана команда video но не найдено аргументов.");
                    break;
            }
            if (datas.getClick())
            clickDown();
        }
        else
        {
            if (DialogBoxScript != null) {
                DialogBoxScript.Say(here_str);
            }else
                Debug.Log("Был вызван DialogBox но не найден script dialogBox");
        }
    }

    public void Renow()
    {
        string here_str = null;
        StageScript.Invisible();
        SelectScript.Invisible();
        for (int i = 0; i<=datas.getNumberString();i++) {
            here_str = scenario[i];
            if (here_str.Contains("@"))
            {
                string[] tmp = here_str.ToLower().Split(new char[] { '@', ' ' });
                switch ("@" + tmp[1])
                {
                    case "@bg":
                        if (tmp.Length == 2)
                            bgScript.Visible();
                        else
                        {
                            if (tmp.Length > 2)
                            {
                                if (tmp[2].ToLower().Trim().Equals("hide"))
                                    bgScript.Invisible();
                                else
                                    bgScript.Visible(tmp[2]);
                            }
                        }
                        break;
                    case "@char":
                        if (tmp.Length > 2)
                        {
                            if (tmp[2].Equals(""))
                                Debug.Log("Была вызвана команда char но не найдено аргументов.");
                            else
                            {
                                if (tmp[2].ToLower().Trim().Equals("with"))
                                {
                                    if (tmp[4].Contains("."))
                                    {
                                        if (datas.getCharacter(tmp[4].Split('.')[0]).Fade())
                                            StageScript.Visible(tmp[3].Trim(), (datas.getCharacter(tmp[4].Split('.')[0])).GetSprite(tmp[4].Split('.')[1]), datas.getCharacter(tmp[4].Split('.')[0]).Time());
                                        else
                                            StageScript.Visible(tmp[3].Trim(), (datas.getCharacter(tmp[4].Split('.')[0])).GetSprite(tmp[4].Split('.')[1]));
                                    }
                                    else
                                    {
                                        if (datas.getCharacter(tmp[4].Split('.')[0]).Fade())
                                            StageScript.Visible(tmp[3].Trim(), (datas.getCharacter(tmp[4])).GetSprite(), datas.getCharacter(tmp[4].Split('.')[0]).Time());
                                        else
                                            StageScript.Visible(tmp[3].Trim(), (datas.getCharacter(tmp[4])).GetSprite());
                                    }
                                }
                                else if (tmp[2].ToLower().Trim().Equals("hide"))
                                    StageScript.Invisible();
                                else
                                {
                                    if (tmp[2].Contains("."))
                                    {
                                        if (datas.getCharacter(tmp[2].Split('.')[0]).Fade())
                                            StageScript.Visible((datas.getCharacter(tmp[2].Split('.')[0])).GetSprite(tmp[2].Split('.')[1]), datas.getCharacter(tmp[2].Split('.')[0]).Time());
                                        else
                                            StageScript.Visible((datas.getCharacter(tmp[2].Split('.')[0])).GetSprite(tmp[2].Split('.')[1]));
                                    }
                                    else
                                    {
                                        if (datas.getCharacter(tmp[2]).Fade())
                                            StageScript.Visible((datas.getCharacter(tmp[2])).GetSprite(), datas.getCharacter(tmp[2]).Time());
                                        else
                                            StageScript.Visible((datas.getCharacter(tmp[2])).GetSprite());
                                    }
                                }
                            }
                        }
                        break;
                    case "@weather":
                        if (tmp.Length > 2)
                        {
                            if (tmp[2].ToLower().Equals("hide"))
                            {
                                if (tmp[3].ToLower().Equals("rain"))
                                    WeatherScript.hideRain();
                                else if (tmp[3].ToLower().Equals("snow"))
                                    WeatherScript.hideShow();
                            }
                            else
                            {
                                if (tmp[2].ToLower().Equals("rain"))
                                    WeatherScript.showRain();
                                else if (tmp[2].ToLower().Equals("snow"))
                                    WeatherScript.showSnow();
                            }
                        }
                        break;

                    case "@music":
                        if (tmp.Length > 2)
                        {
                            if (tmp[2].ToLower().Equals("stop"))
                            {
                                if (SoundControlScript != null)
                                    SoundControlScript.stopBG();
                            }
                            else
                            {
                                AudioClip au = Resources.Load("Sound/Music/" + tmp[2], typeof(AudioClip)) as AudioClip;
                                if (au != null)
                                    if (SoundControlScript != null)
                                        SoundControlScript.playBG(au);
                            }
                        }
                        break;
                    case "@video":
                        if (tmp.Length > 2)
                        {
                            if (tmp[2].ToLower().Equals("stop"))
                                videoPlayer.Stop();
                            else
                            {
                                videoPlayer.clip = Resources.Load("Video/" + tmp[2].ToLower(), typeof(VideoClip)) as VideoClip;
                                videoPlayer.Play();
                            }
                        }
                        break;
                }
            }else
                DialogBoxScript.Say(here_str);
        }
    }

    IEnumerator Skip()
    {
        yield return new WaitForSeconds(.5f);
        clickDown();
        StopAllCoroutines();
    }

    IEnumerator Auto()
    {
        yield return new WaitForSeconds(datas.getTimeAuto());
        clickDown();
        StopAllCoroutines();
    }

    
}
