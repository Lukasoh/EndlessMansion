using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneBool : MonoBehaviour
{
    public bool animDone;
    private MainSceneBool instance; 
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }
}
