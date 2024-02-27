using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChameleonBehaviour : MonoBehaviour
{
    [Header ("Player's Position")]
    [SerializeField] private Transform playerTransform;

    [Header ("Territory")]
    [SerializeField] private float topYPosition;
    [SerializeField] private float bottomYPosition;
    [SerializeField] private float rightXPosition;
    [SerializeField] private float leftXPosition;

    [Header ("Chameleon's Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private EnemyTopHit enemyTopHit;
    [SerializeField] private BoxCollider2D bodyDamagePartBoxCollider2D;
    [SerializeField] private BoxCollider2D attackDamagePartBoxCollider2D;
    [SerializeField] private BoxCollider2D topHitBoxCollider2D;
    [SerializeField] private float attackCooldown;

    private Rigidbody2D body;
    private Animator ani;
    private bool playerInTerritory = false;
    private float attackCooldownTimer = 1000;

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

            if ( (this.playerInTerritory) )
            {
                if ( (this.transform.position.x <= this.playerTransform.position.x) && (this.transform.localScale.x > 0) )
                {
                    this.transform.localScale = new Vector3(-1 * this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
                    this.transform.position = new Vector3(this.transform.position.x + 1.5f, this.transform.position.y, this.transform.position.z);
                }
                else if ( (this.transform.position.x >= this.playerTransform.position.x) && (this.transform.localScale.x < 0) )
                { 
                    this.transform.localScale = new Vector3(-1 * this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
                    this.transform.position = new Vector3(this.transform.position.x - 1.5f, this.transform.position.y, this.transform.position.z);
                }

                this.body.velocity = new Vector2(0, 0);
                this.ani.SetBool("Run", false);

                if ( this.attackCooldownTimer >= this.attackCooldown)
                {
                    if ( (this.playerTransform.position.x <= this.transform.position.x + 2.35) && (this.playerTransform.position.x >= this.transform.position.x) )
                    {
                        this.transform.localScale = new Vector3(-5, 5, 5);
                        StartCoroutine(this.Attack());
                    }
                    else if ( (this.playerTransform.position.x >= this.transform.position.x - 2.35) && (this.playerTransform.position.x <= this.transform.position.x) )
                    {
                        this.transform.localScale = new Vector3(5, 5, 5);
                        StartCoroutine(this.Attack());
                    }
                }
            }
            else
            {
                this.transform.localScale = new Vector3(-5 * Mathf.Sign(this.speed), 5, 5);
                this.body.velocity = new Vector2(this.speed, this.body.velocity.y);
                if ( (this.transform.position.x >= this.rightXPosition) && (this.speed > 0) )
                {
                    this.speed *= -1;
                    this.transform.position = new Vector3(this.transform.position.x - 1.5f, this.transform.position.y, this.transform.position.z);
                }
                else if ( (this.transform.position.x <= this.leftXPosition) && (this.speed < 0) )
                {
                    this.speed *= -1;
                    this.transform.position = new Vector3(this.transform.position.x + 1.5f, this.transform.position.y, this.transform.position.z);
                }

                this.ani.SetBool("Run", true);
            }
            this.attackCooldownTimer += Time.fixedDeltaTime;
        }
        else
        {
            this.topHitBoxCollider2D.enabled = false;
            this.bodyDamagePartBoxCollider2D.enabled = false;
            this.attackDamagePartBoxCollider2D.enabled = false;

            this.body.gravityScale = 3;
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
        if ( this.enemyTopHit.isHit )
        {
            this.ani.SetTrigger("Hit");
            yield return new WaitForSeconds(1.5f);
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.75f);
        this.ani.SetTrigger("Attack");
        this.attackCooldownTimer = 0;
    }

    private void ActivateAttackDamagePart()
    {
        this.attackDamagePartBoxCollider2D.enabled = true;
    }

    private void DeactivateAttackDamagePart()
    {
        this.attackDamagePartBoxCollider2D.enabled = false;
    }
}
