using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiotShield : MonoBehaviour
{
    private PlayerMovementKiller playerMovement;
    private Attack_Script playerAttack;
    public GameObject player;
    public bool stunned = false;
    private void Awake()
    {
        playerMovement = player.GetComponent<PlayerMovementKiller>();
        playerAttack = player.GetComponent<Attack_Script>();
    }
    private void Update()
    {
        if (playerAttack.takeDamage)
            Attack();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player_Dash") && playerMovement.isDashing)
            StartCoroutine(Stun());
    }
    private IEnumerator Stun()
    {
        stunned = true;
        yield return new WaitForSeconds(2f);
        stunned = false;
    }
    public void Attack()
    {
        Destroy(player);
    }
}
