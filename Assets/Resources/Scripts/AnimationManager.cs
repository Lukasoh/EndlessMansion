using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator[] animator;
    public Animator characterAnimator;
    public Animator hintAnimator;
    CameraManager cameraManager;
    JsonManager jsonManager;
    JoystickManager joystickManager;
    HintManager hintManager;


    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        joystickManager = FindObjectOfType<JoystickManager>();
        jsonManager = FindObjectOfType<JsonManager>();
        hintManager = FindObjectOfType<HintManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CharacterAnim(joystickManager.isDragging);
    }

    public void DoorOpenAnim(GameObject doorObject)
    {
        if(doorObject.name == "S1Room1")
        {
            doorObject = GameObject.Find("S1Room1");
            Animator currentAnim = animator[0];
            if (currentAnim != null)
            {
                Debug.Log("Door Anim is not null");
                currentAnim.SetBool("s1Room4Opened", true);
                jsonManager.SaveAnimStatus(doorObject.name, "S1Room1Anim", "s1Room4Opened");


            }

            else
            {
                Debug.LogError("animator is null!");
            }
            
        }
        else if(doorObject.name == "S1Room4")
        {
            doorObject = GameObject.Find("S1Room4");
            Animator currentAnim = animator[8];

            if (currentAnim != null)
            {

                currentAnim.SetBool("S1Room4Opened", true);
                jsonManager.SaveAnimStatus(doorObject.name, "Room4DoorAnim", "S1Room4Opened");
            }
            else
            {
                Debug.LogError("animator is null!");
            }
            
        }
    }

    public void Room1StorageOpenAnim()
    {
        
        Animator currentAnim = animator[1];
        if(currentAnim != null)
        {
            GameObject storageObj = GameObject.Find("Room1Storage");
            
            currentAnim.SetBool("storageUnlocked", true);
            jsonManager.SaveAnimStatus(storageObj.name, "StorageAnim", "storageUnlocked");
            hintManager.ObjectHintData(storageObj.name);
            StartCoroutine(BacktoMainCam());
        }
        else
        {
            Debug.Log("Anim null");
        }
    }
    
    public void Room1CalendarAnim()
    {
        GameObject calendarObj = GameObject.Find("Calendar");
        Animator currentAnim = animator[2];
        if(currentAnim != null)
        {
           currentAnim.SetBool("RemovedStain", true);
           jsonManager.SaveAnimStatus(calendarObj.name, "CalendarAnim", "RemovedStain");
        }
    }

    public void Room2MobAnim()
    {
        Animator currentAnim = animator[3];
        if(currentAnim != null)
        {
            currentAnim.SetBool("isCleaned", true);
        }
    }
    
    public void Room2BookShelfAnim(bool Book1, bool Book2, bool Book3, bool Book4, bool Book5, bool Book6, bool Book7, bool Book8, bool NoBook)
    {
        Animator currentAnim = animator[4];
        if(currentAnim != null)
        {
            bool[] books = { Book1, Book2, Book3, Book4, Book5, Book6, Book7, Book8 };

            for (int i = 0; i < 8; i++)
            {
                currentAnim.SetBool($"Book{i + 1}On", books[i]);
            }

            currentAnim.SetBool("NoBook", NoBook);
        }
        
    }
    
    public void Room2ScannerAnim()
    {
        GameObject scannerObj = GameObject.Find("Scanner");
        Animator scannerAnim = animator[5];
        Animator bookAnim = animator[6];
        if (scannerAnim != null && bookAnim != null)
        {
            scannerAnim.SetBool("isBook", true);
            bookAnim.SetBool("bookScanned", true);
            jsonManager.SaveAnimStatus(scannerObj.name, "ScannerAnim", "isBook");
        }
    }

    public void Room2SafeAnim()
    {
        GameObject safeObj = GameObject.Find("Safe");
        Animator safeAnim = animator[7];
        if(safeAnim != null)
        {
            safeAnim.SetBool("doorOpened", true);
            jsonManager.SaveAnimStatus(safeObj.name, "SafeAnim", "doorOpened");
            cameraManager.safeDoorCollider.center = new Vector3(-25f, 0f, -0.4f);
            cameraManager.safeDoorCollider.size = new Vector3(50f, 1.0f, 0.2f);
        }
    }


    void CharacterAnim(bool isWalking)
    {
        if(characterAnimator != null)
        {
            if(isWalking)
            {
                characterAnimator.SetBool("isWalking", true);
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                
            }
            else
            {
                characterAnimator.SetBool("isWalking", false);
                characterAnimator.Play("StopMotion");
                audioSource.Stop();
            }
        }
    }

    

    IEnumerator BacktoMainCam()
    {
        yield return new WaitForSeconds(2.5f);
        cameraManager.BackToMainCam();
    }







}
