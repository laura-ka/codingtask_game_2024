using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text restartText;  
    public TMP_Text starCountText; 
    public AudioClip defeatSound;
    public GameObject darkOverlay; 
    private AudioSource audioSource;

    void Start()
    {
        restartText.gameObject.SetActive(false); // Make sure the text is not visible at the start
        starCountText.gameObject.SetActive(true);  // Make sure star count is visible
        UpdateStarCount(0);  // Initialize star count display
        audioSource = GetComponent<AudioSource>();
    }

    // Display the restart text and play the sound
    public void ShowRestartText()
    {
        restartText.gameObject.SetActive(true);

        // Enable the dark overlay
        if (darkOverlay != null)
        {
            darkOverlay.SetActive(true);
        }

        if (defeatSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(defeatSound);
        }
    }

    // Update the star count display in the UI
    public void UpdateStarCount(int starCount)
    {
        if (starCountText != null)
        {
            starCountText.text = starCount.ToString(); 
        }
    }


    void Update()
    {
        // Restart the game only when the restart text is visible and screen is tapped or clicked
        if (restartText.gameObject.activeInHierarchy && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
        {
            RestartGame();
        }
    }

    // Restart the game by reloading the current scene
    private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
  }
