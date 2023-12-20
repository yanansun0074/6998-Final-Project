using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookmark : MonoBehaviour
{
    public string markName;
    public Vector3 position;
    public GameObject markPin;
    public bool selected;
    // Miniature pointer
    public GameObject planeNormal;
    public GameObject planeHighlighted; 
    // World pointer
    public GameObject plane2Normal;
    public GameObject plane2Highlighted; 

    protected Vector3 miniaturePos;
    


    
    public Bookmark(string name, Vector3 position, GameObject pin)
    {
        this.markName = name;
        this.position = position;
        this.markPin = pin;
    }

    void Start()
    {
        selected = false;
        // ChangeAlpha();
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
        // if below the surface, set the miniature map above the ground
        if (position[1] <0) {miniaturePos = new Vector3(position[0], -position[1], position[2]);}
        else {miniaturePos = position;}
        planeNormal.transform.position = miniaturePos;
        planeHighlighted.transform.position = miniaturePos;

        // set the world map coordinate
        plane2Normal.transform.position = position;
        plane2Highlighted.transform.position = position;
    }

    public void SetName(string name)
    {
        this.markName = name;
    }

    public void SetPin(GameObject pin)
    {
        this.markPin = pin;
    }

    public void ChangeAlpha()
    {
        // GameObject Plane = (GameObject) this.transform.Find("Sketchfab_model/352c2b73c9204e899b109cb3440e44dc.fbx/RootNode/Plane/Plane_Outsides_0");
        // Material material = Plane.GetComponent<Renderer>().materials[0];
        // Material newMaterial = planeNormal.GetComponent<Renderer>().material;
        // float Alpha = 0.7f/20 * position[2] + 0.3f;
        // Color color = newMaterial.color;
        Material newMaterial = new Material(Shader.Find("Lit"));
        newMaterial.SetColor("_color", new Color(0.1f, 0.9f, 0.1f, 0.4f));
        planeNormal.GetComponent<Renderer>().material = newMaterial;
        // plane.GetComponent<Renderer>().materials[0] = null;
        // Plane.GetComponent<Renderer>().material.SetColor("_color", new Color(color[0], color[1], color[2], Alpha));
    }

    void Update()
    {   
        // Change different color based on whether selected
        if (selected)
        {
            planeHighlighted.SetActive(true);
            planeNormal.SetActive(false);
            plane2Highlighted.SetActive(true);
            plane2Normal.SetActive(false);
        }
        else 
        {
            planeHighlighted.SetActive(false);
            planeNormal.SetActive(true);
            plane2Highlighted.SetActive(false);
            plane2Normal.SetActive(true);
        }
        
    }

}