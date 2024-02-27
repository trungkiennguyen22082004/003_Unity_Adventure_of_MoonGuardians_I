using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBehaviour : MonoBehaviour
{
    [Header ("Player's Position")]
    [SerializeField] private Transform playerTransform;

    [Header ("Territory")]
    [SerializeField] private float topYPosition;
    [SerializeField] private float bottomYPosition;
    [SerializeField] private float rightXPosition;
    [SerializeField] private float leftXPosition;

    [Header ("Mushroom's Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private EnemyTopHit enemyTopHit;
    [SerializeField] private BoxCollider2D damagePartBoxCollider2D;
    [SerializeField] private BoxCollider2D topHitBoxCollider2D;

    private Rigidbody2D body;
    private Animator ani;

    private bool playerInTerritory = false;
    private bool justHit = false;

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
            
            if ( this.playerInTerritory )
            {
                this.ani.SetBool("Run", false);
                this.ani.SetBool("Idle", ( !(this.justHit) && !(this.enemyTopHit.isHit) ));
                this.body.velocity = new Vector2(0, this.body.velocity.y);
            }
            else
            {
                this.ani.SetBool("Idle", false);
                this.ani.SetBool("Run", ( !(this.justHit) && !(this.enemyTopHit.isHit) ));

                this.transform.localScale = new Vector3(-5 * Mathf.Sign(this.speed), 5, 5);

                this.body.velocity = new Vector2(this.speed, this.body.velocity.y);
                if ( ( (this.transform.position.x >= this.rightXPosition) && (this.speed > 0) ) || ( (this.transform.position.x <= this.leftXPosition) && (this.speed < 0) ) )
                    this.speed *= -1;
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
        this.justHit = true;
        this.ani.SetBool("Run", false);
        this.ani.SetBool("Idle", false);
        this.ani.SetTrigger("Hit");
        yield return new WaitForSeconds(0.2f);
        this.justHit = false;

        if ( this.enemyTopHit.isHit )
        {
            yield return new WaitForSeconds(1.5f);
            this.gameObject.SetActive(false);
        }
    }
}
