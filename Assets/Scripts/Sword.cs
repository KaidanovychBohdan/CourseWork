using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public EnemyHealth enemyStats;

    private ShowDamage showDamage;

    [SerializeField] private float _sDmg;

    private void Start()
    {
        showDamage = FindObjectOfType<ShowDamage>();
        _sDmg += enemyStats.Damage;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            collision.gameObject.GetComponent<PlayerController>().getDamage(_sDmg);
            showDamage.ShowDamageText(_sDmg.ToString(), this.transform.position, Color.red);
        }
    }
}
