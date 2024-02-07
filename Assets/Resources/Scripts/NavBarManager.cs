using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavBarManager : MonoBehaviour
{
    public GameObject optionPnl;

    public void NavBarOnClick()
    {

        if (optionPnl.activeSelf)
        {
            optionPnl.SetActive(false);
        }
        else
        {
            optionPnl.SetActive(true);
        }
    }
}
