using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingLimitedMoving : MonoBehaviour
{
    [Header ("Movement's Attributes")]
    [SerializeField] private float topYPosition;
    [SerializeField] private float bottomYPosition;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float rightXPosition;
    [SerializeField] private float leftXPosition;
    [SerializeField] private float horizontalSpeed;

    private void FixedUpdate()
    {
        this.transform.position = new Vector3(this.transform.position.x + this.horizontalSpeed * Time.deltaTime, this.transform.position.y + this.verticalSpeed * Time.deltaTime, this.transform.position.z);
        if ( ( this.transform.position.y >= this.topYPosition ) && (this.verticalSpeed != 0) )
            this.transform.position = new Vector3(this.transform.position.x, this.bottomYPosition, this.transform.position.z);
        if ( ( this.transform.position.x >= this.rightXPosition ) && ( this.horizontalSpeed != 0 ) )
            this.transform.position = new Vector3(this.leftXPosition, this.transform.position.y, this.transform.position.z);
    }
}
