using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBirdBehaviour : MonoBehaviour
{
    [Header ("Territory")]
    [SerializeField] private float rightXPosition;
    [SerializeField] private float leftXPosition;

    [Header ("Blue Bird's Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private EnemyTopHit enemyTopHit;
    [SerializeField] private BoxCollider2D damagePartBoxCollider2D;
    [SerializeField] private BoxCollider2D topHitBoxCollider2D;

    private Rigidbody2D body;
    private Animator ani;

    private void Start() 
    {
        this.body = GetComponent<Rigidbody2D>();
        this.ani = GetComponent<Animator>();
    }

    private void FixedUpdate() 
    {
        if ( !this.enemyTopHit.isHit )
        {
            this.transform.localScale = new Vector3(-5 * Mathf.Sign(this.speed), 5, 5);

            this.body.velocity = new Vector2(this.speed, this.body.velocity.y);
            if ( ( (this.transform.position.x >= this.rightXPosition) && (this.speed > 0) ) || ( (this.transform.position.x <= this.leftXPosition) && (this.speed < 0) ) )
                this.speed *= -1;
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
        if ( this.enemyTopHit.isHit )
        {
            this.ani.SetTrigger("Hit");
            yield return new WaitForSeconds(1.5f);
            this.gameObject.SetActive(false);
        }
    }
}
