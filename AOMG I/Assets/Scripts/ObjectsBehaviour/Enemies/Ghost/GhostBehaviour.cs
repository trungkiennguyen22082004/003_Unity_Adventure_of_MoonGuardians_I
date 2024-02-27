using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    [Header ("Player's Position")]
    [SerializeField] private Transform playerTransform;

    [Header ("Territory")]
    [SerializeField] private float topYPosition;
    [SerializeField] private float bottomYPosition;
    [SerializeField] private float rightXPosition;
    [SerializeField] private float leftXPosition;

    [Header ("Ghost's Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private BoxCollider2D damagePartBoxCollider2D;

    private Rigidbody2D body;

    private float firstXPosition;
    private float firstYPosition;
    private float targetXPosition;
    private float targetYPosition;
    private bool playerInTerritory = false;
    private bool ignoreCollision = false;

    private void Awake() 
    {
        this.body = GetComponent<Rigidbody2D>();

        this.firstXPosition = this.transform.position.x;
        this.firstYPosition = this.transform.position.y;
    }

    private void FixedUpdate() 
    {
        if ( (this.playerTransform.position.x <= this.rightXPosition) && (this.playerTransform.position.x >= this.leftXPosition) && (this.playerTransform.position.y <= this.topYPosition) && (this.playerTransform.position.y >= this.bottomYPosition) )
            this.playerInTerritory = true;
        else
            this.playerInTerritory = false;

        if ( (this.playerInTerritory) && (!this.ignoreCollision) )
        {
            this.targetXPosition = this.playerTransform.position.x;
            this.targetYPosition = this.playerTransform.position.y;
        }
        else
        {
            this.targetXPosition = this.firstXPosition;
            this.targetYPosition = this.firstYPosition;
        }

        this.body.velocity = new Vector2((this.targetXPosition - this.transform.position.x) * this.speed, (this.targetYPosition - this.transform.position.y) * this.speed);
        this.transform.localScale = new Vector3(-5 * Mathf.Sign(this.body.velocity.x), this.transform.localScale.y, this.transform.localScale.z);

        if ( !((this.transform.position.x <= this.rightXPosition) && (this.transform.position.x >= this.leftXPosition) && (this.transform.position.y <= this.topYPosition) && (this.transform.position.y >= this.bottomYPosition)) )
            this.transform.position = new Vector3(this.firstXPosition, this.firstYPosition, this.transform.position.z);
    }

    private IEnumerator justHit()
    {
        this.ignoreCollision = true;
        this.damagePartBoxCollider2D.enabled = false;

        yield return new WaitForSeconds(2f);
        
        this.damagePartBoxCollider2D.enabled = true;
        this.ignoreCollision = false;
    }

    private void OnTriggerEnter2D(Collider2D _collider)
    {
        if ( _collider.tag == "Player" )
            StartCoroutine(this.justHit());
    }
}
