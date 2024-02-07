using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickManager : MonoBehaviour
{
    public CameraRotation cameraRotation;

    InventoryManager inventoryManager;
    PlayerMovement playerMovement;
    JsonManager jsonManager;

    public Transform joystick;
    public Transform controller;

    //GameObject optionObj;
    
    public bool isDragging = false;


    private float maxDistance = 100f;
    private Vector3 CurrentPos;




    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        jsonManager = FindObjectOfType<JsonManager>();

        //optionObj = GameObject.Find("SettingBackgroundPnl");
    }

    void Update()
    {
        

        if (!inventoryManager.inventoryOn)
        {
            Controller_transform();
        }
        else
        {
            // Joystick function stop
        }

       
    }

    public void Controller_transform()
    {
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && IsTouchInRect(touch.position, joystick.GetComponent<RectTransform>()))
            {
                isDragging = true;
            }

            else if (touch.phase == TouchPhase.Moved && isDragging)
            {

                Vector2 localTouchPos = joystick.GetComponent<RectTransform>().InverseTransformPoint(touch.position);


                Vector3 clampedPos = Vector3.ClampMagnitude(new Vector3(localTouchPos.x, localTouchPos.y, 0), maxDistance + 20);
                controller.localPosition = clampedPos;



                //cameraRotation.enabled = true;

            }

            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
                controller.localPosition = Vector3.zero;
                Vector3 charPos = playerMovement.currentPos;
                Vector3 charRot = playerMovement.currentRot;
                jsonManager.SaveCharacterPosition(charPos, charRot);
                //cameraRotation.enabled = false;
            }

            CurrentPos = controller.localPosition;
        }

        
    }

    
    







        

    public bool IsTouchInRect(Vector2 touchPos, RectTransform rectTransform)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, touchPos);
    }

    public Vector3 GetCurrentPos()
    {
        return CurrentPos;
    }

    public bool GetisDragging()
    {
        return isDragging;
    }

    public Transform GetJoystick()
    {
        return joystick;
    }
}

