using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageListManager : MonoBehaviour
{
    public GameObject stagePnl;
    public Button stageBtn;
    private bool isEnabled;
    
    public Animator stageAnim;

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
            
            stageAnim.SetBool("isSelected", true);
            stagePnl.SetActive(true);       
            isEnabled = true;
        }
        else
        {
            
            stageAnim.SetBool("isSelected", false);
            stagePnl.SetActive(false);
                       
            isEnabled = false;
                     
        }
    }

    
}
