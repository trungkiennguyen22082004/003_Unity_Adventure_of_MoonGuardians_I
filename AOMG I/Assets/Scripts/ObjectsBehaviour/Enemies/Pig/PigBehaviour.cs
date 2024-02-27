using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigBehaviour : MonoBehaviour
{
    [Header ("Player's Position")]
    [SerializeField] private Transform playerTransform;

    [Header ("Territory")]
    [SerializeField] private float topYPosition;
    [SerializeField] private float bottomYPosition;
    [SerializeField] private float rightXPosition;
    [SerializeField] private float leftXPosition;

    [Header ("Pig's Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private EnemyTopHit enemyTopHit;
    [SerializeField] private BoxCollider2D damagePartBoxCollider2D;
    [SerializeField] private BoxCollider2D topHitBoxCollider2D;

    private float firstXPosition;
    private float firstYPosition;
    private Rigidbody2D body;
    private Animator ani;

    private bool playerInTerritory = false;
    private bool isAngry = false;
    private bool angryIdle;
    private float idleTimer = 1000f;

    private void Awake()
    {
        this.body = GetComponent<Rigidbody2D>();
        this.ani = GetComponent<Animator>();

        this.firstXPosition = this.transform.position.x;
        this.firstYPosition = this.transform.position.y;
    }

    private void FixedUpdate() 
    {
        if ( !isAngry )
        {
            if ( !this.enemyTopHit.isHit )
            {
                this.idleTimer += Time.fixedDeltaTime;
                if ( this.idleTimer > 5f )
                {
                    this.transform.localScale = new Vector3(-1 * this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
                    this.idleTimer = 0;
                }

                this.transform.position = new Vector3(this.firstXPosition, this.firstYPosition, this.transform.position.z);
            }
        }
        else
        {
            if ( !this.enemyTopHit.isHit )
            {
                if ( (this.playerTransform.position.x <= this.rightXPosition) && (this.playerTransform.position.x >= this.leftXPosition) && (this.playerTransform.position.y <= this.topYPosition) && (this.playerTransform.position.y >= this.bottomYPosition) )
                    this.playerInTerritory = true;
                else
                    this.playerInTerritory = false;
                
                if ( !this.angryIdle )
                {
                    this.ani.SetBool("AngryRun", true);

                    if ( !this.playerInTerritory )
                    {
                        this.transform.localScale = new Vector3(-5 * Mathf.Sign(this.speed), 5, 5);

                        this.body.velocity = new Vector2(this.speed, this.body.velocity.y);
                        if ( ( (this.transform.position.x >= this.rightXPosition) && (this.speed > 0) ) || ( (this.transform.position.x <= this.leftXPosition) && (this.speed < 0) ) )
                            this.speed *= -1;
                    }
                    else
                    {
                        this.speed = Math.Abs(this.speed);
                        this.body.velocity = new Vector2(Mathf.Sign(this.playerTransform.position.x - this.transform.position.x) * this.speed, Mathf.Sign(this.playerTransform.position.y - this.transform.position.y) * this.speed);
                        this.transform.localScale = new Vector3(-4 * Mathf.Sign(this.body.velocity.x), this.transform.localScale.y, this.transform.localScale.z);

                        if ( !((this.transform.position.x <= this.rightXPosition) && (this.transform.position.x >= this.leftXPosition) && (this.transform.position.y <= this.topYPosition) && (this.transform.position.y >= this.bottomYPosition)) )
                        {
                            this.body.velocity = new Vector2(0, 0);
                            this.transform.position = new Vector3(this.firstXPosition, this.firstYPosition, this.transform.position.z);
                            this.playerInTerritory = false;
                        }
                    }
                }
                else
                {
                    this.speed = Math.Abs(this.speed);
                    this.body.velocity = new Vector2(Mathf.Sign(this.firstXPosition - this.transform.position.x) * this.speed, Mathf.Sign(this.firstYPosition - this.transform.position.y) * this.speed);
                    this.transform.localScale = new Vector3(-4 * Mathf.Sign(this.body.velocity.x), this.transform.localScale.y, this.transform.localScale.z);
                }
            }
            else
            {
                this.topHitBoxCollider2D.enabled = false;
                this.damagePartBoxCollider2D.enabled = false;

                this.body.gravityScale = 3;
                this.body.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D _collision) 
    {
        if ( _collision.gameObject.tag == "Player" )
        {
            StartCoroutine(this.AngryIdle());
        }
    }

    private IEnumerator AngryIdle()
    {
        if ( !this.enemyTopHit )
        {
            this.angryIdle = true;
            this.ani.SetBool("AngryRun", false);
            yield return new WaitForSeconds(0.75f);
            this.angryIdle = false;
        }
        else
        {
            if ( this.isAngry )
            {
                this.ani.SetTrigger("Hit");
                yield return new WaitForSeconds(1.5f);
                this.gameObject.SetActive(false);
            }
            else
            {
                yield return new WaitForSeconds(0.75f);
                this.enemyTopHit.isHit = false;
                this.isAngry = true;
            }
        }
    }
}
