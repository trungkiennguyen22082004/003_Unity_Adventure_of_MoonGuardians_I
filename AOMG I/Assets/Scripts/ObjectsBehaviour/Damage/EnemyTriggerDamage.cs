using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerDamage : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D _collider)
    {
        if ( ( _collider.tag == "Player" ) && (this.damage != 0) )
        {
            _collider.GetComponent<HealthSystem>().TakeDamage(this.damage);
        }
    }
}
