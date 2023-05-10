using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTopHitBehaviour : MonoBehaviour
{
    [SerializeField] private EnemyTopHit enemyTopHit;
    [SerializeField] private float beingHitBehaviourTime;
    private Animator ani;

    private void Awake() 
    {
        this.ani = GetComponent<Animator>();
    }

    private void FixedUpdate() 
    {
        if ( this.enemyTopHit.isHit )
        {
            StartCoroutine(Moving());
            this.enemyTopHit.isHit = false;
        }
    }

    private IEnumerator Moving()
    {
        this.ani.SetTrigger("Moving");
        yield return new WaitForSeconds(this.beingHitBehaviourTime);
        this.ani.SetTrigger("Idle");
    }
}
