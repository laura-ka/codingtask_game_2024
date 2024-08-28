using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    public Vector3 minScale = new Vector3(0.1f, 0.1f, 0.1f); // Minimum scale value
    public Vector3 maxScale = new Vector3(1f, 1f, 1f);       // Maximum scale value
    public float speed = 3f;                                  // Speed of scaling

    private Vector3 scaleRange;   // The range between min and max scale
    private float time;   // Timer to control the scaling animation

    void Start()
    {
        // Calculate the range between min and max scale
        scaleRange = maxScale - minScale;
    }

    void Update()
    {
        // Increment time by the speed factor
        time += Time.deltaTime * speed;

        // Calculate a scaling factor using a sine wave function
        float scaleFactor = Mathf.Sin(time) * 0.5f + 0.5f; // Normalize sine wave between 0 and 1

        // Calculate the new scale based on the scaling factor
        transform.localScale = minScale + scaleRange * scaleFactor;
    }
}
