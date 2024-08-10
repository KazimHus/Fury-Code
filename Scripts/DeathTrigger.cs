using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine; // Ensure to include the Cinemachine namespace

public class DeathTrigger : MonoBehaviour
{
    public UIElementController victoryText; // Reference to the TextMeshProUGUI component
    public SpriteRenderer spriteRenderer;
    public CinemachineVirtualCamera virtualCamera; // Reference to the CinemachineVirtualCamera

    private void Start()
    {
        spriteRenderer.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object triggering the event is player1
        if (other.CompareTag("player1"))
        {
            StartCoroutine(ShowUIElementsAndChangeScene("player2"));
        }
        else
            StartCoroutine(ShowUIElementsAndChangeScene("player1"));

    }

    private IEnumerator ShowUIElementsAndChangeScene(string who)
    {
        // Show UI elements
        spriteRenderer.enabled = true;
        victoryText.ShowElement(); // Show the victory text

        // Find player2 by tag and set the camera to follow it
        GameObject player = GameObject.FindGameObjectWithTag(who); // Ensure player2 has the "Player2" tag
        if (player != null && virtualCamera != null)
        {
            virtualCamera.Follow = player.transform;
        }

        // Optionally, wait for a few seconds to allow the player to read the text
        yield return new WaitForSeconds(2f);

        // Change the scene
        SceneManager.LoadScene(0);
    }
}
