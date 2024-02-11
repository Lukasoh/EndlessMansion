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

    public Camera[] cameraList;

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
                currentAnim.SetBool("s1Room1Open", true);
                jsonManager.SaveAnimStatus(doorObject.name, "S1Room1Anim", "s1Room1Open");


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
        Animator currentAnim = animator[2];
        if(currentAnim != null)
        {
           currentAnim.SetBool("RemovedStain", true);            
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

    public void HintAnim()
    {
        Debug.Log("HintAnim started");

        hintAnimator.SetBool("isUnlocked", true);
        
    }

    IEnumerator BacktoMainCam()
    {
        yield return new WaitForSeconds(2.5f);
        cameraManager.BackToMainCam();
    }







}
