using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTableController : MonoBehaviour
{
    [System.Serializable]
    public class LootDrop
    {
        public GameObject item;
        public int weight;
    }

    public List<LootDrop> loot;

    public GameObject SpawnDrop()
    {
        int roll = Random.Range(1, 101);
        int weightSum = 0;

        foreach (LootDrop item in loot)
        {
            weightSum += item.weight;

            if (roll < weightSum)
            {
                return item.item;
            }
        }
        return null;
    }

    private void GetDrop(GameObject enemy)
    {
        GameObject item = SpawnDrop();

        if (item != null)
        {
            Instantiate(item, enemy.transform.position + new Vector3(0, 0.35f, 0), Quaternion.identity);
        }
    }

    private void OnEnable()
    {
        HealthController.onEnemyDead += GetDrop;
    }
    private void OnDisable()
    {
        HealthController.onEnemyDead -= GetDrop;
    }
}
