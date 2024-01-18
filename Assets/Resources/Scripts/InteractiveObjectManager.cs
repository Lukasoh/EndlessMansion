using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObjectManager : MonoBehaviour
{
    ItemsManagement itemsManagement;
    InventoryManager inventoryManager;
    AnimationManager animationManager;

    public Camera[] interactiveCam;
    public GameObject[] interactiveObject;
    public Sprite[] interactiveSprite;
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
        ObjectManager();
    }

    void ObjectManager()
    {
        if (interactiveCam[0].enabled) //Calendar Cam
        {
            GameObject interactiveObj = TouchManager(interactiveCam[0]);
            if (interactiveObj == interactiveObject[0])
            {

                if (itemsManagement.stageOneItems.Room1WetDishCloth)
                {
                    animationManager.Room1CalendarAnim();                   
                }
            }
        }

        else if (interactiveCam[1].enabled) // Stain Cam
        {
            GameObject interactiveObj = TouchManager(interactiveCam[1]);
            if (interactiveObj == interactiveObject[1])
            {
                if(itemsManagement.stageOneItems.Room2Mob)
                {
                    animationManager.Room2MobAnim();
                }
            }    
        }
        
    }



    GameObject TouchManager(Camera interactiveCam)
    {
        if (interactiveCam.enabled) //Calendar Cam
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Ray ray = interactiveCam.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (touch.phase == TouchPhase.Began)
                {

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.CompareTag("Objects"))
                        {
                            GameObject touchedObj = hit.collider.gameObject;
                            return touchedObj;
                        }
                    }


                }
            }
        }
        return null;
    }
    
}
