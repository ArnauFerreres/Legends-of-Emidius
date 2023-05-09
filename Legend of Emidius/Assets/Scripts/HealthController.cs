using SG;
using Suntail;
using System;
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
    public int bossHealth;
    public ParticleSystem particulasMuerte;

    private float alturaOffset = 1.0f;

    [Header("Regenerate Settings")]
    [SerializeField] private int regenerate = 40;
    public static Action<GameObject> onEnemyDead;


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
            currentHealth = bossHealth;
        }
        
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        float finalLife = (float)currentHealth / maxHealth;

        hpBar.fillAmount = Mathf.MoveTowards(hpBar.fillAmount, finalLife, 1f * Time.deltaTime);
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
                playerController.Dead();
                controller.transform.rotation= Quaternion.identity;
                animator.SetBool("dead", true);

                Invoke("ActivarPanelGO", 1.6f);

                //Haecr que cuando mueras te lleve a los check points despues de activar el panel

                return;
            }

            if (currentCharacterType == characterType.Enemy01)
            {
                //Vector3 posicionParticula = particulasMuerte.transform.position;
                //posicionParticula.y += alturaOffset;

                onEnemyDead?.Invoke(gameObject);
                Destroy(gameObject);
                Instantiate(particulasMuerte, transform.position, transform.rotation);

                //ParticleSystem particulaInstanciada = Instantiate(particulasMuerte, posicionParticula, Quaternion.identity);
                
            }

        }
    }

    public void TakeHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += regenerate;
        }
    }

    public void HPBarUpdate()
    {
        hpBar.fillAmount = (float)currentHealth / maxHealth;
    }

    void ActivarPanelGO()
    {
        Cursor.lockState = CursorLockMode.None;
        playerController.GameOverPanel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeadZone")
        {
            currentHealth -= 10;
        }
    }
}
