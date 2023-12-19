using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookmark : MonoBehaviour
{
    public string markName;
    public Vector3 position;
    public GameObject markPin;
    public bool selected;
    
    public Bookmark(string name, Vector3 position, GameObject pin)
    {
        this.markName = name;
        this.position = position;
        this.markPin = pin;
        selected = false;
    }
    public string GetName()
    {
        return markName;
    }
        
        
    public Vector3 GetPosition()
    {
        return position;

    }

    public void SetPosition(Vector3 position)
    {
        this.position = position;
    }

    public void SetName(string name)
    {
        this.markName = name;
    }

    public void SetPin(GameObject pin)
    {
        this.markPin = pin;
    }

    void Update()
    {   
        if (selected)
        {
            markPin.gameObject.transform.localScale += new Vector3(2,2,2);
        }
        else {markPin.gameObject.transform.localScale = new Vector3(1,1,1);}
        
    }

}