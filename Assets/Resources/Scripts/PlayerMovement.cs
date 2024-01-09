using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    DoorOpenManager doorOpenManager;

    public JoystickManager joystick_Manager;
    
    public float moveSpeed = 2f;

    bool detectCd;
    private CapsuleCollider playerCollider;
    void Start()
    {
        joystick_Manager = FindObjectOfType<JoystickManager>();
        doorOpenManager = FindObjectOfType<DoorOpenManager>();
       
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
                if(detectCd)
                {
                    Debug.Log("Minus Playermove");
                    transform.Translate(-playerMove * 0.8f);
                    StartCoroutine(ResetDetectCd());                   
                }
                else
                {
                    transform.Translate(playerMove);
                }
                
                
            }

        }

    }



    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Detected Collider");
        detectCd = true;
    }

    IEnumerator ResetDetectCd()
    {
        
        yield return new WaitForSeconds(0.07f);
       
        detectCd = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Doors"))
        {
            if(doorOpenManager.isDoorLocked.s1Room1 && doorOpenManager.doorObj[0] != null)
            {
                BoxCollider s1Room1Cd = doorOpenManager.doorObj[0].GetComponent<BoxCollider>();
                if (s1Room1Cd != null)
                {
                    s1Room1Cd.enabled = false;
                }

                
            }
            
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Doors"))
        {
            Debug.Log("Exited Door");
        }
    }

}
