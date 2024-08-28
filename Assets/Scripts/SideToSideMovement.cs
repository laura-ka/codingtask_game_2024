using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideToSideMovement : MonoBehaviour
{
    public float moveDistance = 2.5f;    // Maximum distance the lines can move to each side
    public float moveSpeed = 2f;         // Speed of movement
    public float moveDuration = 2f;      // Duration for each movement phase (right, center, left, center)
    public float stationaryPauseDuration = 1f;  // Duration for the stationary pause between movement phases

    private Transform[] lines;
    private float[] originalXPositions;
    private int currentStaticLineIndex = 0;  // Index of the line that remains stationary during movement
    private enum MovementPhase { MoveRight, CenterFromRight, MoveLeft, CenterFromLeft }
    private MovementPhase currentPhase;
    private float moveTimer;
    private float pauseTimer;
    private bool inPause = false;

    void Start()
    {
        // Initialize arrays and store the original X positions of the lines
        lines = new Transform[transform.childCount];
        originalXPositions = new float[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            lines[i] = transform.GetChild(i);
            originalXPositions[i] = lines[i].position.x;
        }

        // Set the initial movement phase and timers
        currentPhase = MovementPhase.MoveRight;
        moveTimer = moveDuration;
        pauseTimer = stationaryPauseDuration;
    }

    void Update()
    {
        if (inPause)
        {
            // Handle the pause
            pauseTimer -= Time.deltaTime;

            if (pauseTimer <= 0)
            {
                inPause = false;
                pauseTimer = stationaryPauseDuration;

                // Update to the next static line
                currentStaticLineIndex = (currentStaticLineIndex + 1) % lines.Length;

                // Reset all lines to their original positions for the next phase
                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i].position = new Vector3(originalXPositions[i], lines[i].position.y, lines[i].position.z);
                }
            }
            return;
        }

        // Move the lines, except the current static line
        for (int i = 0; i < lines.Length; i++)
        {
            if (i != currentStaticLineIndex)
            {
                MoveLine(i);
            }
        }

        // Update the move timer
        moveTimer -= Time.deltaTime;

        // Check if it's time to switch the movement phase
        if (moveTimer <= 0)
        {
            switch (currentPhase)
            {
                case MovementPhase.MoveRight:
                    currentPhase = MovementPhase.CenterFromRight;
                    moveTimer = moveDuration;
                    break;

                case MovementPhase.CenterFromRight:
                    currentPhase = MovementPhase.MoveLeft;
                    moveTimer = moveDuration;
                    inPause = true;  // Enter pause after moving to center
                    break;

                case MovementPhase.MoveLeft:
                    currentPhase = MovementPhase.CenterFromLeft;
                    moveTimer = moveDuration;
                    break;

                case MovementPhase.CenterFromLeft:
                    currentPhase = MovementPhase.MoveRight;
                    moveTimer = moveDuration;
                    inPause = true;  // Enter pause after moving to center
                    break;
            }
        }
    }

    void MoveLine(int index)
    {
        // Determine the target position for the line based on the current movement phase
        float targetX = originalXPositions[index];
        float direction = 0;

        switch (currentPhase)
        {
            case MovementPhase.MoveRight:
                targetX += moveDistance;
                direction = 1;
                break;

            case MovementPhase.CenterFromRight:
                targetX = originalXPositions[index];
                direction = -1;
                break;

            case MovementPhase.MoveLeft:
                targetX -= moveDistance;
                direction = -1;
                break;

            case MovementPhase.CenterFromLeft:
                targetX = originalXPositions[index];
                direction = 1;
                break;
        }

        // Calculate the new X position based on the movement direction and speed
        float newX = lines[index].position.x + direction * moveSpeed * Time.deltaTime;

        // Ensure the line doesn't move beyond the specified distance
        if (Mathf.Abs(newX - originalXPositions[index]) > moveDistance)
        {
            newX = originalXPositions[index] + direction * moveDistance;
        }

        // Apply the new position to the line
        lines[index].position = new Vector3(newX, lines[index].position.y, lines[index].position.z);
    }
}