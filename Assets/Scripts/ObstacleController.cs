using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public Material[] materials; 
    public SpriteRenderer[] segments;  

    void Start()
    {
        // Check if the number of materials matches the number of segments
        if (segments.Length == materials.Length)
        {
            // Loop through each segment and assign the corresponding material
            for (int i = 0; i < segments.Length; i++)
            {
            // Assign material to segment
                segments[i].material = materials[i];
            }
        }
    }
}