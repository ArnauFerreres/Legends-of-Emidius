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
    [SerializeField] private int maxHealth;
    int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
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

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            if (currentCharacterType == characterType.Player)
            {
                //restart game, game over
                return;
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
