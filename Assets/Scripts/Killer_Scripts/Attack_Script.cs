using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack_Script : MonoBehaviour
{
    private GameObject EnemyInRange;
    private RiotShield riotShield;
    public     bool canAttack = true;
    public     bool inRange = false;
    public CircleCollider2D attackCollider;
    [HideInInspector] public bool takeDamage = false;
 
    void Update()
    {
        if (Input.GetButtonDown("Attack") && canAttack)
        {
            StartCoroutine(Attack());
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy_Shield"))
        {
            inRange = true;
            EnemyInRange = collider.gameObject;
            riotShield = EnemyInRange.GetComponent<RiotShield>();
        }
    }
    private IEnumerator Attack()
    {
        canAttack = false;
        attackCollider.enabled = true;
        GetComponent<PlayerMovementKiller>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        if (inRange)
        {
            if (EnemyInRange.CompareTag("Enemy_Shield"))
                if (riotShield.stunned)
                {
                    KillEnemy();
                }  
                else
                {
                    takeDamage = true;
                    riotShield.Attack();
                }
        }
        takeDamage = false;
        GetComponent<PlayerMovementKiller>().enabled = true;
        attackCollider.enabled = false;
        yield return new WaitForSeconds(2f);                   //attack cooldown
        canAttack = true;
    }
    private void KillEnemy()
    {
        Destroy(EnemyInRange);
    }
}
