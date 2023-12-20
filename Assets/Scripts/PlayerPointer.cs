using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointer : MonoBehaviour
{
    protected Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("UIRaycastCamera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(true);
        Vector3 camPos = mainCamera.gameObject.transform.position;
        if (camPos[1] <0)
        {
            transform.position = new Vector3(camPos[0], -camPos[1], camPos[2]);
        }
    }
}
