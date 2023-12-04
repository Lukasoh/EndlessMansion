using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class DoorOpenManager : MonoBehaviour
{
    public IsDoorLocked isDoorLocked;
    
    ItemsManagement itemsManagement;
    AnimationManager animationManager;
    public GameObject inventoryPnl;
    bool[] doorUnlocked = new bool[7];

    public GameObject[] doorObj;
    public Text textStatus;

    void CreateJsonFile(string path)
    {        
        IsDoorLocked isDoorLocked = new IsDoorLocked();         
        string jsonData = JsonUtility.ToJson(isDoorLocked, true);        
        File.WriteAllText(path, jsonData);      
    }

    [ContextMenu("To Json Data")]
    void SaveDoorDataToJson()
    {
        string jsonData = JsonUtility.ToJson(isDoorLocked, true);
        string path = Path.Combine(Application.persistentDataPath, "isDoorLocked.json");
        File.WriteAllText(path, jsonData);

    }

    [ContextMenu("From Json Data")]
    void LoadDoorDataToJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "isDoorLocked.json");
        string jsonData = File.ReadAllText(path);
        isDoorLocked = JsonUtility.FromJson<IsDoorLocked>(jsonData);
    }

    void Awake()
    {
        animationManager = FindObjectOfType<AnimationManager>();
        
    }
    void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, "isDoorLocked.json");
        if (!File.Exists(path))
        {
            CreateJsonFile(path);
        }
        LoadDoorDataToJson();
        itemsManagement = FindObjectOfType<ItemsManagement>();
        


    }

    // Update is called once per frame
    void Update()
    {
        if(!inventoryPnl.activeSelf)
        {
            UnlockDoor();
        }
        else
        {

        }
        
    }

    void UnlockDoor()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            
            if (touch.phase == TouchPhase.Began)
            {
                
                if (Physics.Raycast(ray, out hit))
                {
                    

                    if (hit.collider.gameObject.CompareTag("Doors"))
                    {
                        if(animationManager != null)
                        {
                            if (hit.collider.gameObject.name == "S1Room1")
                            {
                               animationManager.DoorOpenAnim(doorObj[0]);                               
                            }
                        }

                        else 
                        {
                            Debug.Log("Animation is Null");
                        }
                       
                        
                        
                    }
                    

                    
                }
                
            }
            


        }
    }

    bool CheckItems(GameObject interactObj)
    {
        if (interactObj == doorObj[0])
        {
            if (itemsManagement != null && itemsManagement.stageOneItems != null)
            {
                bool hasKey = itemsManagement.stageOneItems.Room1Key;

                if (hasKey)
                {
                    return true;
                }
                else
                {
                    
                }
            }
            else
            {
                
            }
        }
        else if (interactObj == null)
        {
            
        }

        return false;
    }

}

[System.Serializable]
public class IsDoorLocked
{
    public bool s1Room1;
    public bool s1Room2;
    public bool s1Room3;
    public bool s1Room4;
    public bool s1Room5;
    public bool s1Room6;
    public bool truthDoor;
    public bool freedomDoor;


}