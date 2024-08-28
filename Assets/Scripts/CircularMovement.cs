using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{

    public float rotationSpeed = 50f;  // Speed of the rotation in degrees per second

    void Update()
    {
        // Rotate the object around its Z axis
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
