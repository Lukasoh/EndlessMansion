using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;


public class MainSceneManager : MonoBehaviour
{
    public Button continueBtn;
    public TextMeshProUGUI continueTxt;
    public Button newGameBtn;
    public GameObject skipBtn;
    bool hasData;
    

    public GameObject[] optionBtn;
    
    private MainSceneManager instance;

    public Animator mainAnim;

    //Loading Scene
    public GameObject loadingPnl;
    public Slider slider;
    public TextMeshProUGUI loadingTxt;

    MoveScene moveScene;
    MainSceneBool mainSceneBool;
    StageListManager stageListManager;
    
    void Awake()
    {
        stageListManager = FindObjectOfType<StageListManager>();
    }
    void Start()
    {
        moveScene = FindObjectOfType<MoveScene>();
        mainSceneBool = FindObjectOfType<MainSceneBool>();
        if(stageListManager != null )
        {
            stageListManager.LoadStageDataToJson();
        }
        else
        {
            Debug.Log("StageList is null");
        }
        


        //must be updated later
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


    // Update is called once per frame
    void Update()
    {
        HideOption();
        CheckAnim();
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

    public void SkipAnim()
    {
        mainAnim.Play("LogoAnim", 0, 1.0f);
        mainSceneBool.animDone = true;
    }

    public void CheckAnim()
    {
        AnimatorStateInfo stateInfo = mainAnim.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.IsName("LogoAnim") && stateInfo.normalizedTime >= 1)
        {
            mainSceneBool.animDone = true;
        }
    }
    public void HideOption()
    {
        if(!mainSceneBool.animDone)
        {
            for(int i = 0; i < optionBtn.Length; i++)
            {
                optionBtn[i].SetActive(false);
            }
            skipBtn.SetActive(true);
        }
        else
        {
            for (int i = 0; i < optionBtn.Length; i++)
            {
                optionBtn[i].SetActive(true);
            }
            skipBtn.SetActive(false);
            SkipAnim();
        }
    }

    public void ContinueCurrentScene()
    {
        if(stageListManager.stageData.currentStage == "S1_E")
        {
            string sceneNm = "Scene/StageOneEasy";
            LoadScene(sceneNm);
        }
        else if(stageListManager.stageData.currentStage == "S1_N")
        {
            string sceneNm = "Scene/RoomEscape";
            LoadScene(sceneNm);
        }
        else if(stageListManager.stageData.currentStage == "S1_H")
        {
            string sceneNm = "Scene/StageOneDifficult";
            LoadScene(sceneNm);
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
}
