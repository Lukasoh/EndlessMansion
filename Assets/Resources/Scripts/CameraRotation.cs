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
    private bool screenDragging = false;
    private bool playerMovement;

    // Vertical Rotation
    private float initialRotationY;
    private float minRotationX = -90f;
    private float maxRotationX = 90f;



    // Update is called once per frame
    void Start()
    {
        joystick_Manager = FindObjectOfType<JoystickManager>();
        inventoryManager = FindObjectOfType<InventoryManager>();

    }


    void Update()
    {
        if(!inventoryManager.inventoryOn)
        {
            CameraManager();
        }
        else
        {
            // Camera function stop
        }
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

                        if (Mathf.Abs(rotationY) >= Mathf.Abs(rotationX))
                        {
                            transform.Rotate(0, rotationY / (Time.deltaTime * 10), 0);
                        }
                        else
                        {
                            cameraTransform.Rotate(-rotationX / (Time.deltaTime * 10), 0, 0);
                        }



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

                                if (Mathf.Abs(rotationY) >= Mathf.Abs(rotationX))
                                {
                                    transform.Rotate(0, rotationY / (Time.deltaTime * 10), 0);
                                }
                                else
                                {
                                    cameraTransform.Rotate(-rotationX / (Time.deltaTime * 10), 0, 0);

                                }

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
