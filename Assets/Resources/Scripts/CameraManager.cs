using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    //Import

    private bool isDragging = false;
    //joystick
    public Image joystickImg;
    public Image controllerImg;
    public GameObject backBtn;

    //Object List
    public Object[] interactiveObject;

    //Camera List
    public Camera[] cameraList;
    public Camera mainCam;


    // Start is called before the first frame update
    void Start()
    {

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
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (touch.phase == TouchPhase.Began && isDragging == false)
            {
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == interactiveObject[0])
                {
                    SwitchCamera(mainCam, cameraList[0]); // Room1Table Cam
                    joystickImg.enabled = false;
                    controllerImg.enabled = false;
                    backBtn.SetActive(true);


                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                isDragging = true;
            }

            else
            {
                isDragging = false;
            }


        }



    }

    public void BackToMainCam()
    {
        mainCam.enabled = true;


        joystickImg.enabled = true;
        controllerImg.enabled = true;

        backBtn.SetActive(false);

        for (int i = 0; i < cameraList.Length; i++)
        {
            cameraList[i].enabled = false;
        }
    }
}
