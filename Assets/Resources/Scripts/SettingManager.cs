using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingManager : MonoBehaviour
{
    public ScrollRect scrollRect;
    private Vector2 lastPosition;
    static float verticalLocation;
    

    void Start()
    {
        
        if (scrollRect == null)
        {
            scrollRect = GetComponent<ScrollRect>();
        }
    }

    void Update()
    {
        verticalLocation = scrollRect.content.anchoredPosition.y;
        

        if (verticalLocation >= 978)
        {
            scrollRect.content.anchoredPosition = new Vector2(scrollRect.content.anchoredPosition.x, 977);
        }
        else if (verticalLocation <= 0)
        {
            scrollRect.content.anchoredPosition = new Vector2(scrollRect.content.anchoredPosition.x, 1);
        }
    }
    
    

}
