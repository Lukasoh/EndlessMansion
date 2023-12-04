using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;


public class ItemsManagement : MonoBehaviour
{
    public Text statusTxt;
    public StageOneItems stageOneItems;
    public Sprite[] spriteList;
    InventoryManager inventoryManager;

    [ContextMenu("To Json Data")]
    void SaveItemsDataToJson()
    {
        string jsonData = JsonUtility.ToJson(stageOneItems, true);
        string path = Path.Combine(Application.persistentDataPath, "stageOneItems.json");
        //string path = "C:\\Users\\오선우\\AppData\\LocalLow\\LKGenius\\Room Escape\\stageOneItems.json";
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")]
    void LoadItemsDataToJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "stageOneItems.json");
        //string path = "C:\\Users\\오선우\\AppData\\LocalLow\\LKGenius\\Room Escape\\stageOneItems.json";
        string jsonData = File.ReadAllText(path);
    }
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
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
            // Camera function stop
        }

        statusTxt.text = stageOneItems.Room1Key.ToString();


    }

    void StageOneRoomOne()
    {
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Items"))
                    {
                        string objectName = hit.collider.gameObject.name;
                        for(int i = 0; i <= spriteList.Length; i++)
                        {
                            if(objectName == spriteList[i].name)
                            {
                                Sprite itemSprite = spriteList[i];
                                hit.collider.gameObject.SetActive(false);
                                AddItemsToBag(itemSprite);
                                SaveItemsDataToJson();
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

        SaveItemsDataToJson();

    }

    public void RemoveEquipedItem()
    {
        stageOneItems.SetAllFalse();
    }

}

[System.Serializable]
public class StageOneItems
{
    public bool Room1WaterBottle;
    public bool Room1DishCloth;
    public bool Room1Key;

    public void SetAllFalse()
    {
        Room1WaterBottle = false;
        Room1DishCloth = false;
        Room1Key = false;
    }
    
}

