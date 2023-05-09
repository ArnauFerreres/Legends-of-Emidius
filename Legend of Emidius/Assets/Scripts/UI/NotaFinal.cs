using Cinemachine;
using RPGCharacterAnims;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NotaFinal : MonoBehaviour
{
    public GameObject notaFinal;
    public GameObject virtualCamera; // Referencia a la virtual camera en la escena
     // Referencia a la cámara en la escena
    UIController pause;
    // Start is called before the first frame update
    void Start()
    {
        pause = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            notaFinal.SetActive(true);
            Time.timeScale = 0f;
            pause.isPaused = true;
            OnPauseMenuOpen();
            Cursor.lockState = CursorLockMode.None;
            

        }
    }



    private void OnPauseMenuOpen()
    {
        // Desactivar el componente "CinemachineBrain" de la virtual camera
        CinemachineBrain cinemachineBrain = virtualCamera.GetComponent<CinemachineBrain>();
        if (cinemachineBrain != null)
        {
            cinemachineBrain.enabled = false;
        }
    }

    private void OnPauseMenuClose()
    {
        // Volver a activar el componente "CinemachineBrain" de la virtual camera
        CinemachineBrain cinemachineBrain = virtualCamera.GetComponent<CinemachineBrain>();
        if (cinemachineBrain != null)
        {
            cinemachineBrain.enabled = true;
        }
    }

    public void CloseNote(GameObject panel)
    {
        Time.timeScale = 1.0f;
        pause.isPaused = false;
        OnPauseMenuClose();
        panel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
