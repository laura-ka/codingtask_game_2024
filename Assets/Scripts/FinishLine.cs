using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    public GameObject playAgainText;
    public GameObject darkOverlay;
    private AudioSource audioSource;

    void Start()
    {
        // Make sure the text is inactive at the start of the game
        playAgainText.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding with the finish line has the "Player" tag
        if (other.CompareTag("Player"))
        {

            playAgainText.SetActive(true);

            // Enable the dark overlay
            if (darkOverlay != null)
            {
                darkOverlay.SetActive(true);
            }

            // Disable the "BallController" script to stop the ball's movement
            other.GetComponent<BallController>().enabled = false;

            // Freeze the ball's position
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;      // Stop any current movement
            rb.isKinematic = true;           // Make the rigidbody unaffected by physics

            // Start the coroutine that waits for player input to restart the game
            StartCoroutine(WaitForRestart());
        }

        // Play the sound
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    // Coroutine that waits for the player to click or tap the screen to restart the game
    IEnumerator WaitForRestart()
    {
        while (true)
        {
            // If the player taps or clicks the screen
            if (Input.GetMouseButtonDown(0))
            {
                // Reload the current scene, restart the game
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            }
            yield return null;  // Wait until the next frame before continuing the loop
        }
    }
}
