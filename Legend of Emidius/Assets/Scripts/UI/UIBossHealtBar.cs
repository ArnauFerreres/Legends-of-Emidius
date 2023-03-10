using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SG 
{ 
    public class UIBossHealtBar : MonoBehaviour
    {
        public TextMeshProUGUI bossName;
        public Slider slider;
        public AudioClip bossMusic;

        private void Awake()
        {
            slider = GetComponentInChildren<Slider>();
            GetComponent<AudioSource>().clip = bossMusic;
            //bossName = GetComponentInChildren<TextMeshProUGUI>();
        }
        private void Start()
        {
            SetUIHealthBarToInactive();
        }
        private void Update()
        {
            //if(Input.GetKey(KeyCode.T))
            //{
            //    SetUIHealthBarToActive();
            //}


        }
        public void SetBossName(string name)
        {
            bossName.text = name;
        }
        public void SetUIHealthBarToActive()
        {
            slider.gameObject.SetActive(true);
        }
        public void SetUIHealthBarToInactive()
        {
            slider.gameObject.SetActive(false);
        }
        public void SetBossMaxHealth(int maxHealth)
        {
            slider.maxValue= maxHealth;
            slider.value= maxHealth;
        }
        public void SetBossCurrentHealthBar(int currentHealth)
        {
            slider.value = currentHealth;
        }
        public void SetMusicBossToActive()
        {
            GetComponent<AudioSource>().Play();
        }
        public void SetMusicBossToInactive()
        {
            GetComponent<AudioSource>().Stop();
        }
    }
}