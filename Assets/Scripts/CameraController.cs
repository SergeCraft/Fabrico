using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 PositionOffset = new Vector3 (0.0f, 10.0f, -15.0f); 


    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = transform.parent.transform.position + PositionOffset;
    }
}
