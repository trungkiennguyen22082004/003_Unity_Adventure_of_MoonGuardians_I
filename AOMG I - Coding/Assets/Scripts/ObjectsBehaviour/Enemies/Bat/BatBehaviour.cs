using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBehaviour : MonoBehaviour
{
    [Header ("Player's Position")]
    [SerializeField] private Transform playerTransform;

    [Header ("Territory")]
    [SerializeField] private float topYPosition;
    [SerializeField] private float bottomYPosition;
    [SerializeField] private float rightXPosition;
    [SerializeField] private float leftXPosition;

    [Header ("Bat's Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private EnemyTopHit enemyTopHit;
    [SerializeField] private BoxCollider2D damagePartBoxCollider2D;
    [SerializeField] private BoxCollider2D topHitBoxCollider2D;

    private Rigidbody2D body;
    private Animator ani;

    private float firstXPosition;
    private float firstYPosition;
    private float targetXPosition;
    private float targetYPosition;
    private bool playerInTerritory = false;
    private bool justHit = false;

    private void Awake() 
    {
        this.body = GetComponent<Rigidbody2D>();
        this.ani = GetComponent<Animator>();

        this.firstXPosition = this.transform.position.x;
        this.firstYPosition = this.transform.position.y;
    }

    private void FixedUpdate() 
    {
        if ( !this.enemyTopHit.isHit )
        {
            this.ani.SetBool("Fly", !( (Math.Abs(this.transform.position.x - this.firstXPosition) <= 0.2) && (Math.Abs(this.transform.position.y - this.firstYPosition) <= 0.2) && (!(this.justHit)) && (!this.enemyTopHit.isHit) ));

            if ( !this.justHit )
            {
                if ( (this.playerTransform.position.x <= this.rightXPosition) && (this.playerTransform.position.x >= this.leftXPosition) && (this.playerTransform.position.y <= this.topYPosition) && (this.playerTransform.position.y >= this.bottomYPosition) )
                    this.playerInTerritory = true;
                else
                    this.playerInTerritory = false;

                if ( this.playerInTerritory )
                {
                    this.targetXPosition = this.playerTransform.position.x;
                    this.targetYPosition = this.playerTransform.position.y;
                }
                else
                {
                    this.targetXPosition = this.firstXPosition;
                    this.targetYPosition = this.firstYPosition;
                }

                if ( (this.targetXPosition - this.transform.position.x) != 0 )
                    this.body.velocity = new Vector2(Mathf.Sign(this.targetXPosition - this.transform.position.x) * this.speed, Mathf.Sign(this.targetYPosition - this.transform.position.y) * this.speed);
                this.transform.localScale = new Vector3(-5 * Mathf.Sign(this.body.velocity.x), this.transform.localScale.y, this.transform.localScale.z);

                if ( !((this.transform.position.x <= this.rightXPosition) && (this.transform.position.x >= this.leftXPosition) && (this.transform.position.y <= this.topYPosition) && (this.transform.position.y >= this.bottomYPosition)) )
                    this.transform.position = new Vector3(this.firstXPosition, this.firstYPosition, this.transform.position.z);
            }
            else
                this.body.velocity = new Vector2((this.firstXPosition - this.transform.position.x), (this.firstYPosition - this.transform.position.y));
        }
        else
        {
            this.topHitBoxCollider2D.enabled = false;
            this.damagePartBoxCollider2D.enabled = false;

            this.body.gravityScale = 3;
        }
    }

    private void OnCollisionEnter2D(Collision2D _collision) 
    {
        if ( _collision.gameObject.tag == "Player" )
        {
            StartCoroutine(this.HitBehaviour());
        }
    }

    private IEnumerator HitBehaviour()
    {
        this.justHit = true;
        this.ani.SetBool("Fly", false);
        this.ani.SetTrigger("Hit");
        yield return new WaitForSeconds(0.5f);
        this.justHit = false;

        if ( this.enemyTopHit.isHit )
        {
            yield return new WaitForSeconds(1.5f);
            this.gameObject.SetActive(false);
        }
    }
}