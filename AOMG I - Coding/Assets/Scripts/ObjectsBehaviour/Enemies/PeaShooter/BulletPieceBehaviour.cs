using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPieceBehaviour : MonoBehaviour
{
    public void Activate(Vector3 _parentPosition)
    {
        this.gameObject.SetActive(true);
        this.transform.position = _parentPosition;
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
