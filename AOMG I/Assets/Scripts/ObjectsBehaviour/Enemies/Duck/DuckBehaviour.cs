using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckBehaviour : MonoBehaviour
{
    [Header ("Collision Layers")]
    [SerializeField] private LayerMask terrainLayer;

    [Header ("Player's Position")]
    [SerializeField] private Transform playerTransform;

    [Header ("Territory")]
    [SerializeField] private float topYPosition;
    [SerializeField] private float bottomYPosition;
    [SerializeField] private float rightXPosition;
    [SerializeField] private float leftXPosition;

    [Header ("Duck's Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private EnemyTopHit enemyTopHit;
    [SerializeField] private BoxCollider2D damagePartBoxCollider2D;
    [SerializeField] private BoxCollider2D topHitBoxCollider2D;

    private Rigidbody2D body;
    private Animator ani;

    private float timer = 100;
    private bool playerInTerritory = false;

    private void Awake() 
    {
        this.body = GetComponent<Rigidbody2D>();
        this.ani = GetComponent<Animator>();
    }

    private void FixedUpdate() 
    {
        if ( !this.enemyTopHit.isHit )
        {
            if ( (this.playerTransform.position.x <= this.rightXPosition) && (this.playerTransform.position.x >= this.leftXPosition) && (this.playerTransform.position.y <= this.topYPosition) && (this.playerTransform.position.y >= this.bottomYPosition) )
                this.playerInTerritory = true;
            else
                this.playerInTerritory = false;
            
            if ( this.timer >= this.jumpCooldown )
            {
                if ( this.playerInTerritory )
                {
                    this.ani.SetBool("Idle", true);
                    this.body.velocity = new Vector2(0, this.body.velocity.y);
                }
                else
                {
                    this.ani.SetBool("Idle", false);
                    this.ani.SetTrigger("JumpAnticipation");
                }
            }
            else
            {
                this.timer += Time.fixedDeltaTime;
            }

            this.ani.SetBool("Idle", this.isGrounded());

            if ( this.isGrounded() )
                if ( ((this.transform.position.x <= (this.leftXPosition + 1.705)) && ( this.transform.localScale.x > 0)) || ((this.transform.position.x >= (this.rightXPosition - 1.705)) && ( this.transform.localScale.x < 0)) )
                    this.transform.localScale = new Vector3(-1 * this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
        }
        else
        {
            this.topHitBoxCollider2D.enabled = false;
            this.damagePartBoxCollider2D.enabled = false;

            this.body.gravityScale = 2.5f;
        }
    }

    private void DuckJump()
    {
        this.timer = 0;
        this.ani.SetTrigger("Jump");
        this.body.velocity = new Vector2(-1 * this.speed * Mathf.Sign(this.transform.localScale.x), this.jumpPower);
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(this.damagePartBoxCollider2D.bounds.center, this.damagePartBoxCollider2D.bounds.size, 0, new Vector2(0, -1), 0.1f, this.terrainLayer);
        return raycastHit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D _collision) 
    {
        if ( _collision.gameObject.tag == "Player" )
            StartCoroutine(this.HitBehaviour());
    }

    private IEnumerator HitBehaviour()
    {
        if ( this.enemyTopHit.isHit )
        {
            this.ani.SetTrigger("Hit");
            yield return new WaitForSeconds(1.5f);
            this.gameObject.SetActive(false);
        }
    }
}
