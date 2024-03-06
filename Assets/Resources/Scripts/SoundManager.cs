using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SoundManager : MonoBehaviour
{    
    public SoundData soundData;


    // Start is called before the first frame update
    void Start()
    {
        LoadSoundDataToJson();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSoundDataToJson()
    {

        string path = Path.Combine(Application.persistentDataPath, "soundData.json");
        if (!File.Exists(path))
        {
            SoundData soundData = new SoundData();

            soundData.backgroundSound = 50f;
            soundData.voiceSound = 50f;
            soundData.effectSound = 50f;

            string newSoundData = JsonUtility.ToJson(soundData, true);
            File.WriteAllText(path, newSoundData);
        }
        string jsonData = File.ReadAllText(path);
        soundData = JsonUtility.FromJson<SoundData>(jsonData);
        SetVolume();
    }

    public void SaveSoundDataToJson()
    {
        string jsonData = JsonUtility.ToJson(soundData, true);
        string path = Path.Combine(Application.persistentDataPath, "soundData.json");

        File.WriteAllText(path, jsonData);
    }

    public void InGameSaveAndLoadDataToJson()
    {
        SaveSoundDataToJson();
        SetVolume();
        
    }
    public void SetVolume()
    {
        AudioSource thisAudio = this.GetComponent<AudioSource>();

        if (this.tag == "Background")
        {
            thisAudio.volume = soundData.backgroundSound / 100;
        }
        else if (this.tag == "Voice")
        {
            thisAudio.volume = soundData.voiceSound / 100;
        }
        else if(this.tag == "Effect")
        {
            thisAudio.volume = soundData.effectSound / 100;
        }
        else
        {

        }
    }
}

[System.Serializable]
public class SoundData
{
    public float backgroundSound;
    public float voiceSound;
    public float effectSound;
}
