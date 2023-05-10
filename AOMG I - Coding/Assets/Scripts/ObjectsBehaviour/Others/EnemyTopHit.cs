using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTopHit : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    public bool isHit = false;

    private void Awake() 
    {
        this.boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D _collision) 
    {
        if (_collision.gameObject.tag == "Player")
        {
            if ( this.gameObject.tag == "Enemy Top Hit" )
                _collision.gameObject.GetComponent<PlayerMovement>().Jump(1f);
            else if ( this.gameObject.tag == "End Point" )
                _collision.gameObject.GetComponent<HealthSystem>().Finish();
            this.isHit = true;
        }
    }
}
