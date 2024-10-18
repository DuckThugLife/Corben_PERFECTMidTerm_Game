using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Windows;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    private PlayerInput input;

    [Header("UI Element")]
    public TMP_Text txtHealth;
    public GameObject gameOver;
    public GameObject settingsMenu;

    [Header("Settings Sliders")]
    public Slider mouseSens;
    public Slider musicVolume;

    

    void Start()
    {
        input = PlayerInput.GetInstance();
        SetUpSettingsSliders();

        gameOver.SetActive(false);
    }

    private void Update()
    {
        if (input != null)
        {
            if (input.escape) // if the player hits escape
            {
                SettingsMenu();
            }
        }
    }

    private void OnEnable()
    {
        playerHealth.OnHealthUpdated += OnHealthUpdated;
    }

    private void OnDestroy()
    {
        playerHealth.OnHealthUpdated -= OnHealthUpdated;
    }

    void OnHealthUpdated(float health)
    {
        txtHealth.text = "Health: " + Mathf.Floor(health).ToString();
    }

    void OnDeath()
    {
        // Currently using the respawn system where the player doesn't actually "Die" just gets teleported to a checkpoint, so currently unused.
        //gameOver.SetActive(true);
    }

    public void GameEnd()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        gameOver.SetActive(true);
    }

    private void SettingsMenu()
    {
        
        settingsMenu.SetActive(!settingsMenu.activeSelf);
        
        if (settingsMenu.activeSelf == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            DisableCursor();
        }

    }
    private void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void SetUpSettingsSliders()
    {
        mouseSens.value = input.mouseSensitivity;
        musicVolume.value = 1; // Sound is currently not saving so just making it a base amount
        // 3
    }

    
}
