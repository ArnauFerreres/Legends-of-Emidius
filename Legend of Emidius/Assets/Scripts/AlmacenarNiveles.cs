using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlmacenarNiveles : MonoBehaviour
{
    private int nivel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CargarNivel()
    {
        nivel = PlayerPrefs.GetInt("nivelX");

        if(nivel == 1)
        {
            SceneManager.LoadScene("FirstLevel");
        }
        if (nivel == 2)
        {
            SceneManager.LoadScene("CastilloInterior");
        }
        if (nivel == 3)
        {
            SceneManager.LoadScene("CastilloExterior");
        }
        if (nivel == 4)
        {
            SceneManager.LoadScene("Forest");
        }
        if (nivel == 5)
        {
            SceneManager.LoadScene("Desierto");
        }
        if (nivel == 6)
        {
            SceneManager.LoadScene("Dungeon");
        }
    }
}
