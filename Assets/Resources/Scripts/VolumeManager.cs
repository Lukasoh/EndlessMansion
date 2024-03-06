using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class VolumeManager : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject volumeController; 
    public RectTransform volumeBar; 
    public TextMeshProUGUI volumeStatus; 
    public Button muteBtn;

    private string objName;
    private int soundVolume; 
    private bool[] isMuted;

    float newSoundData;

    SoundManager soundManager;

    float ConvertIntToFloat(float soundVal)
    {
        float minInput = 0f;
        float maxInput = 100f;
        float minOutput = -150f;
        float maxOutput = 150f;

        return (maxOutput - minOutput) * (soundVal - minInput) / (maxInput - minInput) + minOutput;
    }

    void Start()
    {
        

        objName = this.name;

        soundManager = FindObjectOfType<SoundManager>();

        if(objName == "Controller1")
        {
            float soundVal = (float)soundManager.soundData.backgroundSound;
            newSoundData = ConvertIntToFloat(soundVal);
            
        }
        else if (objName == "Controller2")
        {
            float soundVal = (float)soundManager.soundData.voiceSound;
            newSoundData = ConvertIntToFloat(soundVal);
        }
        else if (objName == "Controller3")
        {
            float soundVal = (float)soundManager.soundData.effectSound;
            newSoundData = ConvertIntToFloat(soundVal);
        }

        this.transform.localPosition = new Vector3(newSoundData, this.transform.localPosition.y, this.transform.localPosition.z);
        UpdateVolume(newSoundData);

    }

    void Update()
    {
        GetSoundData();
    }

    public void OnDrag(PointerEventData eventData)
    {

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(volumeBar, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
        {

            localPoint.y = this.transform.localPosition.y;
            localPoint.x = Mathf.Clamp(localPoint.x, volumeBar.rect.min.x + 25f, volumeBar.rect.max.x - 25f);

            this.transform.localPosition = new Vector3(localPoint.x, localPoint.y, this.transform.localPosition.z);

            UpdateVolume(localPoint.x);
            
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        soundManager.InGameSaveAndLoadDataToJson();
    }

    void UpdateVolume(float xPosition)
    {
        
        float minValInput = volumeBar.rect.min.x + 25f;
        float maxValInput = volumeBar.rect.max.x - 25f;
        float minValOutput = 0;
        float maxValOutput = 100;

        int volumeVal = (int)(((xPosition - minValInput) / (maxValInput - minValInput)) * (maxValOutput - minValOutput) + minValOutput);
        volumeStatus.text = volumeVal.ToString() + "%";
        soundVolume = volumeVal;

        if(xPosition <= minValInput)
        {
            muteBtn.GetComponent<Image>().color = new Color(207f / 255f, 0f, 0f);
        }
        else
        {
            muteBtn.GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }

        
    }

    public void MuteSound()
    {
        this.transform.localPosition = new Vector3(-150f, this.transform.localPosition.y, this.transform.localPosition.z);
        UpdateVolume(-150f);
        GetSoundData();
        soundManager.InGameSaveAndLoadDataToJson();
    }

    void GetSoundData()
    {
        if(soundManager != null)
        {
            if(objName == "Controller1")
            {
                soundManager.soundData.backgroundSound = soundVolume;
            }
            else if(objName == "Controller2")
            {
                soundManager.soundData.voiceSound = soundVolume;
            }
            else if(objName == "Controller3")
            {
                soundManager.soundData.effectSound = soundVolume;
            }
            else
            {
                Debug.Log("No Name Detected");
            }
        }
        else
        {
            Debug.Log("SoundManager is NULL");
        }
                
    }
    
}



