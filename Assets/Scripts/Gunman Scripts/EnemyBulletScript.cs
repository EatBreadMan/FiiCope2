using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private GameObject player;
    private EnemyShoot IgnoredColliders;
    [SerializeField] private float bulletSpeed;
    private Rigidbody2D rb;
    private float timer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;

        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != null)
        {
            if (other.gameObject.CompareTag("Player")) 
                Destroy(player);
            Destroy(gameObject);
        }
            
    }
}
