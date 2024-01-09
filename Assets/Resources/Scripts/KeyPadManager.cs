using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class KeyPadManager : MonoBehaviour
{
    public GameObject[] storageNumBtn;
    public GameObject[] keyboardScreen;

    public TextMeshPro storageTm;

    AnimationManager animationManager;
    CameraManager cameraManager;
    
    string storagePw = "";
    string safePw;

    private bool resetStorageNum;

    public bool storageUnlocked; 
    // Start is called before the first frame update
    void Start()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        animationManager = FindObjectOfType<AnimationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PressBtn();
        
        storageTm.text = storagePw;
        

    }

    void PressBtn()
    {
        if (cameraManager != null)
        {
            
            if (cameraManager.cameraList[0].enabled)
            {
                
                if (storageNumBtn != null)
                {
                    
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Began)
                        {
                            
                            Ray ray = cameraManager.cameraList[0].ScreenPointToRay(touch.position);
                            RaycastHit hit;

                            for (int i = 0; i < storageNumBtn.Length; i++)
                            {
                                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == storageNumBtn[i])
                                {
                                    if(storagePw.Length < 4)
                                    {
                                        storagePw += i.ToString();
                                        Debug.Log(storagePw);
                                        KeyboardNumManager();
                                    }
                                    



                                }
                            }
                        }


                        
                    }    
                }
                
            }
        }
        
        
    }

    void KeyboardNumManager()
    {
        if(storagePw.Length == 4 && storagePw != "1219")
        {
            StartCoroutine(ResetScreenNum());
        }

        if(storagePw == "1219")
        {
            Debug.Log("Yeah!");
            animationManager.Room1StorageOpenAnim();
        }

        
    }

    IEnumerator ResetScreenNum()
    {
        yield return new WaitForSeconds(0.5f);
        storagePw = "";
    }






}
