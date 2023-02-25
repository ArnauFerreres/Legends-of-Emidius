using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG 
{ 
    public class EnemyBossManager : MonoBehaviour
    {
        public string bossName;

        UIBossHealtBar bossHealthBar;
        HealthController healthController;

     private void Awake()
     {
          bossHealthBar = FindObjectOfType<UIBossHealtBar>();
          healthController = GetComponentInChildren<HealthController>();
     }
     private void Start()
     {
            bossHealthBar.SetBossName(bossName);
            bossHealthBar.SetBossMaxHealth(healthController.maxHealth);
     }
        public void UpdateBossHealthBar(int currentHealth)
        {
            bossHealthBar.SetBossCurrentHealthBar(currentHealth);
        }
   }
}