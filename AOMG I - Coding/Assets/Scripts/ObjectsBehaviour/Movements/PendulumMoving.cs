using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumMoving : MonoBehaviour
{
    [Header ("Movement Attributes")]
    [SerializeField] private float angularSpeed;
    [SerializeField] private float angularAmplitude;

    private void Update()
    {
        float _timeNow = Time.realtimeSinceStartup;
        float _z = this.angularAmplitude * (float)(Math.Cos((this.angularSpeed / 180) * Math.PI * _timeNow));
        
        this.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, _z);
    }
}
