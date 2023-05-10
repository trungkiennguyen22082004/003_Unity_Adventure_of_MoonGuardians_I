using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleBehaviour : MonoBehaviour
{
    [Header ("Turtle's Attributes")]
    [SerializeField] private float spikesTriggeredDuration;
    [SerializeField] private EnemyTopHit enemyTopHit;
    [SerializeField] private CircleCollider2D damagePartCircleCollider2D;
    [SerializeField] private CircleCollider2D bodyPartCircleCollider2D;
    [SerializeField] private BoxCollider2D topHitBoxCollider2D;

    private Rigidbody2D body;
    private Animator ani;

    private float timer;
    private bool spikesIn = true;

    private int i;

    private void Awake() 
    {
        this.body = GetComponent<Rigidbody2D>();
        this.ani = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if ( !this.enemyTopHit.isHit )
        {
            this.timer += Time.fixedDeltaTime;

            if ( (this.timer >= 0) && (this.timer < this.spikesTriggeredDuration) )
            {
                if ( this.spikesIn )
                    this.ani.SetTrigger("SpikesOut");
                else
                {
                    this.damagePartCircleCollider2D.enabled = true;
                    this.bodyPartCircleCollider2D.enabled = false;
                    this.topHitBoxCollider2D.enabled = false;        
                }
            }
            else if ( (this.timer >= this.spikesTriggeredDuration) && (this.timer < (2 * this.spikesTriggeredDuration)) )
            {
                if ( !this.spikesIn )
                    this.ani.SetTrigger("SpikesIn");
                else
                {
                    this.damagePartCircleCollider2D.enabled = false;
                    this.bodyPartCircleCollider2D.enabled = true;
                    this.topHitBoxCollider2D.enabled = true;       
                }
            }
            else if ( this.timer >= (2 * this.spikesTriggeredDuration) )
                this.timer = 0;
        }
        else
        {
            this.topHitBoxCollider2D.enabled = false;
            this.damagePartCircleCollider2D.enabled = false;
            this.bodyPartCircleCollider2D.enabled = false;

            this.body.gravityScale = 3;
            this.body.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        }
    }

    private void SpikesIn()
    {
        this.spikesIn = true;
        this.ani.SetBool("IdleOut", false);
        this.ani.ResetTrigger("SpikesIn");
    }

    private void SpikesOut()
    {
        this.spikesIn = false;
        this.ani.SetBool("IdleOut", true);
        this.ani.ResetTrigger("SpikesOut");
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
}
