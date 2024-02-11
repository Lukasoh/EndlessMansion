using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class StageListManager : MonoBehaviour
{
    public StageData stageData;

    public Button stageBtn;
    private bool isEnabled;
    bool[] mydifficulty;

    public Animator[] stageAnim;
    public Button startBtn;
    public TextMeshProUGUI[] difficultyTxt;

    public GameObject loadingPnl;
    public Slider slider;
    public TextMeshProUGUI loadingTxt;

    public GameObject resetCheckPnl;
    private string resetLvl;

    MoveScene moveScene;
   

    public void SaveStageDataToJson()
    {

        string jsonData = JsonUtility.ToJson(stageData, true);
        string path = Path.Combine(Application.persistentDataPath, "stageData.json");

        File.WriteAllText(path, jsonData);
    }

    public void LoadStageDataToJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "stageData.json");
        if (!File.Exists(path))
        {
            StageData stageData = new StageData();
            stageData.SetDefault();
            string newStageData = JsonUtility.ToJson(stageData, true);
            File.WriteAllText(path, newStageData);
        }

        string jsonData = File.ReadAllText(path);
        stageData = JsonUtility.FromJson<StageData>(jsonData);
    }


    void Start()
    {
        LoadStageDataToJson();
        mydifficulty = new bool[3];
        moveScene = FindObjectOfType<MoveScene>();
        
    }

    
    void Update()
    {
        DifficultyTxtColorSet();
        PlayBtnSet();

        

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

    public void EasyLvlOnClick()
    {
        for (int i = 0; i < mydifficulty.Length; i++)
        {
            if(i == 0)
            {
                mydifficulty[i] = true;
            }
            else
            {
                mydifficulty[i] = false;
            }
        }
    }

    public void NormalLvlOnClick()
    {
        for (int i = 0; i < mydifficulty.Length; i++)
        {
            if (i == 1)
            {
                mydifficulty[i] = true;
            }
            else
            {
                mydifficulty[i] = false;
            }
        }
    }

    public void HardLvlOnClick()
    {
        for (int i = 0; i < mydifficulty.Length; i++)
        {
            if (i == 2)
            {
                mydifficulty[i] = true;
            }
            else
            {
                mydifficulty[i] = false;
            }
        }
    }

    public void StartGameWithDifficultylevel()
    {
        if (mydifficulty[0])
        {
            stageData.currentStage = "S1_E";
            SaveStageDataToJson();
        }
        else if (mydifficulty[1])
        {
            string path = Path.Combine(Application.persistentDataPath, "itemActive.json");
            if (!File.Exists(path))
            {
                stageData.currentStage = "S1_N";
                SaveStageDataToJson();
                LoadScene("Scene/RoomEscape");
            }
            else
            {
                resetLvl = "N";
                resetCheckPnl.SetActive(true);
                
            }
            
        }
        else if (mydifficulty[2])
        {
            stageData.currentStage = "S1_H";
            SaveStageDataToJson();
        }
        else
        {
            Debug.Log("Difficulty level not chosen yet");
        }
    }

    public void CheckReset()
    {
        
        if (resetLvl == "N")
        {
            string gameDatapath = Path.Combine(Application.persistentDataPath, "itemActive.json");
            string inventoryDatapath = Path.Combine(Application.persistentDataPath, "inventoryData.json");
            string hintDatapath = Path.Combine(Application.persistentDataPath, "hintData.json");
            File.Delete(inventoryDatapath);
            File.Delete(gameDatapath);
            File.Delete(hintDatapath);
            LoadScene("Scene/RoomEscape");
        }
    }

    public void DifficultyTxtColorSet()
    {
        if(isEnabled)
        {
            if (mydifficulty[0])
            {
                difficultyTxt[0].color = new Color(148/255f, 1f, 131/255f);
                difficultyTxt[1].color = new Color(1f, 1f, 1f);
                difficultyTxt[2].color = new Color(1f, 1f, 1f);

            }

            else if (mydifficulty[1])
            {
                difficultyTxt[0].color = new Color(1f, 1f, 1f);
                difficultyTxt[1].color = new Color(148 / 255f, 1f, 131 / 255f);
                difficultyTxt[2].color = new Color(1f, 1f, 1f);
            }

            else if (mydifficulty[2])
            {
                difficultyTxt[0].color = new Color(1f, 1f, 1f);
                difficultyTxt[1].color = new Color(1f, 1f, 1f);
                difficultyTxt[2].color = new Color(148 / 255f, 1f, 131 / 255f);
            }
            else
            {
                for (int i = 0; i < difficultyTxt.Length; i++)
                {
                    difficultyTxt[i].color = new Color(1f, 1f, 1f);
                }
            }
        }
        else
        {
            for (int i = 0; i < mydifficulty.Length; i++)
            {
                mydifficulty[i] = false;
            }
        }
        
    }

    public void PlayBtnSet()
    {
        if (!mydifficulty[0] && !mydifficulty[1] && !mydifficulty[2])
        {
            startBtn.enabled = false;
            startBtn.GetComponent<Image>().color = new Color(176/255f, 176 / 255f, 176 / 255f, 0.8f);
        }

        else
        {
            startBtn.enabled = true;
            startBtn.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
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
        if(resetLvl == "N")
        {
            LoadScene("Scene/RoomEscape");
        }
    }



}

[System.Serializable]
public class StageData
{
    public string currentStage;

    public void SetDefault()
    {
        currentStage = new string("");
    }
}

