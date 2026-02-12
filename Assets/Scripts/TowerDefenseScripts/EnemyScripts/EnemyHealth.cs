using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int healPoints = 2;
    private StateManager SanityValue;

    void Start()
    {
        GameObject stateManager = GameObject.FindWithTag("HealthBar");
         SanityValue = stateManager.GetComponent<StateManager>();
    }

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        if(hitPoints <= 0)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
            SanityValue.HealSanity(healPoints);
        }
    }
}
