using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangularMoving : MonoBehaviour
{
    [Header ("Movement's Attributes")]
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private float speed;
    [SerializeField] private float delayTime;

    private float firstXPosition;
    private float firstYPosition;
    private int orderOfPosition = 0;
    private bool isInDelay = false;

    private void Start() 
    {
        this.firstXPosition = this.transform.position.x;
        this.firstYPosition = this.transform.position.y;
    }

    private void FixedUpdate() 
    {
        float _x = this.transform.position.x - this.firstXPosition;
        float _y = this.transform.position.y - this.firstYPosition;
        if ( ( Math.Abs(_x - 0) < (this.speed * Time.fixedDeltaTime) ) && ( Math.Abs(_y - 0) < (this.speed * Time.fixedDeltaTime) && (this.orderOfPosition == 0) )
          || ( Math.Abs(_x - 0) < (this.speed * Time.fixedDeltaTime) ) && ( Math.Abs(_y - this.height) < (this.speed * Time.fixedDeltaTime) && (this.orderOfPosition == 1) )
          || ( Math.Abs(_x - this.width) < (this.speed * Time.fixedDeltaTime) ) && ( Math.Abs(_y - this.height) < (this.speed * Time.fixedDeltaTime) && (this.orderOfPosition == 2) )
          || ( Math.Abs(_x - this.width) < (this.speed * Time.fixedDeltaTime) ) && ( Math.Abs(_y - 0) < (this.speed * Time.fixedDeltaTime) ) && (this.orderOfPosition == 3) )
        {
            if (this.orderOfPosition < 3)
                this.orderOfPosition += 1;
            else
                this.orderOfPosition = 0;

            StartCoroutine(this.DelayTime());
        }
        if ( !this.isInDelay )
        {
            switch ( this.orderOfPosition )
            {
                case 0:
                    this.transform.position = new Vector3(this.transform.position.x - (this.speed * Time.fixedDeltaTime), this.transform.position.y, this.transform.position.z);
                    break;
                case 1:
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + (this.speed * Time.fixedDeltaTime), this.transform.position.z);
                    break;
                case 2:
                    this.transform.position = new Vector3(this.transform.position.x + (this.speed * Time.fixedDeltaTime), this.transform.position.y, this.transform.position.z);
                    break;
                case 3:
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - (this.speed * Time.fixedDeltaTime), this.transform.position.z);
                    break;
            }
        }
    }

    private IEnumerator DelayTime()
    {
        this.isInDelay = true;
        yield return new WaitForSeconds(this.delayTime);
        this.isInDelay = false;
    }
}
