using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitcher : MonoBehaviour
{
    public Material[] materials;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // What happens when the ball collides with the color switcher
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for the "Player" tag
        if (collision.CompareTag("Player"))
        {
            // Get the "BallController" component from the ball
            BallController ballController = collision.GetComponent<BallController>();

            // If the ball has the component, switch its material
            if (ballController != null)
            {
                SwitchMaterial(ballController);
            }

            // Play the sound
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Destroy the color switcher after the sound has played
            Destroy(gameObject, audioSource.clip.length);
        }
    }

    // Switch the ball's material to a different one
    void SwitchMaterial(BallController ballController)
    {
        Material newMaterial;
        Material currentMaterial = ballController.GetCurrentMaterial();  // Get the ball's current material

        // Loop to ensure the new material is different from the current one
        do
        {
            // Pick a random material from the materials array
            newMaterial = materials[UnityEngine.Random.Range(0, materials.Length)];
        }
        while (newMaterial == currentMaterial);

        // Set the new material to the ball
        ballController.SetMaterial(newMaterial);
    }
}
