using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockImgManager : MonoBehaviour
{
    HintManager hintManager;
    // Start is called before the first frame update
    void Awake()
    {
        hintManager = FindObjectOfType<HintManager>();
    }

    void OnEnable()
    {
        hintManager.LoadProgressToJson();
        hintManager.HintAtStart();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
