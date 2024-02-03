using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;


public class MainSceneManager : MonoBehaviour
{
    public Button continueBtn;
    public TextMeshProUGUI continueTxt;
    public Button newGameBtn;
    
    bool hasData;

    public GameObject warningPnl;

    MoveScene moveScene;
    
    void Start()
    {
        moveScene = FindObjectOfType<MoveScene>();
        
        string path = Path.Combine(Application.persistentDataPath, "itemActive.json");
        if(!File.Exists(path))
        {           
            hasData = false;
            
        }
        else
        {            
            hasData = true;
            
        }

    }

    public void CheckExistingGame()
    {
        if(hasData)
        {
            warningPnl.SetActive(true);

        }
        else
        {
            moveScene.ChangeSceneToStageList();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(hasData)
        {
            continueBtn.interactable = true;
            continueTxt.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            continueBtn.interactable = false;
            continueTxt.color = new Color(0.6f, 0.596f, 0.596f, 0.502f);
        }
    }
}
