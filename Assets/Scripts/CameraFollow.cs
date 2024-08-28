using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;               // Reference to the target (ball) that the camera will follow
    public float smoothTime = 0.35f;       // Time to smooth camera movement
    public float cameraYOffset = 1f;      // Vertical offset distance to maintain from the ball
    public float triggerPositionOffset = 0.1f; // The Y-offset for when the camera should start following the ball
    private Vector3 velocity = Vector3.zero;  // Velocity reference used by SmoothDamp to smoothly move the camera

    void FixedUpdate()
    {
        if (target != null)
        {
            // Calculate the Y position at which the camera should start following the target
            float triggerPositionY = transform.position.y + triggerPositionOffset;

            // Check if the target has moved above the trigger position
            if (target.position.y > triggerPositionY)
            {
                // Calculate the target position for the camera based on the target's position and the offset
                Vector3 targetPosition = new Vector3(transform.position.x, target.position.y + cameraYOffset, transform.position.z);

                // Smoothly move the camera towards the target position using SmoothDamp
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            }
        }
    }
}
