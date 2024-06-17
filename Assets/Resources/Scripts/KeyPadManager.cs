using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class KeyPadManager : MonoBehaviour
{
    public GameObject[] storageNumBtn;
    public GameObject[] safeNumBtn;

    public TextMeshPro storageTm;
    public TextMeshPro safeTm;

    AnimationManager animationManager;
    CameraManager cameraManager;
    
    string storagePw = "";
    string safePw = "";

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
        safeTm.text = safePw;

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

            else if (cameraManager.cameraList[5].enabled)
            {
                
                if (safeNumBtn != null)
                {

                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Began)
                        {
                            Debug.Log(safePw);
                            Ray ray = cameraManager.cameraList[5].ScreenPointToRay(touch.position);
                            RaycastHit hit;

                            for (int i = 0; i < safeNumBtn.Length; i++)
                            {
                                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == safeNumBtn[i])
                                {
                                    if (safePw.Length < 4)
                                    {
                                        safePw += i.ToString();
                                        Debug.Log(safePw);
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

        else if(storagePw == "1219")
        {
            Debug.Log("Yeah!");
            animationManager.Room1StorageOpenAnim();
        }

        if (safePw.Length == 4 && safePw != "1989")
        {
            StartCoroutine(ResetScreenNum());
        }

        else if (safePw == "1989")
        {
            Debug.Log("Yeah!");
            animationManager.Room2SafeAnim();
        }


    }

    IEnumerator ResetScreenNum()
    {
        yield return new WaitForSeconds(0.5f);
        storagePw = "";
        safePw = "";
    }






}
