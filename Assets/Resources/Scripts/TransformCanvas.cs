using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformCanvas : MonoBehaviour
{
    public Transform
targetCamera;

    // Update is called once per frame
    void Update()
    {
        transform.position = targetCamera.position;
        transform.rotation = targetCamera.rotation;
    }
}
