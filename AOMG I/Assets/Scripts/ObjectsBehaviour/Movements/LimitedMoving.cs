using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedMoving : MonoBehaviour
{
    [Header ("Movement's Attributes")]
    [SerializeField] private float horizontalMovementDistance;
    [SerializeField] public float horizontalSpeed;
    [SerializeField] private float verticalMovementDistance;
    [SerializeField] public float verticalSpeed;
    [SerializeField] private float delayTime;

    private float leftEdge;
    private float rightEdge;
    private float bottomEdge;
    private float topEdge;
    private bool isInDelay = false;

    private void Awake()
    {
        this.leftEdge = this.transform.position.x - (this.horizontalMovementDistance / 2);
        this.rightEdge = this.transform.position.x + (this.horizontalMovementDistance / 2);

        this.bottomEdge = this.transform.position.y - (this.verticalMovementDistance / 2);
        this.topEdge = this.transform.position.y + (this.verticalMovementDistance / 2);
    }

    private void Update()
    {
        if ( ( (this.transform.position.x <= this.leftEdge) && (this.horizontalSpeed > 0) ) || ( (this.transform.position.x >= this.rightEdge) && (this.horizontalSpeed < 0) ) )
            StartCoroutine(this.DelayTime());

        if ( !this.isInDelay )
            this.transform.position = new Vector3(this.transform.position.x - this.horizontalSpeed * Time.deltaTime, this.transform.position.y, this.transform.position.z);

        if ( ( (this.transform.position.y <= this.bottomEdge) && (this.verticalSpeed > 0) ) || ( (this.transform.position.y >= this.topEdge) && (this.verticalSpeed < 0) ) )
            StartCoroutine(this.DelayTime());
            
        if ( !this.isInDelay )
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - this.verticalSpeed * Time.deltaTime, this.transform.position.z);
    }

    private IEnumerator DelayTime()
    {
        this.horizontalSpeed *= -1;
        this.verticalSpeed *= -1;
        this.isInDelay = true;
        yield return new WaitForSeconds(this.delayTime);
        this.isInDelay = false;
    }
}
