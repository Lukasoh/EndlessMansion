using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;
    private GameObject doorObj;
    // Start is called before the first frame update
    void Start()
    {
        
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
            animator = doorObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("s1Room1Open", true);
            }

            else
            {
                Debug.LogError("animator is null!");
            }
            
        }
    }
}
