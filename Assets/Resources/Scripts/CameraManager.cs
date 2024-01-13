using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    //Import

    
    //joystick
    public Image joystickImg;
    public Image controllerImg;
    public GameObject backBtn;

    bool storageCamOn; 

    public Text statusTxt;
    //Object List
    public GameObject[] interactiveObject;
    public GameObject storageKeypad;
    //Camera List
    public Camera[] cameraList;
    public Camera mainCam;

    Collider storageKeypadCollider;

    // Start is called before the first frame update
    void Start()
    {
        storageKeypadCollider = storageKeypad.GetComponent<Collider>();

        mainCam.enabled = true;
        
        backBtn.SetActive(false);

        for (int i = 0; i < cameraList.Length; i++)
        {
            cameraList[i].enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        ObjectOnClick();

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

    }

    public void ObjectOnClick()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = mainCam.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (touch.phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == interactiveObject[0])
                {
                    Debug.Log("KeyPad Touched");
                    SwitchCamera(mainCam, cameraList[0]); // Room1Storage Cam
                    joystickImg.enabled = false;
                    controllerImg.enabled = false;
                    backBtn.SetActive(true);
                    storageCamOn = true;
                }

                else if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == interactiveObject[1])
                {
                    Debug.Log("Calendar Touched");
                    SwitchCamera(mainCam, cameraList[1]); // Room1Calendar Cam
                    joystickImg.enabled = false;
                    controllerImg.enabled = false;
                    backBtn.SetActive(true);

                }

                else if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == interactiveObject[2])
                {
                    Debug.Log("Stain Touched");
                    SwitchCamera(mainCam, cameraList[2]); // Room2Stain Cam
                    joystickImg.enabled = false;
                    controllerImg.enabled = false;
                    backBtn.SetActive(true);

                }

                else if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == interactiveObject[3])
                {
                    Debug.Log("Bookshelf Touched");
                    SwitchCamera(mainCam, cameraList[3]); // Room2BookShelf Cam
                    joystickImg.enabled = false;
                    controllerImg.enabled = false;
                    backBtn.SetActive(true);

                }



            }
            


        }



    }

    public void BackToMainCam()
    {
        mainCam.enabled = true;
        storageCamOn = false;

        joystickImg.enabled = true;
        controllerImg.enabled = true;

        backBtn.SetActive(false);

        for (int i = 0; i < cameraList.Length; i++)
        {
            cameraList[i].enabled = false;
        }
    }
}
