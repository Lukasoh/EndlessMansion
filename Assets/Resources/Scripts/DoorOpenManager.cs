﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class DoorOpenManager : MonoBehaviour
{
    public IsDoorLocked isDoorLocked;
    public Camera activeCam;

    ItemsManagement itemsManagement;
    InventoryManager inventoryManager;
    CameraRotation cameraRotation;
    HintManager hintManager;
    GuidelineManager guidelineManager;
    AnimationManager animationManager;
    public GameObject inventoryPnl;
    

    public GameObject[] doorObj;
    public Sprite[] keySprite;
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
        inventoryManager = FindObjectOfType<InventoryManager>();
        cameraRotation = FindObjectOfType<CameraRotation>();
        hintManager = FindObjectOfType<HintManager>();
        guidelineManager = FindObjectOfType<GuidelineManager>();


    }

    // Update is called once per frame
    void Update()
    {
        if (!inventoryPnl.activeSelf && !cameraRotation.screenDragging)
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
            Ray ray = activeCam.ScreenPointToRay(touch.position);
            RaycastHit hit;
            if(touch.phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out hit))
                {


                    if (hit.collider.gameObject.CompareTag("Doors"))
                    {
                        
                        if (animationManager != null)
                        {
                            if (hit.collider.gameObject.name == "S1Room1")
                            {
                                if (isDoorLocked.s1Room1 == false)
                                {
                                    if (itemsManagement.stageOneItems.Room1Key)
                                    {

                                        if (!isDoorLocked.s1Room1)
                                        {
                                            animationManager.DoorOpenAnim(doorObj[1]);
                                            isDoorLocked.s1Room1 = true;
                                            itemsManagement.stageOneItems.Room1Key = false;
                                            inventoryManager.UseItem(keySprite[0], "S1Room1Key");
                                            hintManager.ObjectHintData("S1Room1");
                                            guidelineManager.ScenarioFour();
                                        }

                                    }
                                    else
                                    {
                                        Debug.Log("Don't Have key");
                                    }
                                }



                            }

                            else if (hit.collider.gameObject.name == "S1Room4")
                            {
                                if (isDoorLocked.s1Room4 == false)
                                {
                                    if (itemsManagement.stageOneItems.Room2Key)
                                    {

                                        if (!isDoorLocked.s1Room4)
                                        {
                                            animationManager.DoorOpenAnim(doorObj[1]);
                                            isDoorLocked.s1Room4 = true;
                                            itemsManagement.stageOneItems.Room2Key = false;
                                            inventoryManager.UseItem(keySprite[0], "S1Room4Key");
                                            hintManager.ObjectHintData("S1Room4");
                                            guidelineManager.ScenarioFour();
                                        }

                                    }
                                    else
                                    {
                                        Debug.Log("Don't Have key");
                                    }
                                }


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

    void UnlockAnim(GameObject doorObj)
    {
        if (doorObj != null)
        {
            animationManager.DoorOpenAnim(doorObj);
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