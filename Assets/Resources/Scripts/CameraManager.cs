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
    
    //Object List
    public GameObject[] interactiveObject;
    public GameObject storageKeypad;
    //Camera List
    public Camera[] cameraList;
    public Camera mainCam;

    Collider storageKeypadCollider;
    JsonManager jsonManager;
    JoystickManager joystickManager;
    CameraRotation cameraRotation;

    // Start is called before the first frame update
    void Start()
    {
        storageKeypadCollider = storageKeypad.GetComponent<Collider>();
        cameraRotation = FindObjectOfType<CameraRotation>();

        mainCam.enabled = true;
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
                        }


                    }

                    else if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == interactiveObject[1])
                    {
                        Debug.Log("Calendar Touched");
                        SwitchCamera(mainCam, cameraList[1]); // Room1Calendar Cam


                    }

                    else if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == interactiveObject[2])
                    {
                        Debug.Log("Stain Touched");
                        SwitchCamera(mainCam, cameraList[2]); // Room2Stain Cam


                    }

                    else if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == interactiveObject[3])
                    {
                        Debug.Log("Bookshelf Touched");
                        SwitchCamera(mainCam, cameraList[3]); // Room2BookShelf Cam

                        hit.collider.gameObject.SetActive(false);

                    }

                    else if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == interactiveObject[4])
                    {
                        Debug.Log("Scanner Touched");
                        SwitchCamera(mainCam, cameraList[4]); // Room2Scanner Cam

                    }
                }
                



            }
            


        }



    }

    public void BackToMainCam()
    {
        mainCam.enabled = true;
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
