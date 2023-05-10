using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionDamage : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if ( ( _collision.gameObject.tag == "Player" ) && (this.damage != 0) )
        {
            _collision.gameObject.GetComponent<HealthSystem>().TakeDamage(this.damage);
        }
    }
}