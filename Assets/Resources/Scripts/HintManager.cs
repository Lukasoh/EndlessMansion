using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
public class HintManager : MonoBehaviour
{
    public HintData hintData;

    public TextMeshProUGUI progressTxt;
    public Slider slider;

    private float statusNum;
    private int statusIntNum;
    [ContextMenu("To Json Data")]
    public void SaveProgressToJson()
    {
        string jsonData = JsonUtility.ToJson(hintData, true);
        string path = Path.Combine(Application.persistentDataPath, "hintData.json");

        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")]
    public void LoadProgressToJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "hintData.json");
        if (!File.Exists(path))
        {
            HintData hintData = new HintData();
            hintData.DefaultHintData();
            string newHintData = JsonUtility.ToJson(hintData, true);
            File.WriteAllText(path, newHintData);
        }
        
        string jsonData = File.ReadAllText(path);
        hintData = JsonUtility.FromJson<HintData>(jsonData);
        
    }

    void Start()
    {
        LoadProgressToJson();
        slider.minValue = 0f;
        slider.maxValue = 100f;
        slider.interactable = false;
    }

    void Update()
    {
        progressTxt.text = statusIntNum.ToString() + "%";
    }

    public void ObjectHintData(string objName)
    {
        if (objName == "Room1WaterBottle")
        {
            hintData.progressData[0] = true;
        }
        else if (objName == "Room1Dishcloth")
        {
            hintData.progressData[1] = true;
        }
        else if (objName == "Room1WetDishcloth")
        {
            hintData.progressData[2] = true;
        }
        else if (objName == "Room1Storage")
        {
            hintData.progressData[3] = true;
        }
        else if (objName == "Room1Key")
        {
            hintData.progressData[4] = true;
        }
        else if (objName == "S1Room1")
        {
            hintData.progressData[5] = true;
        }
        else
        {
            
        }
        hintData.SetNum();
        SaveProgressToJson();
    }

    public void GetCalculatedVal()
    {
        LoadProgressToJson();
        statusNum = ((float)hintData.currentProgress / hintData.progressData.Length) * 100;
        statusIntNum = (int)statusNum;
        slider.value = statusIntNum;

    }
}

[System.Serializable]
public class HintData
{
    public bool[] isHintOpened;
    public bool[] progressData;

    /*
    Room1
    0. Obtain water bottle
    1. Obtain dishcloth
    2. Combine to make wet discloth
    
    3. Open storage cabinet
    4. Obtain Room1 key
    5. Open S1Room1
    
    Room2
    7. Obtain rod
    8. Combine to make mop
    9. Wipe stain
    10. Solve bookshelf code
    11. Obtain book
    12. Operate scanner
    13. Obtain paper
    14. Open safe
    15. Obtain S1Room4 key + memory orb
    16. Open S1Room4 door
    
    Room4
    17. Obtain windless balloon
    18. Obtain sensor
    19. Obtain soda
    20. Drink or discard soda
    21. Open drawer
    22. Obtain helium gas
    23. Combine to make helium balloon
    24. Combine to make helium balloon with sensor
    25. Operate ceiling device
    
    Room5
    26. Obtain wall photo
    27. Obtain key
    28. Open circuit box
    29. Solve minigame
    30. Obtain Heart Key
    31. Obtain USB, memory orb
    32. Open S1Room3
    33. Obtain Sunflower Key
    34. Obtain Four-leaf Clover Key
    35. Obtain Empty Key
    36. Obtain glue
    37. Obtain glowing book
    38. Obtain smiling face paper
    39. Combine to make Smiling Face Key
    40. Place keys
    41. Obtain memory orb
    42. Obtain plastic mixture
     */
    public int currentProgress;
    
    public void DefaultHintData()
    {
        progressData = new bool[43];
        isHintOpened = new bool[43];
        for (int i = 0; i < progressData.Length; i++)
        {
            progressData[i] = false;
            isHintOpened[i] = false;
        }
    }

    

    public void SetNum()
    {
        currentProgress = 0;

        for (int i = 0; i < progressData.Length; i++)
        {
            if (progressData[i])
            {
                currentProgress++;
            }
        }
    }

}

