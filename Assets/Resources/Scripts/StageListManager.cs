using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class StageListManager : MonoBehaviour
{
    
    public Button stageBtn;
    private bool isEnabled;
    

    public Animator[] stageAnim;
    
    

    public GameObject loadingPnl;
    public Slider slider;
    public TextMeshProUGUI loadingTxt;

    public GameObject resetCheckPnl;
    private string resetLvl;

    MoveScene moveScene;
   

    


    void Start()
    {
        
        
        moveScene = FindObjectOfType<MoveScene>();
        
    }

    
    void Update()
    {
        
    }

    public void setStageInfoPnl()
    {
        
        if(!isEnabled)
        {

            stageAnim[0].SetBool("isSelected", true);
            stageAnim[1].SetBool("isSelected", true);
            stageAnim[2].SetBool("isSelected", true);
            isEnabled = true;
        }
        else
        {
            stageAnim[0].SetBool("isSelected", false);
            stageAnim[1].SetBool("isSelected", false);
            stageAnim[2].SetBool("isSelected", false);
            isEnabled = false;                    
        }
    }


    public void StartGameWithDifficultylevel()
    {
        string path = Path.Combine(Application.persistentDataPath, "itemActive.json");
        if (!File.Exists(path))
        {         
            
            LoadScene("Scene/RoomEscape");
        }
        else
        {          
            resetCheckPnl.SetActive(true);
        }
    }

    public void CheckReset()
    {
        string gameDatapath = Path.Combine(Application.persistentDataPath, "itemActive.json");
        string inventoryDatapath = Path.Combine(Application.persistentDataPath, "inventoryData.json");
        string hintDatapath = Path.Combine(Application.persistentDataPath, "hintData.json");
        File.Delete(inventoryDatapath);
        File.Delete(gameDatapath);
        File.Delete(hintDatapath);
        LoadScene("Scene/RoomEscape");
    }


    

    


    // loading Scene function
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsync(sceneName));
    }

    IEnumerator LoadAsync(string sceneName)
    {
        loadingPnl.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            loadingTxt.text = ((int)(progress * 100)).ToString() + "%";

            yield return null;

        }
    }

    public void MoveToGameScene()
    {
        if(resetLvl == "S1_N")
        {
            LoadScene("Scene/RoomEscape");
        }
    }



}



