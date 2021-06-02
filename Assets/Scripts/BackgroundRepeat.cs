using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeat : MonoBehaviour
{
    public Vector3 initialPosition = new Vector3(50, 9.5f, 4);
    public float repeatPosition = -6.0f;

    void Update()
    {
        if(transform.position.x < repeatPosition)
        {
            transform.position = initialPosition;
        }
    }
}