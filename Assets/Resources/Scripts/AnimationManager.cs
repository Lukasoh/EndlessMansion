using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator[] animator;
    CameraManager cameraManager;

    public Camera[] cameraList; 

    // Start is called before the first frame update
    void Start()
    {
        cameraManager = FindObjectOfType<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoorOpenAnim(GameObject doorObject)
    {
        if(doorObject.name == "S1Room1")
        {
            doorObject = GameObject.Find("S1Room1");
            Animator currentAnim = animator[0];
            if (currentAnim != null)
            {
                currentAnim.SetBool("s1Room1Open", true);
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
            Debug.Log("Anim still fine");
            currentAnim.SetBool("storageUnlocked", true);
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
            currentAnim.SetBool("RemovedStain", true);
            currentAnim.SetBool("RemovedStain", true);


            currentAnim.SetBool("RemovedStain", true);



            currentAnim.SetBool()
        }
    }
    


    IEnumerator BacktoMainCam()
    {
        yield return new WaitForSeconds(2.5f);
        cameraManager.BackToMainCam();
    }







}
