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

    public bool isEquiping;
    
    public Sprite[] spriteList;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    void ItemSelection()
    {
        for (int i = 0; i < itemBtn.Length; i++)
        {
            int btnIndex = i;

            itemBtn[i].onClick.AddListener(() => OnButtonClick(btnIndex));

            
        }        
    }

   

    void OnButtonClick(int btnIndex)
    {
        Sprite selectedSprite = itemBtn[btnIndex].GetComponent<Image>().sprite;
               
        if(selectedSprite != defaultImage)
        {
            equipedItem = selectedSprite;
            itemsManagement.ManageEquipedItem();
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

    
}

