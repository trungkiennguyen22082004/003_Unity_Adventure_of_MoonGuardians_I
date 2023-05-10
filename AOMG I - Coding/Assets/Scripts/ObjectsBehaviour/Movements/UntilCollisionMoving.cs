using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UntilCollisionMoving : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private string verticalOrHorizontal;
    [SerializeField] private float delayTime;

    private Rigidbody2D body;
    private BoxCollider2D boxCollider2D;
    private float initialPoxX;
    private float initialPoxY;
    private bool alreadyChangeSpeed = false;
    private bool isInDelay = false;

    private void Awake()
    {
        this.body = GetComponent<Rigidbody2D>();
        this.boxCollider2D = GetComponent<BoxCollider2D>();

        this.initialPoxX = this.transform.position.x;
        this.initialPoxY = this.transform.position.y;
    }

    private void FixedUpdate()
    {
        if ( !this.isInDelay )
        {
            if ( this.verticalOrHorizontal == "Vertical" )
            {
                this.transform.position = new Vector3(this.initialPoxX, this.transform.position.y, this.transform.position.z);
                this.body.velocity = new Vector2(this.body.velocity.x, this.speed);
            }
            else if ( this.verticalOrHorizontal == "Horizontal" )
            {
                this.transform.position = new Vector3(this.transform.position.x, this.initialPoxY, this.transform.position.z);
                this.body.velocity = new Vector2(this.speed, this.body.velocity.y);
            }
            this.alreadyChangeSpeed = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( ( ( collision.gameObject.layer == 8 ) || ( collision.gameObject.layer == 10 ) ) && (!this.alreadyChangeSpeed) )
        {
            StartCoroutine(this.DelayTime());
            this.speed *= -1;
            this.alreadyChangeSpeed = true;
        }
    }

    private IEnumerator DelayTime()
    {
        this.isInDelay = true;
        yield return new WaitForSeconds(this.delayTime);
        this.isInDelay = false;
    }
}
