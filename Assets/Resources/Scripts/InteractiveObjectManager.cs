using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObjectManager : MonoBehaviour
{
    ItemsManagement itemsManagement;
    InventoryManager inventoryManager;
    AnimationManager animationManager;

    public Camera calendarCam;
    public GameObject calendarObject;
    public Sprite calendarSprite;
    // Start is called before the first frame update
    void Start()
    {
        itemsManagement = FindObjectOfType<ItemsManagement>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        animationManager = FindObjectOfType<AnimationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CleanCalendar();
    }

    void CleanCalendar()
    {
        if(calendarCam.enabled)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Ray ray = calendarCam.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (touch.phase == TouchPhase.Began)
                {

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.CompareTag("Objects"))
                        {
                            GameObject calendarObj = hit.collider.gameObject;
                            if (calendarObj == calendarObject)
                            {

                                if (itemsManagement.stageOneItems.Room1WetDishCloth)
                                {
                                    animationManager.Room1CalendarAnim();
                                    inventoryManager.UseItem(calendarSprite);
                                }
                            }

                        }
                    }


                }
            }
        }
        
    }
}
