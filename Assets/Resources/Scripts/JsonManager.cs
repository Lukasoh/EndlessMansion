using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class JsonManager : MonoBehaviour
{
    public ItemActive itemActive;
    public GameObject[] itemUsed;
    
    // Start is called before the first frame update

    void CreateJsonFile(string path)
    {
        ItemActive itemActive = new ItemActive();
        string jsonData = JsonUtility.ToJson(itemActive, true);
        File.WriteAllText(path, jsonData);
    }

    void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, "itemActive.json");
        if (!File.Exists(path))
        {
            CreateJsonFile(path);
        }

        //itemActive.items = new bool[4];
        //itemActive.characterPos = new Vector3();
        //itemActive.objName = new string[4];
        //itemActive.animPar = new string[4];
        LoadItemStatusToJson();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("To Json Data")]
    public void SaveItemStatusToJson()
    {
        string jsonData = JsonUtility.ToJson(itemActive, true);
        string path = Path.Combine(Application.persistentDataPath, "itemActive.json");

        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")]
    public void LoadItemStatusToJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "itemActive.json");
        string jsonData = File.ReadAllText(path);
        itemActive = JsonUtility.FromJson<ItemActive>(jsonData);

        SetDefaultVal();
        LoadCharacterPosition();
        LoadItemStatus();
        LoadAnimStatus();
    }

    

    
    // Player Location Save and Load Functions
    public void SaveCharacterPosition(Vector3 charPos)
    {
        itemActive.characterPos = charPos;
        SaveItemStatusToJson();
    }

    public void LoadCharacterPosition()
    {
        if (itemActive != null)
        {
            GameObject playerObj = GameObject.Find("Player");
            Vector3 savedPos = itemActive.characterPos;
            playerObj.transform.position = savedPos;
        }
    }
    
    // Item Set Active Save and Load Functions
    public void SaveItemStatus(string itemName)
    {
        if (itemActive != null)
        {
            for (int i = 0; i < itemUsed.Length; i++)
            {
                if (itemUsed[i].name == itemName)
                {
                    itemActive.items[i] = false;
                    break;
                }
            }
        }
        SaveItemStatusToJson();
    }
    public void LoadItemStatus()
    {
        for (int i = 0; i < itemActive.items.Length; i++)
        {
            if (!itemActive.items[i])
            {
                itemUsed[i].SetActive(false);
            }
        }
    }
    
    // Animation Status Save and Load
    public void SaveAnimStatus(string objName, string stateName, string parameter)
    {
        for (int i = 0; i < itemActive.objName.Length; i++)
        {
            if (itemActive.objName[i] == "")
            {
                itemActive.objName[i] = objName;
                itemActive.animStatus[i] = stateName;
                itemActive.animPar[i] = parameter;
                break;
            }
        }
        SaveItemStatusToJson();
    }

    public void LoadAnimStatus()
    {
        for(int i = 0; i < itemActive.objName.Length; i++)
        {
            if (itemActive.objName[i] != "")
            {
                GameObject savedObj = GameObject.Find(itemActive.objName[i]);
                Animator savedAnim = savedObj.GetComponent<Animator>();
                savedAnim.SetBool(itemActive.animPar[i], true);
                savedAnim.Play(itemActive.animStatus[i], 0, 1.0f);
                savedAnim.Update(0f);
            }
        }
        
    }

    // Reset Json Data
    void SetDefaultVal()
    {
        if(itemActive != null) 
        {
            
            if (itemActive.items.Length == 0)
            {
                itemActive.items = new bool[4];
                for (int i = 0; i < itemActive.items.Length; i++)
                {
                    itemActive.items[i] = true;
                }
            }

            if (itemActive.characterPos.x == 0 && itemActive.characterPos.y == 0 && itemActive.characterPos.z == 0)
            {
                itemActive.characterPos = new Vector3();
                SetDefaultCharacterPos();
            }

            if (itemActive.objName.Length == 0)
            {
                itemActive.objName = new string[4];
                for (int i = 0; i < itemActive.objName.Length; i++)
                {
                    itemActive.objName[i] = "";
                }
            }

            if (itemActive.animStatus.Length == 0)
            {
                itemActive.animStatus = new string[4];
                for (int i = 0; i < itemActive.animStatus.Length; i++)
                {
                    itemActive.animStatus[i] = "";
                }
            }

            if (itemActive.animPar.Length == 0)
            {
                itemActive.animPar = new string[4];
                for (int i = 0; i < itemActive.animPar.Length; i++)
                {
                    itemActive.animPar[i] = "";
                }
            }
        }
        SaveItemStatusToJson();


    }
    
    

    private void SetDefaultCharacterPos()
    {
        itemActive.characterPos = new Vector3(-550, -540, -750);
    }



    

}

[System.Serializable]
public class ItemActive
{
    public bool[] items;
    public Vector3 characterPos;
    public string[] objName;
    public string[] animStatus;
    public string[] animPar;

}

