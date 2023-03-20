using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementKiller : MonoBehaviour
{
    private Rigidbody2D rb;

    //horizontal variables
    [SerializeField] float HorizontalForce = 10f;
    private float dirX;

    //vertical variables
    [SerializeField] float VerticalForce = 15f;
    bool wantJump = false;
    public BoxCollider2D PlayerBoxCollider;
    private float dirY;

    //dash variables
    private bool canDash = true;
    [HideInInspector] public bool isDashing = false;
    [SerializeField] private float dashingPowerHorizontal = 24f;
    [SerializeField] private float dashingPowerVertical = 12f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 2f;
    public BoxCollider2D dashCollider;

    //layermask for walkable terrain
    [SerializeField] private LayerMask walkableLayerMask;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private bool IsGrounded()
    {

        float extraHeightText = 0.05f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(PlayerBoxCollider.bounds.center, PlayerBoxCollider.bounds.size, 0f, Vector2.down, extraHeightText, walkableLayerMask);   //Raycast to determine if walkable ground is below the feet
        return raycastHit.collider != null;

    }
    private void Update()
    {

        //can't do anything while dashing
        if (isDashing)
            return;

        //flipping sprite and getting horizontal axis
        Vector2 characterScale = transform.localScale;

        dirX = Input.GetAxisRaw("Horizontal");
        if (dirX < 0 && transform.localScale.x > 0)
            characterScale.x = transform.localScale.x * -1f;
        if (dirX > 0 && transform.localScale.x < 0)
            characterScale.x = transform.localScale.x * -1f;
        transform.localScale = characterScale;

        //getting vertical axis
        dirY = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
            wantJump = true;

        if (Input.GetButtonDown("Dash") && canDash)
            StartCoroutine(Dash());
    }
    private void FixedUpdate()
    {
        //can't do anything while dashing
        if (isDashing)
            return;

        //jumping
        if (wantJump == true)
        {
            if (IsGrounded())
                rb.velocity = new Vector2(rb.velocity.x, VerticalForce);
            wantJump = false;
        }
        // x axis movement
        rb.velocity = new Vector2(dirX * HorizontalForce, rb.velocity.y);
    }
    private IEnumerator Dash()   //dashing coroutine
    {
        isDashing = true;
        dashCollider.enabled = true;
        canDash = false;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        PlayerBoxCollider.enabled = false;
        rb.velocity = new Vector2(Mathf.Abs(transform.localScale.x) * dashingPowerHorizontal * dirX, transform.localScale.y * dashingPowerVertical * dirY);   //the actual dash

        yield return new WaitForSeconds(dashingTime);  
        
        PlayerBoxCollider.enabled = true;
        rb.gravityScale = originalGravity;
        isDashing = false;
        dashCollider.enabled = false;

        yield return new WaitForSeconds(dashingCooldown);          //dash cooldown
        canDash = true;
    }
}
