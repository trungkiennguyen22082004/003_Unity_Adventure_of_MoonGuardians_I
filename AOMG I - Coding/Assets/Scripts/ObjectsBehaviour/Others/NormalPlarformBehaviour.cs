using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlarformBehaviour : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private BoxCollider2D boxCollider2D;

    private void Awake() 
    {
        this.boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate() 
    {
        if ( this.playerTransform.position.y >= (this.transform.position.y + 0.5f) )
            this.boxCollider2D.isTrigger = false;
        else
            this.boxCollider2D.isTrigger = true;
    }
}
