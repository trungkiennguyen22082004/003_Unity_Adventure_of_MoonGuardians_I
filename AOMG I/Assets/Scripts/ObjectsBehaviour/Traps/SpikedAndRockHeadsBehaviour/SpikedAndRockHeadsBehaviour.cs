using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedAndRockHeadsBehaviour : MonoBehaviour
{
    private Animator ani;
    private Transform playerMovementTransform;
    private Vector2 collisionVector2;

    private void Start()
    { 
        this.ani = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D _collision) 
    {
        this.playerMovementTransform = _collision.gameObject.GetComponent<Transform>();
        this.collisionVector2 = new Vector2(this.playerMovementTransform.position.x - this.transform.position.x, this.playerMovementTransform.position.y - this.transform.position.y);

        if ( ( (this.collisionVector2.x >= this.collisionVector2.y) && (this.collisionVector2.x >= (-this.collisionVector2.y) ) ) 
        || ( (this.collisionVector2.x <= this.collisionVector2.y) && (this.collisionVector2.x <= (-this.collisionVector2.y) ) ) )
        {
            this.ani.SetTrigger("HorizontalCollision");
        }
        else
        {
            this.ani.SetTrigger("VerticalCollision");
        }
    }

}