using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniatureCameraScript : MonoBehaviour
{
    public Transform Player; // Camera
    public bool overview;

    // Start is called before the first frame update
    void Start()
    {
        overview = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!overview) {transform.position = new Vector3(Player.position[0], 15, Player.position[2]);}
        else {transform.position = new Vector3(5.98f, 30, 6.57f);}
    }
}
