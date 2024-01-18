using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour
{
    ItemsManagement itemsManagement;

    public Text statusTxt;

    public Button[] itemBtn;
    public GameObject inventoryPnl;
    public bool inventoryOn;
    public Sprite defaultImage;
    public Sprite equipedItem;

    public Image selectedItemImg;
    public Text selectedItemName;
    public Text selectedItemInfo;

    public bool isEquiping;

    public Sprite[] spriteList;

    public Image[] itemSelectedImg;

    // Start is called before the first frame update
    void Start()
    {
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
        
        SetInteractiveBtn();
        SelectedItemManager();
        
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

    public void UseItem(Sprite ItemUsed)
    {
        for (int i = 0; i < itemBtn.Length; i++)
        {
            if (itemBtn[i].GetComponent<Image>().sprite == ItemUsed)
            {
                itemBtn[i].GetComponent<Image>().sprite = defaultImage;
            }
        }

        AlignInventory();
    }

    
}
