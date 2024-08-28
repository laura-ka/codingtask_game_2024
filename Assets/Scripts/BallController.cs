using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public float jumpForce = 6.5f;  // The ball's jumping force
    private Rigidbody2D rb;
    private Material currentMaterial;
    public UIManager uiManager;  // UIManager for updating UI elements
    public Material[] materials;  // Add materials in the inspector
    public GameObject starCollectedPrefab;  // Prefab for the star's particle effect
    private int starCount = 0;

    // Audio sources and clips
    public AudioSource src1;  
    public AudioSource src2;  
    public AudioClip snd1;  
    public AudioClip snd2;   


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        SetRandomMaterial();
    }

    void Update()
    {
        // Check if the player has tapped or clicked the screen
        if (Input.GetMouseButtonDown(0))
        {
        // Make the ball jump and play the jump sound
            Jump();
            src1.clip = snd1;
            src1.Play();
        }

        // If the ball falls below the screen, destroy the ball
        if (transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize)
        {
            DestroyBall();
        }
    }

    // How the ball jumps
    void Jump()
    {
        // Apply an upward force to the ball's rigidbody to simulate a jump
        rb.velocity = Vector2.up * jumpForce;
    }

    // What happens when the ball collides with another object
    void OnTriggerEnter2D(Collider2D other)
    {
        // If it collides with a star
        if (other.CompareTag("Star"))
        {
            // Collect the star and play the sound
            CollectStar(other.gameObject);
            src2.clip = snd2;
            src2.Play();
        }
        // If it collides with an obstacle
        else if (other.CompareTag("Obstacle"))
        {
            // Handle the collision with the obstacle
            HandleObstacleCollision(other);
        }
    }

    // Handle the ball's collision with an obstacle
    void HandleObstacleCollision(Collider2D other)
    {
        SpriteRenderer ballRenderer = spriteRenderer;
        SpriteRenderer segmentRenderer = other.GetComponent<SpriteRenderer>();

        if (ballRenderer != null && segmentRenderer != null)
        {
            // Get the materials of the ball and the segment
            Material ballMaterial = ballRenderer.material;
            Material segmentMaterial = segmentRenderer.material;

            // Check if the ball's material matches the segment's material
            if (ballMaterial.color == segmentMaterial.color)
            {
                UnityEngine.Debug.Log("Ball passed through, colors match");
            }
            else
            {
                // Destroy the ball if the colors don't match
                DestroyBall();
            }
        }
    }

    // Assign a random material to the ball
    void SetRandomMaterial()
    {
        // Check if there are any materials in the array
        if (materials.Length > 0)
        {
            // Select a random material and assign it to the ball
            int index = UnityEngine.Random.Range(0, materials.Length);
            currentMaterial = materials[index];
            spriteRenderer.material = currentMaterial;
        }
    }


    // Change the ball's material
    public void SetMaterial(Material newMaterial)
    {
        // Update the current material and apply it to the ball
        currentMaterial = newMaterial;
        spriteRenderer.material = newMaterial;
    }

    // Get the ball's current material
    public Material GetCurrentMaterial()
    {
        return currentMaterial;
    }

    // Handle star collection
    void CollectStar(GameObject star)
    {
        // Instantiate the particle effect at the star's position
        Instantiate(starCollectedPrefab, star.transform.position, Quaternion.identity);

        // Destroy the star object
        Destroy(star);

        // Increment the star count
        starCount++;

        // Update the UI to show the new star count
        if (uiManager != null)
        {
            uiManager.UpdateStarCount(starCount);
        }
    }

    // Handle the ball's destruction
    void DestroyBall()
    {
        // Instantiate the particle effect and destroy the ball object
        Instantiate(Resources.Load("PlayerDeath"), transform.position, Quaternion.identity);
        Destroy(gameObject);
        UnityEngine.Debug.Log("Ball Destroyed");

        // Show the text on the UI
        if (uiManager != null)
        {
            uiManager.ShowRestartText();
        }
    }
}
