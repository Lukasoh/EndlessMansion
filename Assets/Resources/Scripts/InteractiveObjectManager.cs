using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObjectManager : MonoBehaviour
{
    ItemsManagement itemsManagement;
    InventoryManager inventoryManager;
    AnimationManager animationManager;
    GuidelineManager guidelineManager;
    

    public Camera[] interactiveCam;
    public GameObject[] interactiveObject;
    

    public GameObject[] bookObj;
    bool[] bookTouched = { false, false, false, false, false, false, false, false };
    bool noBook;
    private string bookPwd;
    public Material[] colorMat;
    public Renderer[] signRenderer;
    public GameObject bookShelfDoor;
    private Rigidbody doorRb;
    private bool bookShelfOn;

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

       
        else
        {
            bookShelfOn = true;
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
