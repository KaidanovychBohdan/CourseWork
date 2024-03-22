using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private ShowDamage showDamage;

    [SerializeField] private PlayerController player;
    [SerializeField] private float dmg;
    [SerializeField] private float speed = 5f; 
    [SerializeField] private float gravity = -0.5f;
    private float timeToDel = 10f;

    private void Start()
    {
        showDamage = FindObjectOfType<ShowDamage>();
        player = FindObjectOfType<PlayerController>();
        dmg += player.Damage;
        Destroy(gameObject, timeToDel);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.position += Vector3.up * gravity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") 
        {
            other.gameObject.GetComponent<EnemyHealth>().getDamage(dmg);
            showDamage.ShowDamageText(dmg.ToString(), this.transform.position, Color.green);
            Destroy(this.gameObject);
        }
    }
}
