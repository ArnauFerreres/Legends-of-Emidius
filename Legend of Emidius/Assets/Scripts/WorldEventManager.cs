using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WorldEventManager : MonoBehaviour
    {
        public List<FogWall> fogWalls;
        public UIBossHealtBar bossHealtBar;
        public EnemyBossManager boss;


        //public TextFade bossHealthBarFade;

        public bool bossFightIsActive; // Is correctly fighting boss
        public bool bossHasBeenAwakened; // Woke the boss/watched cutscene but died during fight
        public bool bossHasBeenDefeated; // Boss has been defeated
        private void Start()
        {
            //GetComponent<AudioSource>().clip = bossMusic;
            // bossHealthBarFade= GetComponent<TextFade>();
        }
        private void Awake()
        {
            bossHealtBar = FindObjectOfType<UIBossHealtBar>();
        }
        public void Update()
        {
            if(boss == null)
            {
                BossHasBeenDefeated();
            }
        }
        public void ActiveBossFight()
        {
            bossFightIsActive = true;
            bossHasBeenAwakened= true;
            bossHealtBar.SetUIHealthBarToActive();
            bossHealtBar.SetMusicBossToActive();
            foreach (var fogWall in fogWalls)
            {
                fogWall.ActivateFogWall();
            }
        }
        public void BossHasBeenDefeated()
        {
            bossHasBeenDefeated = true;
            bossFightIsActive = false;
            foreach (var fogWall in fogWalls)
            {
                fogWall.DeactivateFogWall();
            }
            //bossHealthBarFade.FadeOut();
            bossHealtBar.SetMusicBossToInactive();
            Invoke("HPBoss", 3f);
        }

        public void HPBoss()
        {
            bossHealtBar.SetUIHealthBarToInactive();
        }
    }


}
