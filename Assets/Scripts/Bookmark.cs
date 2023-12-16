using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookmark : MonoBehaviour
{
    public string markName;
    public Vector3 position;
    
    public Bookmark(string name, Vector3 position)
    {
        this.markName = name;
        this.position = position;
    }
    public string GetName()
    {
        return markName;
    }
        
        
    public Vector3 GetPosition()
    {
        return position;

    }
}