using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateControl : MonoBehaviour
{
    public float rotateSpeed;
    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0);
    }
}
