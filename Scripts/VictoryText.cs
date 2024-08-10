using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIElementController : MonoBehaviour
{
    private GameObject uiElement; 

    private void Awake()
    {
        uiElement = gameObject; // Get the GameObject this script is attached to
        uiElement.SetActive(false); // Hide the UI element initially
    }

    public void ShowElement()
    {
        
        uiElement.SetActive(true); // Show the UI element
    }

    public void HideElement()
    {
        uiElement.SetActive(false); // Hide the UI element
    }
}