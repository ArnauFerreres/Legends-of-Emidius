using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PassLevel : MonoBehaviour
{
    public string passLevel;
    public TextMeshProUGUI textProgress;
    public Slider sliderProgress;
    public float currentPercent;
    public Image PanelLoading;

    private void Start()
    {
        //gameObject.SetActive(false);
        PanelLoading.enabled = false;
        sliderProgress.enabled = false;
        textProgress.enabled = false;
    }

    public IEnumerator LoadScene(string nameToLoad)
    {
        textProgress.text = "Loading... 00%";
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(nameToLoad);

        while (!loadAsync.isDone)
        {
            currentPercent = loadAsync.progress * 100 / 0.9f;
            textProgress.text = "Loading... " + currentPercent.ToString("00") + "%";
            yield return null;
        }
    }

    public void Update()
    {
        sliderProgress.value = Mathf.MoveTowards(sliderProgress.value, currentPercent, 10 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PanelLoading.enabled = true;
            sliderProgress.enabled = true;
            textProgress.enabled = true;
            StartCoroutine(LoadScene(passLevel));
            //SceneManager.LoadScene(passLevel);
        }
    }
}