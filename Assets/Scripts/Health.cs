using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 50;

    public int GetHealth()
    {
        return health;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        DamageDealer damageDealer = col.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            health -= damageDealer.GetDamageAmount();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
