using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass3D : MonoBehaviour
{

    public GameObject tragetObject;
    
    void Update()
    {
      Vector3 target  = tragetObject.transform.position;

      Vector3 relativeTarget = transform.parent.InverseTransformPoint(target);

      float needleRotation = Mathf.Atan2(relativeTarget.x, relativeTarget.z) * Mathf.Rad2Deg;

      transform.localRotation = Quaternion.Euler(0, needleRotation, 0);
    }
}