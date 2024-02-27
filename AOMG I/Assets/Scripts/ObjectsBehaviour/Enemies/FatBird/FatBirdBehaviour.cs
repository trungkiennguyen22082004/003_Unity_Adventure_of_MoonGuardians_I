using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBirdBehaviour : MonoBehaviour
{
    [Header ("Player's Position")]
    [SerializeField] private Transform playerTransform;

    [Header ("Territory")]
    [SerializeField] private float topYPosition;
    [SerializeField] private float bottomYPosition;
    [SerializeField] private float rightXPosition;
    [SerializeField] private float leftXPosition;

    [Header ("Fat-Bird's Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private EnemyTopHit enemyTopHit;
    [SerializeField] private BoxCollider2D damagePartBoxCollider2D;
    [SerializeField] private BoxCollider2D topHitBoxCollider2D;

    private Rigidbody2D body;
    private Animator ani;
    private BoxCollider2D boxCollider2D;

    private bool playerInTerritory = false;
    private bool isFalling = false;

    private void Awake() 
    {
        this.body = GetComponent<Rigidbody2D>();
        this.ani = GetComponent<Animator>();
        this.boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate() 
    {

        if ( !this.enemyTopHit.isHit )
        {
            if ( (this.playerTransform.position.x <= this.rightXPosition) && (this.playerTransform.position.x >= this.leftXPosition) )
                this.playerInTerritory = true;
            else
                this.playerInTerritory = false;

            if ( this.isFalling )
            {
                this.boxCollider2D.enabled = false;
                this.ani.SetBool("Idle", false);
                this.body.gravityScale = 2f * Math.Abs(this.speed);
            }
            else
            {
                this.ani.SetBool("Idle", true);
                this.body.gravityScale = 0;

                if ( (this.transform.position.y > this.topYPosition) || (this.transform.position.y < this.bottomYPosition) )
                {
                    this.body.velocity = new Vector2( this.body.velocity.x, Mathf.Sign((((this.topYPosition + this.bottomYPosition) / 2) - this.transform.position.y) * Math.Abs(this.speed)) );
                }
                else
                {
                    this.boxCollider2D.enabled = true;
                    this.body.velocity = new Vector2(this.body.velocity.x, this.speed);
                    if ( ((this.transform.position.y <= (this.topYPosition + 0.25f)) && (this.speed >= 0))
                      || ((this.transform.position.y >= (this.bottomYPosition - 0.25f)) && (this.speed <= 0)) )
                        this.speed *= -1;
                }
            }
        }
        else
        {
            this.topHitBoxCollider2D.enabled = false;
            this.damagePartBoxCollider2D.enabled = false;

            this.body.gravityScale = 3f;
        }
    }

    private void OnCollisionEnter2D(Collision2D _collision) 
    {
        if ( _collision.gameObject.tag == "Player" )
            StartCoroutine(this.HitBehaviour());

        this.ani.SetTrigger("Grounded");
        this.isFalling = false;
    }

    private void OnTriggerEnter2D(Collider2D _collider) 
    {
        if ( _collider.tag == "Player" )
            if ( this.playerInTerritory )
                this.isFalling = true;
    }

    private IEnumerator HitBehaviour()
    {
        if ( this.enemyTopHit.isHit )
        {
            this.ani.SetBool("Idle", false);
            this.ani.SetTrigger("Hit");
            yield return new WaitForSeconds(1.5f);
            this.gameObject.SetActive(false);
        }
    }
}
