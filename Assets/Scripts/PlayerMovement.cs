using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    //horizontal variables
    [SerializeField] private float HorizontalForce = 7f;
    private float dirX;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {

        //flipping sprite and getting horizontal axis
        Vector2 characterScale = transform.localScale;

        dirX = Input.GetAxisRaw("Horizontal");
        if (dirX < 0 && transform.localScale.x > 0)
            characterScale.x = transform.localScale.x * -1f;
        if (dirX > 0 && transform.localScale.x < 0)
            characterScale.x = transform.localScale.x * -1f;
        transform.localScale = characterScale;

        animator.SetFloat("Speed", Mathf.Abs(dirX));
      

    }
    private void FixedUpdate()
    {
        // x axis movement
        rb.velocity = new Vector2(dirX * HorizontalForce, rb.velocity.y);
    }
}
