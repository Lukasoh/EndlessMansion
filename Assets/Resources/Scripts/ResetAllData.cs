using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ResetAllData : MonoBehaviour
{
    MoveScene moveScene;
    LanguageManager languageManager;
    SetKeyValManager[] setKeyValManager;

    public GameObject resetPnl;
    public GameObject[] resetTxt;
    // Start is called before the first frame update
    void Start()
    {
        moveScene = FindObjectOfType<MoveScene>();
        languageManager = FindObjectOfType<LanguageManager>();
        setKeyValManager = new SetKeyValManager[2];

        for(int i = 0; i <resetTxt.Length; i++)
        {
            setKeyValManager[i] = resetTxt[i].GetComponent<SetKeyValManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckReset()
    {

        string gameDatapath = Path.Combine(Application.persistentDataPath, "itemActive.json");
        string inventoryDatapath = Path.Combine(Application.persistentDataPath, "inventoryData.json");
        string hintDatapath = Path.Combine(Application.persistentDataPath, "hintData.json");
        string soundDatapath = Path.Combine(Application.persistentDataPath, "soundData.json");

        File.Delete(inventoryDatapath);
        File.Delete(gameDatapath);
        File.Delete(hintDatapath);
        File.Delete(soundDatapath);
        moveScene.ChangeSceneToMainScene();
    }

    public void OpenResetPnl()
    {
        resetPnl.SetActive(true);
        languageManager.LoadLanguageDataToJson();
        if(setKeyValManager != null)
        {
            for (int i = 0; i < setKeyValManager.Length; i++)
            {
                setKeyValManager[i].SetKeyValue();
        }
        }
        
    }
}
