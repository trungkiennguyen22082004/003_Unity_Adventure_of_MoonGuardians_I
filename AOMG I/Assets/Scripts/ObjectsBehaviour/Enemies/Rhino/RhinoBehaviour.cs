using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoBehaviour : MonoBehaviour
{
    [Header ("Player's Position")]
    [SerializeField] private Transform playerTransform;

    [Header ("Territory")]
    [SerializeField] private float topYPosition;
    [SerializeField] private float bottomYPosition;
    [SerializeField] private float rightXPosition;
    [SerializeField] private float leftXPosition;

    [Header ("Rhino's Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private EnemyTopHit enemyTopHit;
    [SerializeField] private BoxCollider2D damagePartBoxCollider2D;
    [SerializeField] private BoxCollider2D topHitBoxCollider2D;

    private float firstXPosition;
    private float firstYPosition;
    private Rigidbody2D body;
    private Animator ani;
    private bool playerInTerritory = false;

    private void Start() 
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
            if ( (this.playerTransform.position.x <= this.rightXPosition) && (this.playerTransform.position.x >= this.leftXPosition) && (this.playerTransform.position.y <= this.topYPosition) && (this.playerTransform.position.y >= this.bottomYPosition) )
                this.playerInTerritory = true;
            else
            {
                this.playerInTerritory = false;
            }

            if ( !this.playerInTerritory )
            {
                this.ani.SetBool("Run", false);

                this.transform.localScale = new Vector3(-4 * Mathf.Sign(this.body.velocity.x), this.transform.localScale.y, this.transform.localScale.z);
                this.body.velocity = new Vector2(this.speed, this.body.velocity.y);
                if ( ( (this.transform.position.x >= this.rightXPosition) && (this.speed > 0) ) || ( (this.transform.position.x <= this.leftXPosition) && (this.speed < 0) ) )
                    this.speed *= -1;
            }
            else
            {
                this.ani.SetBool("Run", true);

                this.speed = Math.Abs(this.speed);
                this.body.velocity = new Vector2(Mathf.Sign(this.playerTransform.position.x - this.transform.position.x) * this.speed * 3, Mathf.Sign(this.playerTransform.position.y - this.transform.position.y) * this.speed);
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
            this.topHitBoxCollider2D.enabled = false;
            this.damagePartBoxCollider2D.enabled = false;

            this.body.gravityScale = 2.5f;
            this.body.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
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
        this.ani.SetTrigger("Hit");
        yield return new WaitForSeconds(0.2f);

        if ( this.enemyTopHit.isHit )
        {
            yield return new WaitForSeconds(1.5f);
            this.gameObject.SetActive(false);
        }
    }
}
