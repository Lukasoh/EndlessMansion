using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public JoystickManager joystick_Manager;
    //public Transform player;
    public float moveSpeed = 0.5f;




    void Start()
    {
        joystick_Manager = FindObjectOfType<JoystickManager>();

    }

    void Update()
    {

        Movement();

    }

    public void Movement()
    {
        if (joystick_Manager != null)
        {
            Vector3 CurrentContPos = joystick_Manager.GetCurrentPos();
            Vector3 playerMove = new Vector3(CurrentContPos.x, 0f, CurrentContPos.y) * moveSpeed * Time.deltaTime;

            if (Input.touchCount > 0)
            {
                transform.Translate(playerMove);
            }

            if (Input.GetMouseButton(0))
            {
                transform.Translate(playerMove); // For desktop 
            }

        }






    }


}
