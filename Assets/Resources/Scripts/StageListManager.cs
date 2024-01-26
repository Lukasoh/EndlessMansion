using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageListManager : MonoBehaviour
{
    
    public Button stageBtn;
    private bool isEnabled;
    
    public Animator[] stageAnim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setStageInfoPnl()
    {
        if(!isEnabled)
        {

            stageAnim[0].SetBool("isSelected", true);
            stageAnim[1].SetBool("isSelected", true);
                 
            isEnabled = true;
        }
        else
        {

            stageAnim[0].SetBool("isSelected", false);
            stageAnim[1].SetBool("isSelected", false);

            isEnabled = false;
                     
        }
    }

    
}
