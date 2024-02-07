using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public void ChangeSceneToStage1MediumLvl()
    {
        SceneManager.LoadScene("Scene/RoomEscape"); 
    }

    public void ChangeSceneToStageList()
    {
        SceneManager.LoadScene("Scene/StageListScene");
    }

    public void ChangeSceneToMainScene()
    {
        SceneManager.LoadScene("Scene/MainScene");
    }
}
