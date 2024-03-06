using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class InventoryManager : MonoBehaviour
{
    ItemsManagement itemsManagement;
    InventoryData inventoryData;

    

    public Button[] itemBtn;
    public GameObject inventoryPnl;
    public bool inventoryOn;
    public Sprite defaultImage;
    public Sprite equipedItem;

    public Image selectedItemImg;
    
    public Text selectedItemName;
    public Text selectedItemInfo;

    // Manage Navigation bar
    public GameObject navBtn;

    // Show user which item is equiped
    public Image equipedItemStatusImg;
    public Image equipedItemStatusBorder;
    public Sprite equipedBorderSprite;

    public bool isEquiping;

    public Sprite[] spriteList;

    public Image[] itemSelectedImg;

    string spriteName;

    [ContextMenu("To Json Data")]
    void SaveInventoryDataToJson()
    {
        string jsonData = JsonUtility.ToJson(inventoryData, true);
        string path = Path.Combine(Application.persistentDataPath, "inventoryData.json");
        
        File.WriteAllText(path, jsonData);
        Debug.Log("Saved Json");
    }

    [ContextMenu("From Json Data")]
    void LoadInventoryDataToJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "inventoryData.json");
        
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            inventoryData = JsonUtility.FromJson<InventoryData>(jsonData);

            for (int i = 0; i < inventoryData.spName.Length; i++)
            {
                spriteName = inventoryData.spName[i];
                for (int j = 0; j < spriteList.Length; j++)
                {
                    if (spriteName == spriteList[j].name)
                    {
                        itemBtn[i].GetComponent<Image>().sprite = spriteList[j];
                        Debug.Log(spriteList[j].name);
                    }
                }

            }
        }
        else
        {
            InventoryData inventoryData = new InventoryData();
            
            string newInventoryData = JsonUtility.ToJson(inventoryData, true);
            File.WriteAllText(path, newInventoryData);
        }
        

        

        Debug.Log(inventoryData.spName[1]);
    }
    

    void Start()
    {
        inventoryData = new InventoryData();
        LoadInventoryDataToJson();
        for (int i = 0; i < itemSelectedImg.Length; i++)
        {
            Color color = itemSelectedImg[i].color;                      
            color.a = 0f;
            itemSelectedImg[i].color = color;         
        }

        equipedItem = null;
        inventoryOn = false;


        itemsManagement = FindObjectOfType<ItemsManagement>();
        ItemSelection();

    }

    // Update is called once per frame
    void Update()
    {
        inventoryOn = inventoryPnl.activeSelf;

        ManageEquipedItemStatusImg();
        SetInteractiveBtn();
        SelectedItemManager();
        ManageNavBar();
    }

    void ManageEquipedItemStatusImg()
    {
        if(!inventoryOn)
        {
            equipedItemStatusImg.enabled = true;
            equipedItemStatusBorder.enabled = true;
            if (equipedItem == null)
            {
                equipedItemStatusImg.sprite = defaultImage;
                equipedItemStatusBorder.sprite = defaultImage;
            }
            else
            {
                equipedItemStatusImg.sprite = equipedItem;
                equipedItemStatusBorder.sprite = equipedBorderSprite;
            }
        }
        else
        {
            equipedItemStatusImg.enabled = false;
            equipedItemStatusBorder.enabled = false;
        }
        
    }

    void ManageNavBar()
    {
        if(!inventoryOn)
        {
            navBtn.SetActive(true);
        }
        else
        {
            navBtn.SetActive(false);
        }
    }
    public void ManageInventory(Sprite itemSprite)
    {

        Sprite currentItem = itemSprite;

        for (int i = 0; i < itemBtn.Length; i++)
        {
            if (itemBtn[i].GetComponent<Image>().sprite == defaultImage)
            {             
                itemBtn[i].GetComponent<Image>().sprite = currentItem;
                break;
            }
        }
        AlignInventory();

        for(int i = 0; i < itemBtn.Length; i++)
        {
            inventoryData.spName[i] = itemBtn[i].GetComponent<Image>().sprite.name;
        }
        SaveInventoryDataToJson();
    }

    void ItemSelection()
    {
        for (int i = 0; i < itemBtn.Length; i++)
        {
            int btnIndex = i;

            itemBtn[i].onClick.AddListener(() => OnButtonClick(btnIndex));
           
        }        
    }

    

    public void SetAllOutlineFalse()
    {
        
        for (int i = 0; i < itemSelectedImg.Length; i++)
        {
            Color color = itemSelectedImg[i].color;
            if (color.a == 255f)
            {
                color.a = 0f;
                itemSelectedImg[i].color = color;
                equipedItem = null;
            }
        }
        
    }

    void OnButtonClick(int btnIndex)
    {
        Sprite selectedSprite = itemBtn[btnIndex].GetComponent<Image>().sprite;
               
        if(selectedSprite != defaultImage)
        {
            equipedItem = selectedSprite;
            itemsManagement.ManageEquipedItem();
            for(int i = 0; i < itemSelectedImg.Length; i++)
            {
                Color color = itemSelectedImg[i].color;
                if(i == btnIndex)
                {
                    if(color.a == 255f)
                    {
                        color.a = 0f;
                        itemSelectedImg[i].color = color;
                        equipedItem = null;
                    }
                    else
                    {
                        color.a = 255f;
                        itemSelectedImg[i].color = color;
                    }
                    
                }
                else
                {
                    color.a = 0f;
                    itemSelectedImg[i].color = color;
                }
            }
        }
    }

    // Set Buttons unable being touched
    void SetInteractiveBtn()
    {
        for (int i = 0; i < itemBtn.Length; i++)
        {
            if (itemBtn[i].GetComponent<Image>().sprite == defaultImage)
            {
                itemBtn[i].interactable = false;
            }
            else
            {
                itemBtn[i].interactable = true;
            }

        }
    }

    void SelectedItemManager()
    {
        if(itemsManagement.stageOneItems != null)
        {
            Color color = selectedItemImg.color;

            if (equipedItem == null)
            {              
                color.a = 0f;
                selectedItemImg.color = color;
                
                selectedItemName.text = "";
                selectedItemInfo.text = "";
            }

            else
            {
                selectedItemImg.sprite = equipedItem;
                color.a = 255f;
                selectedItemImg.color = color;

                if (itemsManagement.stageOneItems.Room1WaterBottle)
                {                                       
                    selectedItemName.text = "Water Bottle";
                    selectedItemInfo.text = "There is a little water left in the water bottle. There's got to be a way to use this...";
                }

                else if (itemsManagement.stageOneItems.Room1DishCloth)
                {
                    selectedItemName.text = "Dish-Cloth";
                    selectedItemInfo.text = "An ordinary dishcloth";
                }

                else if (itemsManagement.stageOneItems.Room1Key)
                {
                    selectedItemName.text = "Key";
                    selectedItemInfo.text = "A key that can open a lock somewhere";
                }

                else if (itemsManagement.stageOneItems.Room1WetDishCloth)
                {
                    selectedItemName.text = "Wet Dish-Cloth";
                    selectedItemInfo.text = "The dishcloth is wet from the water";
                }
                else if (itemsManagement.stageOneItems.Room2Stick)
                {
                    selectedItemName.text = "Mop Handle";
                    selectedItemInfo.text = "It's a handle for a mop, but it can't be used because the mop head is not attached";
                }
                else if (itemsManagement.stageOneItems.Room2Mop)
                {
                    selectedItemName.text = "Mop";
                    selectedItemInfo.text = "A mop with a Dish-Cloth and handle combined";
                }
            }
            
        }
    }

    void AlignInventory()
    {
        for (int i = 0; i < itemBtn.Length - 1; i++)
        {
            if (itemBtn[i].GetComponent<Image>().sprite == defaultImage)
            {
                if(itemBtn[i + 1].GetComponent<Image>().sprite != defaultImage)
                {
                    itemBtn[i].GetComponent<Image>().sprite = itemBtn[i + 1].GetComponent<Image>().sprite;
                    itemBtn[i + 1].GetComponent<Image>().sprite = defaultImage;
                }
            }
           
        }
        SetAllOutlineFalse();
    }

    public void UseItem(Sprite itemUsed, string itemName)
    {
        for (int i = 0; i < itemBtn.Length; i++)
        {
            if (itemBtn[i].GetComponent<Image>().sprite == itemUsed)
            {
                itemBtn[i].GetComponent<Image>().sprite = defaultImage;
                inventoryData.spName[i] = "DefaultImage";
            }
        }
        AlignInventory();
        SaveInventoryDataToJson();

    }

    Sprite LoadSprite(string spriteName)
    {
        return Resources.Load<Sprite>(spriteName);
    }

}

[System.Serializable]
public class InventoryData
{
    public string[] spName = new string[12];  
}

