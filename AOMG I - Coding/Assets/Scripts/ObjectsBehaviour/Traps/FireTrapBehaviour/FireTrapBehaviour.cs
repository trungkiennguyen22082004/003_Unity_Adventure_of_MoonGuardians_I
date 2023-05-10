using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrapBehaviour : MonoBehaviour
{
    [SerializeField] private EnemyTopHit enemyTopHit;
    [SerializeField] private BoxCollider2D fireBoxCollider2D;

    private Animator ani;

    private void Awake() 
    {
        this.ani = GetComponent<Animator>();
    }

    private void FixedUpdate() 
    {
        if ( this.enemyTopHit.isHit )
        {
            StartCoroutine(Firing());
            this.enemyTopHit.isHit = false;
        }
    }

    private IEnumerator Firing()
    {
        this.ani.SetTrigger("Hit");
        yield return new WaitForSeconds(1.5f);

        this.ani.SetTrigger("Firing");
        this.fireBoxCollider2D.enabled = true;
        yield return new WaitForSeconds(1.5f);
        this.fireBoxCollider2D.enabled = false;

        this.ani.SetTrigger("Off");
    }
}
