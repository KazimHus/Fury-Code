using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab; 
    public GameObject healthTextPrefab;
    public Canvas gameCanvas; 

    public Button button; // This is correct for the button

    public TextMeshProUGUI text; // Use TextMeshProUGUI if using TextMeshPro


    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
        button.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        
    }

    public void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;

    }

    public void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed;

    }

    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
        .GetComponent<TMP_Text>();


        tmpText.text = damageReceived.ToString();

    }

    public void CharacterHealed(GameObject character, int healthRestored)
    {
         Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
        .GetComponent<TMP_Text>();


        tmpText.text = healthRestored.ToString();

    }

    public void OnExit(InputAction.CallbackContext context)
    {
    if (context.performed)
    {
        // Toggle the visibility of the button and text
        bool isActive = button.gameObject.activeSelf;
        button.gameObject.SetActive(!isActive);
        text.gameObject.SetActive(!isActive);
    }
    }

}
