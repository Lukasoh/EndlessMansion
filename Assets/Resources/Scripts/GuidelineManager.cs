using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuidelineManager : MonoBehaviour
{
    private float lastTouchTime;

    public AudioClip[] audioClip;

    public GameObject subtitleObj;
    public GameObject second_SubtitleObj;

    public TextMeshProUGUI guideTxt;
    public TextMeshProUGUI second_GuideTxt;
    public GameObject inventoryPnl;

    public string[] keyName;

    public GameObject[] guideTxtBg;
    private AudioSource audioSource;

    SetKeyValManager setKeyValManager;
    SetKeyValManager second_SetKeyValManager;

    Vector2 originPos;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        setKeyValManager = guideTxt.GetComponent<SetKeyValManager>();
        second_SetKeyValManager = second_GuideTxt.GetComponent<SetKeyValManager>();

        originPos = guideTxt.rectTransform.anchoredPosition;
    }

    void Update()
    {

        if (Input.touchCount > 0)
        {
            lastTouchTime = Time.time; 
        }

        if(inventoryPnl.activeSelf)
        {          
            guideTxt.color = new Color(0, 0, 0);
        }
        else
        {
            guideTxt.rectTransform.anchoredPosition = originPos;
            guideTxt.color = new Color(255, 255, 255);
        }

        if(subtitleObj.activeSelf)
        {
            if(!inventoryPnl.activeSelf)
            {
                guideTxtBg[0].SetActive(true);
            }
            

        }
        else
        {
            guideTxtBg[0].SetActive(false);
        }

        if(second_SubtitleObj.activeSelf)
        {
            guideTxtBg[1].SetActive(true);
        }
        else
        {
            guideTxtBg[1].SetActive(false);
        }
    }

    public void ScenarioOne()
    {
        int randNum = Random.Range(0, 3);
        audioSource.clip = audioClip[randNum];
        audioSource.Play();
        ObjCamSetSubtitle(randNum);
    }

    public void ScenarioTwo()
    {
        int randNum = Random.Range(3, 5);
        audioSource.clip = audioClip[randNum];
        audioSource.Play();
    }

    public void ScenarioThree()
    {
        int randNum = Random.Range(6, 9);
        audioSource.clip = audioClip[randNum];
        audioSource.Play();
        MainCamSetSubtitle(randNum);
    }

    public void ScenarioFour()
    {
        int randNum = Random.Range(9, 12);
        audioSource.clip = audioClip[randNum];
        audioSource.Play();
        MainCamSetSubtitle(randNum);
    }

    public void ScenarioFive()
    {
        int randNum = Random.Range(13, 15);
        audioSource.clip = audioClip[randNum];
        audioSource.Play();
    }

    public void ObjCamSetSubtitle(int randNum)
    {
        if(randNum == 0)
        {
            second_SetKeyValManager.objKey = keyName[0];
        }

        else if (randNum == 1)
        {
            second_SetKeyValManager.objKey = keyName[1];
        }

        else if (randNum == 2)
        {
            second_SetKeyValManager.objKey = keyName[2];
        }

        second_SetKeyValManager.SetKeyValue();
        second_SubtitleObj.SetActive(true);
        if(Time.time - lastTouchTime > 1.2f)
        {
            StartCoroutine(DisableObjSubtitle());
        }
        
        Debug.Log(second_SetKeyValManager.objKey);
    }

    public void MainCamSetSubtitle(int randNum)
    {
        if (randNum == 6)
        {
            setKeyValManager.objKey = keyName[6];
        }

        else if (randNum == 7)
        {
            setKeyValManager.objKey = keyName[7];
        }

        else if (randNum == 8)
        {
            setKeyValManager.objKey = keyName[8];
        }

        else if (randNum == 9)
        {
            setKeyValManager.objKey = keyName[9];
        }

        else if (randNum == 10)
        {
            setKeyValManager.objKey = keyName[10];
        }
        else if (randNum == 11)
        {
            setKeyValManager.objKey = keyName[11];
        }

        setKeyValManager.SetKeyValue();
        subtitleObj.SetActive(true);
        StartCoroutine(DisableMainSubtitle());
    }

    public void QuitObjSubtitle()
    {
        second_SubtitleObj.SetActive(false);
    }

    IEnumerator DisableObjSubtitle()
    {
        yield return new WaitForSeconds(1.5f);
        second_SubtitleObj.SetActive(false);

    }

    IEnumerator DisableMainSubtitle()
    {
        yield return new WaitForSeconds(2f);
        subtitleObj.SetActive(false);

    }
}
