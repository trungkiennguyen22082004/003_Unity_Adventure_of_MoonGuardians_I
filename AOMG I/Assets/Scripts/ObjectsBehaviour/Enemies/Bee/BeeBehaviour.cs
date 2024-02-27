using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBehaviour : MonoBehaviour
{
    [Header ("Player's Position")]
    [SerializeField] private Transform playerTransform;

    [Header ("Territory")]
    [SerializeField] private float topYPosition;
    [SerializeField] private float bottomYPosition;
    [SerializeField] private float rightXPosition;
    [SerializeField] private float leftXPosition;

    [Header ("Bee's Attributes")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private EnemyTopHit enemyTopHit;
    [SerializeField] private BoxCollider2D topHitBoxCollider2D;
    [SerializeField] private Transform bulletTransform;
    [SerializeField] private GameObject[] bullets;

    private float cooldownTimer = Mathf.Infinity;
    private bool playerInTerritory = false;

    private Rigidbody2D body;
    private Animator ani;
    private BoxCollider2D boxCollider2D;

    private void Awake() 
    {
        this.body = GetComponent<Rigidbody2D>();
        this.ani = GetComponent<Animator>();
        this.boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update() 
    {
        if ( !this.enemyTopHit.isHit )
        {
            if ( (this.playerTransform.position.x <= this.rightXPosition) && (this.playerTransform.position.x >= this.leftXPosition) && (this.playerTransform.position.y <= this.topYPosition) && (this.playerTransform.position.y >= this.bottomYPosition) )
                this.playerInTerritory = true;
            else
                this.playerInTerritory = false;

            if ( (this.playerInTerritory) && (this.cooldownTimer > this.attackCooldown) )
                this.Attack();

            this.cooldownTimer += Time.deltaTime;
        }
        else
        {     
            this.topHitBoxCollider2D.enabled = false;
            this.boxCollider2D.enabled = false;

            this.body.gravityScale = 3;
            this.body.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        }
    }

    private void Attack()
    {
        this.ani.SetTrigger("Attack");
        cooldownTimer = 0;
        StartCoroutine(this.ReadyToAttack());
    }

    private IEnumerator ReadyToAttack()
    {
        yield return new WaitForSeconds(0.35f);
        this.bullets[this.FindBullet()].transform.position = this.bulletTransform.position;
        this.bullets[this.FindBullet()].GetComponent<BeeBulletBehaviour>().SetUpBullet();
    }

    private int FindBullet()
    {
        for (int i = 0; i < this.bullets.Length; i++)
        {
            if (!this.bullets[i].activeInHierarchy)
                return i;
        }
        return 0;
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
