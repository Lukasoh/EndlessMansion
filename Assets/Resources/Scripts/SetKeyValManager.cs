using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SetKeyValManager : MonoBehaviour
{
    public string objKey;

    public TMP_FontAsset korFont;
    public TMP_FontAsset jpnFont;
    public TMP_FontAsset chnFont;
    public TMP_FontAsset defaultFont;

    TextMeshProUGUI thisTxt;
    LanguageManager languageManager;
    // Start is called before the first frame update
    void Start()
    {
        languageManager = FindObjectOfType<LanguageManager>();
        thisTxt = GetComponent<TextMeshProUGUI>();

        languageManager.LoadLanguageDataToJson();
        SetKeyValue();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetKeyValue()
    {
        
        if (languageManager != null)
        {
            
            if (languageManager.languageData.languageOn[0])
            {
                
                for (int i = 0; i < languageManager.englishData.key.Length; i++)
                {
                    if (languageManager.englishData.key[i] == objKey)
                    {
                        thisTxt.font = defaultFont;
                        thisTxt.text = languageManager.englishData.val[i];                       
                    }
                }
            }
            else if (languageManager.languageData.languageOn[1])
            {
                
                for (int i = 0; i < languageManager.koreanData.key.Length; i++)
                {
                    if (languageManager.koreanData.key[i] == objKey)
                    {
                        thisTxt.font = korFont;
                        thisTxt.text = languageManager.koreanData.val[i];
                    }
                }
            }
            else if (languageManager.languageData.languageOn[2])
            {
                for (int i = 0; i < languageManager.chineseData.key.Length; i++)
                {
                    if (languageManager.chineseData.key[i] == objKey)
                    {
                        thisTxt.font = chnFont;
                        thisTxt.text = languageManager.chineseData.val[i];
                    }
                }
            }
            else if (languageManager.languageData.languageOn[3])
            {
                for (int i = 0; i < languageManager.japaneseData.key.Length; i++)
                {
                    if (languageManager.japaneseData.key[i] == objKey)
                    {
                        thisTxt.font = jpnFont;
                        thisTxt.text = languageManager.japaneseData.val[i];
                    }
                }
            }
            else if (languageManager.languageData.languageOn[4])
            {
                for (int i = 0; i < languageManager.spanishData.key.Length; i++)
                {
                    if (languageManager.spanishData.key[i] == objKey)
                    {
                        thisTxt.font = defaultFont;
                        thisTxt.text = languageManager.spanishData.val[i];
                    }
                }
            }
            else if (languageManager.languageData.languageOn[5])
            {
                for (int i = 0; i < languageManager.frenchData.key.Length; i++)
                {
                    if (languageManager.frenchData.key[i] == objKey)
                    {
                        thisTxt.font = defaultFont;
                        thisTxt.text = languageManager.frenchData.val[i];
                    }
                }
            }

            
        }
    }
}
