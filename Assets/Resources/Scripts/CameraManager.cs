using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{  
    public GameObject[] canvasObj;

    public GameObject inventoryPnl;
    public GameObject settingPnl;

    bool storageCamOn;
    bool storageUnlocked;

    bool safeCamOn;
    bool safeUnlocked;
    
    
    //Object List
    public GameObject[] interactiveObject;
    public GameObject storageKeypad;
    public GameObject safeDoor;
    
    //Camera List
    public Camera[] cameraList;
    public Camera mainCam;
    public Camera currentCam;

    Collider storageKeypadCollider;
    BoxCollider drawerCollider;
    public BoxCollider safeDoorCollider;
    public BoxCollider refriCollider;
    BoxCollider cabLockerCollider;

    JsonManager jsonManager;
    JoystickManager joystickManager;
    CameraRotation cameraRotation;

    // Start is called before the first frame update
    void Start()
    {
        storageKeypadCollider = storageKeypad.GetComponent<Collider>();
        safeDoorCollider = safeDoor.GetComponent<BoxCollider>();
        refriCollider = interactiveObject[7].GetComponent<BoxCollider>();
        cabLockerCollider = interactiveObject[8].GetComponent<BoxCollider>();

        cameraRotation = FindObjectOfType<CameraRotation>();
        drawerCollider = interactiveObject[6].GetComponent<BoxCollider>();

        mainCam.enabled = true;
        currentCam = mainCam;
        canvasObj[0].SetActive(true);
        canvasObj[1].SetActive(false);
        jsonManager = FindObjectOfType<JsonManager>();
        joystickManager = FindObjectOfType<JoystickManager>();

        for (int i = 0; i < cameraList.Length; i++)
        {
            cameraList[i].enabled = false;
        }

        

    }

    // Update is called once per frame
    
    
    
    void Update()
    {
        if (!inventoryPnl.activeSelf && !settingPnl.activeSelf)
        {
            ObjectOnClick();
        }
        

        if(storageCamOn)
        {
            storageKeypadCollider.enabled = false;
        }
        else
        {
            storageKeypadCollider.enabled = true;
        }

        if (cameraList[5].enabled)
        {
            safeDoorCollider.enabled = false;
        }
        else
        {
            safeDoorCollider.enabled = true;
        }

        if (cameraList[6].enabled)
        {
            drawerCollider.enabled = false;
        }
        else
        {
            drawerCollider.enabled = true;
        }
        if (cameraList[7].enabled)
        {
            cabLockerCollider.enabled = false;
        }
        else
        {
            cabLockerCollider.enabled = true;
        }

    }

    void SwitchCamera(Camera orginCam, Camera newCam)
    {
        orginCam.enabled = false;
        newCam.enabled = true;
        canvasObj[0].SetActive(false);
        canvasObj[1].SetActive(true);
    }

    public void ObjectOnClick()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = mainCam.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (touch.phase == TouchPhase.Ended && !cameraRotation.screenDragging)
            {
                if(!joystickManager.IsTouchInRect(touch.position, joystickManager.joystick.GetComponent<RectTransform>()))
                {
                    
                    if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == interactiveObject[0])
                    {
                        foreach (string storage in jsonManager.itemActive.objName)
                        {
                            if (storage == "Storage")
                            {
                                storageUnlocked = true;
                                break;
                            }

                        }

                        if (!storageUnlocked)
                        {
                            SwitchCamera(mainCam, cameraList[0]); // Room1Storage Cam                   
                            storageCamOn = true;
                            currentCam = cameraList[0];
                        }


                    }

                    else if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == interactiveObject[1])
                    {
                        Debug.Log("Calendar Touched");
                        SwitchCamera(mainCam, cameraList[1]); // Room1Calendar Cam
                        currentCam = cameraList[1];


                    }

                    else if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == interactiveObject[2])
                    {
                        Debug.Log("Stain Touched");
                        SwitchCamera(mainCam, cameraList[2]); // Room2Stain Cam
                        currentCam = cameraList[2];


                    }

                    else if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == interactiveObject[3])
                    {
                        Debug.Log("Bookshelf Touched");
                        SwitchCamera(mainCam, cameraList[3]); // Room2BookShelf Cam
                        currentCam = cameraList[3];
                        hit.collider.gameObject.SetActive(false);

                    }

                    else if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == interactiveObject[4])
                    {
                        Debug.Log("Scanner Touched");
                        SwitchCamera(mainCam, cameraList[4]); // Room2Scanner Cam
                        currentCam = cameraList[4];
                    }

                    else if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == interactiveObject[5])
                    {
                        Debug.Log("Safe Touched");
                        SwitchCamera(mainCam, cameraList[5]); //Room2Safe Cam
                        currentCam = cameraList[5];
                    }

                    else if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == interactiveObject[6])
                    {
                        Debug.Log("Drawer Touched");
                        SwitchCamera(mainCam, cameraList[6]); //Room4Drawer Cam
                        currentCam = cameraList[6];
                    }

                    else if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == interactiveObject[8])
                    {
                        Debug.Log("Cabinet Locker Touched");
                        SwitchCamera(mainCam, cameraList[7]); //Room2Cabinet Locker Cam
                        currentCam = cameraList[7];
                    }
                    else if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == interactiveObject[9])
                    {
                        Debug.Log("WaterBottle Touched");
                        SwitchCamera(mainCam, cameraList[8]); //Room2Cabinet Locker Cam
                        currentCam = cameraList[8];
                    }
                }
                



            }
            


        }



    }

    public void BackToMainCam()
    {
        mainCam.enabled = true;
        currentCam = mainCam;
        storageCamOn = false;

        interactiveObject[3].SetActive(true);
        canvasObj[0].SetActive(true);
        canvasObj[1].SetActive(false);

        for (int i = 0; i < cameraList.Length; i++)
        {
            cameraList[i].enabled = false;
        }
    }
}
