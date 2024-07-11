using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObjectManager : MonoBehaviour
{
    ItemsManagement itemsManagement;
    InventoryManager inventoryManager;
    AnimationManager animationManager;
    GuidelineManager guidelineManager;

    public Camera mainCam;
    public Camera[] interactiveCam;
    public GameObject[] interactiveObject;

    public GameObject[] cabinetMark;

    public GameObject[] bookObj;
    public GameObject[] drawerObj;
    bool[] bookTouched = { false, false, false, false, false, false, false, false };
    bool[] drawerTouched = { false, false, false };
    bool noBook;
    private string bookPwd;
    
    public Material[] colorMat;
    public Renderer[] signRenderer;
    public GameObject bookShelfDoor;
    private Rigidbody doorRb;
    private bool bookShelfOn;
    private int[] cabinetLock;
    //cabinet Object
    public bool isDone = false;

    // Start is called before the first frame update
    void Start()
    {
        itemsManagement = FindObjectOfType<ItemsManagement>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        animationManager = FindObjectOfType<AnimationManager>();
        guidelineManager = FindObjectOfType<GuidelineManager>();
        

        doorRb = bookShelfDoor.GetComponent<Rigidbody>();
        doorRb.isKinematic = true;
        bookPwd = "";

        bookShelfOn = true;
        cabinetLock = new int[3];

        

    }

    // Update is called once per frame
    void Update()
    {
        ObjectManager();
        BookShelfSign();
        
    }

    void ObjectManager()
    {
        if (interactiveCam[0].enabled) //Calendar Cam
        {
            GameObject interactiveObj = TouchManager(interactiveCam[0]);
            if (interactiveObj == interactiveObject[0])
            {

                if (itemsManagement.stageOneItems.Room1WetDishCloth)
                {
                    animationManager.Room1CalendarAnim();
                }
                else
                {
                    if (inventoryManager.equipedItem != null)
                    {
                        guidelineManager.ScenarioOne();
                    }

                }
            }
        }

        else if (interactiveCam[1].enabled) // Stain Cam
        {
            GameObject interactiveObj = TouchManager(interactiveCam[1]);
            if (interactiveObj == interactiveObject[1])
            {
                if (itemsManagement.stageOneItems.Room2Mop)
                {
                    animationManager.Room2MobAnim();
                }
                else
                {
                    if (inventoryManager.equipedItem != null)
                    {
                        guidelineManager.ScenarioOne();
                    }
                }
            }
        }

        else if (interactiveCam[2].enabled) //BookShelf
        {

            GameObject interactiveObj = TouchManager(interactiveCam[2]);
            StartCoroutine(BookTouchOn());
            if (!bookShelfOn)
            {
                for (int i = 0; i < bookObj.Length; i++)
                {
                    if (bookObj[i] == interactiveObj)
                    {

                        if (bookTouched[i])
                        {
                            bookTouched[i] = false;
                            noBook = true;
                        }
                        else
                        {
                            bookPwd += i.ToString();
                            Debug.Log(bookPwd);
                            bookTouched[i] = true;
                            noBook = false;
                            for (int j = 0; j < bookObj.Length; j++)
                            {
                                if (j != i)
                                {
                                    bookTouched[j] = false;
                                }
                            }
                        }

                    }
                }
                animationManager.Room2BookShelfAnim(bookTouched[0], bookTouched[1], bookTouched[2], bookTouched[3], bookTouched[4], bookTouched[5], bookTouched[6], bookTouched[7], noBook);
            }


        }
        else if (interactiveCam[3].enabled) //Scanner
        {
            GameObject interactiveObj = TouchManager(interactiveCam[3]);
            //Debug.Log(interactiveObj.name);
            if (interactiveObj == interactiveObject[3])
            {
                if (itemsManagement.stageOneItems.Room2Book)
                {
                    Debug.Log("Book Equiped");
                    animationManager.Room2ScannerAnim();
                }
                else
                {
                    if (inventoryManager.equipedItem != null)
                    {
                        guidelineManager.ScenarioOne();
                        Debug.Log("Equiped Item null");
                    }
                }
            }
        }

        else if (interactiveCam[5].enabled) //Drawer
        {
            GameObject interactiveObj = TouchManager(interactiveCam[5]);
            
            for (int i = 0; i < drawerObj.Length; i++)
            {
                if (interactiveObj == drawerObj[i])
                {
                    
                    if (drawerTouched[i])
                    {
                        Debug.Log("Already Opened");
                        drawerTouched[i] = false;
                    }
                    else
                    {
                        drawerTouched[i] = true;

                        for (int j = 0; j < drawerObj.Length; j++)
                        {
                            if (j != i)
                            {
                                drawerTouched[j] = false;
                            }
                        }
                    }
                    
                }

                
                
            }

            animationManager.Room4DrawerAnim(drawerTouched[0], drawerTouched[1], drawerTouched[2]);
        }

        else if (mainCam.enabled)
        {
            GameObject interactiveObj = TouchManager(mainCam);

            if(interactiveObj == interactiveObject[6])
            {
                Debug.Log("refri touched");
                Animator refriAnim = interactiveObj.GetComponent<Animator>();
                bool doorOpened = refriAnim.GetBool("doorOpened");
                animationManager.Room4RefrigeratorAnim(doorOpened);
            }
            
            
        }

        else if (interactiveCam[6])
        {
            GameObject interactiveObj = TouchManager(interactiveCam[6]);

            for (int i = 0; i < 3; i++)
            {
                if (interactiveObj == cabinetMark[i])
                {
                    
                    animationManager.Room4CabinetLockerAnim(cabinetLock, i);
                    CabinetPwd(i);
                    Debug.Log(i + " : " + cabinetLock[i]);


                }     
            }

            if (cabinetLock[0] == 3 && cabinetLock[1] == 2 && cabinetLock[2] == 6)
            {
                animationManager.Room4CabinetDoorAnim();
            }
           

        }

       
        else
        {
            bookShelfOn = true;
        }


    }

    void CabinetPwd(int i)
    {
        if (cabinetLock[i] != 7)
        {
            cabinetLock[i]++;
        }
        else
        {
            cabinetLock[i] = 0;
        }
    }

    

    void BookShelfSign()
    {
        for (int i = 0; i < signRenderer.Length; i++)
        {
            if (bookPwd == "")
            {
                signRenderer[i].material = colorMat[0];
            }

            else
            {
                if (bookPwd.Length == i && i < 6)
                {
                    signRenderer[i - 1].material = colorMat[1];
                }
                else if (bookPwd.Length == 6)
                {
                    signRenderer[5].material = colorMat[1];
                    
                    if(bookPwd == "451370")
                    {
                        for (int j = 0; j < signRenderer.Length; j++)
                        {
                            signRenderer[i].material = colorMat[3];
                            if (doorRb != null)
                            {
                                doorRb.isKinematic = false;
                            }
                        }


                    }
                    else
                    {
                        for (int j = 0; j < signRenderer.Length; j++)
                        {
                            signRenderer[i].material = colorMat[2];
                            StartCoroutine(BookShelfNumWrong());
                        }
                    }
                }
            }
        }
    }





    GameObject TouchManager(Camera interactiveCam)
    {
        if (interactiveCam.enabled) //Calendar Cam
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Ray ray = interactiveCam.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (touch.phase == TouchPhase.Began)
                {

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.CompareTag("Objects"))
                        {
                            GameObject touchedObj = hit.collider.gameObject;
                            return touchedObj;
                        }
                    }


                }
            }
        }
        return null;
    }

    IEnumerator BookShelfNumWrong()
    {
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < bookObj.Length; i++)
        {
            bookTouched[i] = false;
            noBook = true;
        }
        for (int i = 0; i < signRenderer.Length; i++)
        {
            signRenderer[i].material = colorMat[0];
        }
        bookPwd = "";
        
    }

    IEnumerator BookTouchOn()
    {
        yield return new WaitForSeconds(0.2f);
        bookShelfOn = false;

    }
}
