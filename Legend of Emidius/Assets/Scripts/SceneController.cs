using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string menuScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void options()
    {
        SceneManager.LoadScene(menuScene);
    }
    public void newgame()
    {
        PlayerPrefs.SetInt("nivelX", 1);
        SceneManager.LoadScene("FirstLevel");
    }
    public void volume()
    {
        SceneManager.LoadScene("Volume");
    }
    public void credit()
    {
        SceneManager.LoadScene("Credits");
    }
    public void Screen()
    {
        SceneManager.LoadScene("Screen");
    }
    public void Language()
    {
        SceneManager.LoadScene("Language");
    }
    public void Controles()
    {
        SceneManager.LoadScene("Controles");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void exit()
    {
        Application.Quit();
    }
}
