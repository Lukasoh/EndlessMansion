using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public JoystickManager joystick_Manager;
    InventoryManager inventoryManager;

    public Transform cameraTransform;
    public Transform canvas;
    public float rotationSpeed = 5f;
    private Vector2 touchStartPos;
    public bool screenDragging = false;
    private bool playerMovement;

    // Vertical Rotation
    private float initialRotationY;

    public GameObject optionObj;

    private float minRotationY = -80f;
    private float maxRotationY = 80f;

    private float currentRotationX = 0f;

    public Vector3 cameraDirection;

    // Update is called once per frame
    void Start()
    {
        joystick_Manager = FindObjectOfType<JoystickManager>();
        inventoryManager = FindObjectOfType<InventoryManager>();

    }


    void Update()
    {
        if (!inventoryManager.inventoryOn && !optionObj.activeSelf)
        {
            CameraManager();
        }
        else
        {
            // Camera function stop
        }

        cameraDirection = transform.forward;
        
    }

    public void CameraManager()
    {
        Transform joystick = joystick_Manager.GetJoystick();

        if (Input.touchCount > 0)
        {

            playerMovement = joystick_Manager.GetisDragging();

            if (playerMovement == false)
            {
                Touch touch = Input.GetTouch(0);


                if (touch.phase == TouchPhase.Began)
                {
                    touchStartPos = touch.position;
                    screenDragging = true;

                }

                else if (touch.phase == TouchPhase.Moved)
                {

                    if (screenDragging)
                    {
                        Vector2 touchDelta = touch.position - touchStartPos;

                        float rotationY = touchDelta.x * rotationSpeed * Time.deltaTime;
                        float rotationX = touchDelta.y * rotationSpeed * Time.deltaTime;

                        // 수평 회전 적용
                        transform.Rotate(0, rotationY, 0);

                        // 수직 회전 적용
                        currentRotationX -= rotationX;
                        currentRotationX = Mathf.Clamp(currentRotationX, minRotationY, maxRotationY);
                        cameraTransform.localEulerAngles = new Vector3(currentRotationX, cameraTransform.localEulerAngles.y, 0);

                        Vector3 currentRotation = transform.eulerAngles;
                        currentRotation.z = 0;
                        

                        touchStartPos = touch.position;



                    }

                }

                else if (touch.phase == TouchPhase.Ended)
                {
                    screenDragging = false;

                }


            }

            else
            {
                if (Input.touchCount > 1)
                {
                    Touch secondTouch = Input.GetTouch(1);
                    if (IsTouchInRect(secondTouch.position, joystick.GetComponent<RectTransform>()))
                    {

                    }
                    else
                    {
                        if (secondTouch.phase == TouchPhase.Began)
                        {
                            touchStartPos = secondTouch.position;
                            screenDragging = true;

                        }

                        else if (secondTouch.phase == TouchPhase.Moved)
                        {

                            if (screenDragging)
                            {
                                Vector2 touchDelta = secondTouch.position - touchStartPos;

                                float rotationY = touchDelta.x * rotationSpeed * Time.deltaTime;
                                float rotationX = touchDelta.y * rotationSpeed * Time.deltaTime;

                                // 수평 회전 적용
                                transform.Rotate(0, rotationY, 0);

                                // 수직 회전 적용
                                currentRotationX -= rotationX;
                                currentRotationX = Mathf.Clamp(currentRotationX, minRotationY, maxRotationY);
                                cameraTransform.localEulerAngles = new Vector3(currentRotationX, cameraTransform.localEulerAngles.y, 0);

                                Vector3 currentRotation = transform.eulerAngles;
                                currentRotation.z = 0;
                                transform.eulerAngles = currentRotation;

                                touchStartPos = secondTouch.position;

                            }

                        }

                        else if (secondTouch.phase == TouchPhase.Ended)
                        {
                            screenDragging = false;

                        }
                    }


                }
            }




        }
        else
        {

        }


    }

    bool IsTouchInRect(Vector2 touchPos, RectTransform rectTransform)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, touchPos, null);
    }






}