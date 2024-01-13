using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemCombineManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    
    InventoryManager inventoryManager;
    ItemsManagement itemsManagement;

    
    public RectTransform draggingItem;
    public RectTransform[] targetItem;
    public RectTransform selectedRt;

    public bool itemDragging;
    public bool isCombined;
    private Vector2 originPos;
    private Vector2 originLinePos;

    

    void Start()
    {        
        inventoryManager = FindObjectOfType<InventoryManager>();
        itemsManagement = FindObjectOfType<ItemsManagement>();
    }
        
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemDragging = true;
        originPos = draggingItem.anchoredPosition;
        originLinePos = selectedRt.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(itemDragging && draggingItem != null)
        {
            
            draggingItem.anchoredPosition += eventData.delta;
            selectedRt.position = originLinePos;

        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isCombined)
        {
            draggingItem.anchoredPosition = originPos;
            selectedRt.position = originLinePos;
            itemDragging = false;

            for(int i = 0; i < targetItem.Length; i++)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(targetItem[i], eventData.position, null))
                {
                    
                    RectTransform compareRt = targetItem[i];
                    CheckCombine(compareRt);
                }

            }
            
        }

        
    }

    public void CheckCombine(RectTransform compareRt)
    {
        
        GameObject currentObj = draggingItem.gameObject;
        GameObject chosenObj = compareRt.gameObject;

        Image currentObjImg = currentObj.GetComponent<Image>();
        Image chosenObjImg = chosenObj.GetComponent<Image>();
        
        if (currentObjImg.sprite != null)
        {
            
            bool wetDishClothOne = currentObjImg.sprite == itemsManagement.spriteList[1] && chosenObjImg.sprite == itemsManagement.spriteList[2];
            bool wetDishClothTwo = currentObjImg.sprite == itemsManagement.spriteList[2] && chosenObjImg.sprite == itemsManagement.spriteList[1];
            
            if (wetDishClothOne || wetDishClothTwo)
            {
                
                itemsManagement.stageOneItems.Room1WaterBottle = false;
                itemsManagement.stageOneItems.Room1DishCloth = false;
                currentObj.GetComponent<Image>().sprite = itemsManagement.spriteList[0];
                chosenObj.GetComponent<Image>().sprite = itemsManagement.spriteList[0];

                inventoryManager.ManageInventory(itemsManagement.spriteList[3]);
                //inventoryManager.SetAllOutlineFalse();
                
            }
            
            
        }
    }

    
}
