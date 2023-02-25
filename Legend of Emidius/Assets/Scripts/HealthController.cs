using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public enum characterType { Player, Enemy01 }

    [Header("Health Settings")]
    public characterType currentCharacterType = characterType.Player;
    public bool friendlyFire = false;
    [SerializeField] private Image hpBar;

    [Header("Stats Settings")]
    [SerializeField] public int maxHealth;
    public int currentHealth;

    PlayerController playerController;

    CharacterController controller;
    EnemyBossManager enemyBossManager;
    public bool isBoss;

    private Animator animator;
    private void Awake()
    {
        enemyBossManager= GetComponent<EnemyBossManager>();
    }
    void Start()
    {
        if(!isBoss)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth = 100;
        }
        
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }
    public void TakeDamage(int damage, string tag)
    {
        if (gameObject.tag != tag && !friendlyFire)
            return;

        currentHealth -= damage;

        if(hpBar != null)
        {
            HPBarUpdate();
        }
        if(!isBoss)
        {
            HPBarUpdate();
        }
        else if (isBoss && enemyBossManager!= null)
        {
            enemyBossManager.UpdateBossHealthBar(currentHealth);
        }
        if (currentHealth <= 0)
        {
            currentHealth = 0;

            if (currentCharacterType == characterType.Player)
            {
                controller.enabled = false;
                controller.transform.rotation= Quaternion.identity;
                animator.SetBool("dead", true);
                
                //return;
            }

            if (currentCharacterType == characterType.Enemy01)
            {
                Destroy(gameObject);
            }

        }
    }
    public void HPBarUpdate()
    {
        hpBar.fillAmount = (float)currentHealth / maxHealth;
    }
}
