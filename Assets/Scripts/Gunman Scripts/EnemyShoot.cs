using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private float timer;
    public Animator animator;
    public GameObject player;
    public CircleCollider2D PlayerAttackCollider;
    [SerializeField] private float gunRange = 4f;
    [SerializeField] private float reloadTime = 2f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= gunRange)
        {
            //flip sprite to face player
            Vector2 GunmanScale = transform.localScale;
            if(player.transform.position.x < transform.position.x)
                GunmanScale.x = -1f * Mathf.Abs(transform.localScale.x);
            if(player.transform.position.x > transform.position.x)
                GunmanScale.x = Mathf.Abs(transform.localScale.x);
            transform.localScale = GunmanScale;
            timer += Time.deltaTime;
            animator.SetBool("inRange", true);
            if (timer > reloadTime)
            {
                //animator.SetBool("isShooting", true);
                timer = 0;
                Shoot();
                //animator.SetBool("isShooting", false);
            }
        }
        else animator.SetBool("inRange", false);
        
    }
    void Shoot()
    {
        
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        StartCoroutine(shootAnim());
        animator.SetBool("ShootBool", false);
    }
    private IEnumerator shootAnim()
    {
        animator.SetTrigger("Shoot");
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("ShootBool", true);
    }
}
