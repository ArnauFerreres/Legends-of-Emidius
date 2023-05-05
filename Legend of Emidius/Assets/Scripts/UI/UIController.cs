using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("HUD Panel Settings")]
    [SerializeField] private TextMeshProUGUI coinsTextCounter;

    int totalCoins = 0;

    public static Action onUpdateCoins;

    [SerializeField] private GameObject settingsPanel;

    public GameObject optionsPanel;
    private bool isPaused;
    public void UpdateTotalCoins()
    {
        totalCoins++;
        coinsTextCounter.text = totalCoins.ToString();

        if (totalCoins == 1)
            onUpdateCoins?.Invoke();
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        settingsPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            OpenPanel(settingsPanel);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            ClosePanel(settingsPanel);
        }
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ClosePanel(GameObject panel)
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        settingsPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void Options()
    {
        optionsPanel.SetActive(true);
    }
}
