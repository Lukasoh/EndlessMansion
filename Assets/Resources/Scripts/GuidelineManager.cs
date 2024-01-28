using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidelineManager : MonoBehaviour
{
    public AudioClip[] audioClip;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScenarioOne()
    {
        int randNum = Random.Range(0, 3);
        audioSource.clip = audioClip[randNum];
        audioSource.Play();
    }

    public void ScenarioTwo()
    {
        int randNum = Random.Range(3, 5);
        audioSource.clip = audioClip[randNum];
        audioSource.Play();
    }

    public void ScenarioThree()
    {
        int randNum = Random.Range(6, 8);
        audioSource.clip = audioClip[randNum];
        audioSource.Play();
    }

    public void ScenarioFour()
    {
        int randNum = Random.Range(9, 11);
        audioSource.clip = audioClip[randNum];
        audioSource.Play();
    }

    public void ScenarioFive()
    {
        int randNum = Random.Range(12, 13);
        audioSource.clip = audioClip[randNum];
        audioSource.Play();
    }
}
