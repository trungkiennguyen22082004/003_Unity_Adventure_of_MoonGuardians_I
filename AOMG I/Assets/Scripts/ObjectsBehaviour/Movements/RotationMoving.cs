using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMoving : MonoBehaviour
{    
    [Header ("Movement Attributes")]
    [SerializeField] private float angularSpeed;

    private void Update()
    {
        this.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + (this.angularSpeed * Time.deltaTime));
    }
}
