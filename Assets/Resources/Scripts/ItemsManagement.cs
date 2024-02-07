using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;


public class ItemsManagement : MonoBehaviour
{
    
    public StageOneItems stageOneItems;
    public Sprite[] spriteList;
    //0.Default 1.WaterBottle 2.DishCloth 3.Room1Key
    
    public Camera activeCam;

    JoystickManager joystickManager;
    InventoryManager inventoryManager;
    JsonManager jsonManager;
    HintManager hintManager;


    [ContextMenu("To Json Data")]
    void SaveItemsDataToJson()
    {
        string jsonData = JsonUtility.ToJson(stageOneItems, true);
        string path = Path.Combine(Application.persistentDataPath, "stageOneItems.json");
        
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")]
    void LoadItemsDataToJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "stageOneItems.json");
        
        string jsonData = File.ReadAllText(path);
    }
    
    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        joystickManager = FindObjectOfType<JoystickManager>();
        jsonManager = FindObjectOfType<JsonManager>();
        hintManager = FindObjectOfType<HintManager>();
        
        LoadItemsDataToJson();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inventoryManager.inventoryOn)
        {
            StageOneRoomOne();
        }
        else
        {
            EquipedItemUI();
        }

        

        
    }

    void StageOneRoomOne()
    {
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = activeCam.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (touch.phase == TouchPhase.Began)
            {
                if (!joystickManager.IsTouchInRect(touch.position, joystickManager.joystick.GetComponent<RectTransform>()))
                {
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.CompareTag("Items"))
                        {
                            string objectName = hit.collider.gameObject.name;
                            for (int i = 0; i < spriteList.Length; i++)
                            {
                                if (objectName == spriteList[i].name)
                                {
                                    Sprite itemSprite = spriteList[i];
                                    hit.collider.gameObject.SetActive(false);
                                    hintManager.ObjectHintData(objectName);
                                    jsonManager.SaveItemStatus(objectName);
                                    AddItemsToBag(itemSprite);
                                    SaveItemsDataToJson();

                                }
                            }

                        }
                    }

                }
                


            }
        }


    }

    void AddItemsToBag(Sprite itemSprite)
    {
        for (int i = 0; i < spriteList.Length; i++)
        {
            if (itemSprite == spriteList[i])
            {
                inventoryManager.ManageInventory(itemSprite);
            }
        }
        



    }

    public void ManageEquipedItem()
    {
        if (inventoryManager.equipedItem == spriteList[1])
        {
            if(stageOneItems.Room1WaterBottle) // If equiping this item already, remove it
            {
                RemoveEquipedItem();
            }
            else
            {
                RemoveEquipedItem(); // if other item is equiped, remove it 
                stageOneItems.Room1WaterBottle = true;
            }

        }
        else if (inventoryManager.equipedItem == spriteList[2])
        {
            if(stageOneItems.Room1DishCloth)
            {
                RemoveEquipedItem();
            }
            else
            {
                RemoveEquipedItem();
                stageOneItems.Room1DishCloth = true;
            }
            
        }
        else if (inventoryManager.equipedItem == spriteList[3])
        {
            if (stageOneItems.Room1WetDishCloth)
            {
                RemoveEquipedItem();
            }
            else
            {
                RemoveEquipedItem();
                stageOneItems.Room1WetDishCloth = true;
            }

        }

        else if (inventoryManager.equipedItem == spriteList[4])
        {
            if (stageOneItems.Room1Key)
            {
                RemoveEquipedItem();
            }
            else
            {
                RemoveEquipedItem();
                stageOneItems.Room1Key = true;
            }
            
        }

        else if (inventoryManager.equipedItem == spriteList[5])
        {
            if (stageOneItems.Room2Stick)
            {
                RemoveEquipedItem();
            }
            else
            {
                RemoveEquipedItem();
                stageOneItems.Room2Stick = true;
            }

        }

        else if (inventoryManager.equipedItem == spriteList[6])
        {
            if (stageOneItems.Room2Mop)
            {
                RemoveEquipedItem();
            }
            else
            {
                RemoveEquipedItem();
                stageOneItems.Room2Mop = true;
            }

        }

        SaveItemsDataToJson();

    }

    public void RemoveEquipedItem()
    {
        stageOneItems.SetAllFalse();
    }

    void EquipedItemUI()
    {
        if(stageOneItems.Room1WaterBottle)
        {
            
        }
        else if(stageOneItems.Room1DishCloth)
        {
            
        }
        else if(stageOneItems.Room1Key)
        {
            
        }
    }
    

}

[System.Serializable]
public class StageOneItems
{
    public bool Room1WaterBottle;
    public bool Room1DishCloth;
    public bool Room1WetDishCloth;
    public bool Room1Key;

    public bool Room2Stick;
    public bool Room2Mop;
    public bool Room2Book;
    public bool Room2Paper;
    public bool Room2Key;

    public bool Room4EmptyBalloon;
    public bool Room4Sensor;
    public bool Room4Soda;
    public bool Room4Helium;

    public bool Room5Photo;
    public bool Room5BoxKey;
    public bool Room5HeartKey;
    public bool Room5Usb;

    public bool Room3SunflowerKey;
    public bool Room3CloverKey;
    public bool Room3EmptyKey;
    public bool Room3Bond;
    public bool Room3BrightBook;
    public bool Room3SmilePaper;
    public bool Room3SmileKey;
    public bool Room3AccountBook;

    public bool Room6Plastick;
    public bool Room6TruthKey;
    public bool Room6FreedomKey;

    public bool TruthPotion;

    public bool MemoryMarble1;
    public bool MemoryMarble2;
    public bool MemoryMarble3;

    public void SetAllFalse()
    {
        Room1WaterBottle = false;
        Room1DishCloth = false;
        Room1WetDishCloth = false;
        Room1Key = false;

        Room2Stick = false;
        Room2Mop = false;
        Room2Book = false;
        Room2Paper = false;
        Room2Key = false;

        Room4EmptyBalloon = false;
        Room4Sensor = false;
        Room4Soda = false;
        Room4Helium = false;

        Room5Photo = false;
        Room5BoxKey = false;
        Room5HeartKey = false;
        Room5Usb = false;

        Room3SunflowerKey = false;
        Room3CloverKey = false;
        Room3EmptyKey = false;
        Room3Bond = false;
        Room3BrightBook = false;
        Room3SmilePaper = false;
        Room3SmileKey = false;
        Room3AccountBook = false;

        Room6Plastick = false;
        Room6TruthKey = false;
        Room6FreedomKey = false;

        TruthPotion = false;

        MemoryMarble1 = false;
        MemoryMarble2 = false;
        MemoryMarble3 = false;

    }
    
}

