using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanEffectSpriteBehaviour : MonoBehaviour
{
    private Rigidbody2D body;
    private bool isActive = false;
    private string pushOrPull;
    private float power;
    private float rightXPosition;
    private float leftXPosition;

    private void Start() 
    {
        this.body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() 
    {
        if ( this.isActive )
        {
            if ( this.pushOrPull == "Pull" )
            {
                this.transform.position = new Vector3(this.transform.position.x - this.power * Time.fixedDeltaTime, this.transform.position.y, this.transform.position.z);
                if ( this.transform.position.x <= this.leftXPosition )
                {
                    this.isActive = false;
                    this.gameObject.SetActive(false);
                }
            }
            else if ( this.pushOrPull == "Push" )
            {
                this.transform.position = new Vector3(this.transform.position.x + this.power * Time.fixedDeltaTime, this.transform.position.y, this.transform.position.z);
                if ( this.transform.position.x >= this.rightXPosition )
                {
                    this.isActive = false;
                    this.gameObject.SetActive(false);
                }
            }
        }
    }

    public void SetUpSprite(float _rightXPosition, float _leftXPosition, float _topYPosition, float _bottomYPosition, string _pushOrPull, float _power)
    {
        this.gameObject.SetActive(true);
        this.isActive = true;

        this.rightXPosition = _rightXPosition;
        this.leftXPosition = _leftXPosition;
        this.pushOrPull = _pushOrPull;
        this.power = _power;
        
        if ( _pushOrPull == "Pull" )
            this.transform.position = new Vector3(this.rightXPosition, Random.Range(_bottomYPosition, _topYPosition), this.transform.position.z);
        else if ( _pushOrPull == "Push" )
            this.transform.position = new Vector3(this.leftXPosition, Random.Range(_bottomYPosition, _topYPosition), this.transform.position.z);
    }
}
