using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformBehaviour : MonoBehaviour
{
    private Animator ani;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        this.ani = GetComponent<Animator>();
        this.body = GetComponent<Rigidbody2D>();
        this.boxCollider2D = GetComponent<BoxCollider2D>();

        this.body.gravityScale = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.tag == "Player" )
        {
            StartCoroutine(AHalfSecondsBeforeFalling());
        }
    }

    private IEnumerator AHalfSecondsBeforeFalling()
    {
        yield return new WaitForSeconds(0.5f);
        this.body.gravityScale = 1;
        this.ani.enabled = false;
    }
}
