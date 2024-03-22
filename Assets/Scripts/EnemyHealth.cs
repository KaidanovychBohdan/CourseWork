using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private float def;
    [SerializeField] private Slider slider;
    [SerializeField] private string enemyName;
    [SerializeField] private TextMeshProUGUI textName;
    
    private float maxHealth;

    public event Action<bool> isDead;

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    private void Start()
    {
        maxHealth = health;
        DisplayInfo();
    }

    public void getDamage(float Damage)
    {
        health -= Damage;
        DisplayInfo();
        if (health <= 0)
        {
            Die();
        }
    }
    private void DisplayInfo() 
    {
        float normalizedHealth = health / maxHealth;

        textName.text = enemyName;

        slider.value = normalizedHealth;
    }

    private void Die()
    {
        isDead?.Invoke(true);
        Destroy(this.gameObject);
    }
}
