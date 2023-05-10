using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header ("Camera's Limitation")]
    [SerializeField] private Transform player;
    [SerializeField] private float leftPoint;
    [SerializeField] private float rightPoint;
    [SerializeField] private float topPoint;
    [SerializeField] private float bottomPoint;

    [Header ("Camera's Position")]
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float aheadDistance;

    private float lookAheadX;
    private float lookAheadY;
    private float roomPosX;
    private float roomPosY;
    public bool isMovingToNewRoom = false;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        if ( !isMovingToNewRoom )
        {
            if ( ((this.player.position.x + this.lookAheadX) >= this.leftPoint) && ((this.player.position.x + this.lookAheadX) <= this.rightPoint) )
                this.transform.position = new Vector3(this.player.position.x + this.lookAheadX, this.transform.position.y, this.transform.position.z);
            this.lookAheadX = Mathf.Lerp(this.lookAheadX, (this.aheadDistance * this.player.localScale.x), this.cameraSpeed * Time.deltaTime);

            if ( ((this.player.position.y - this.lookAheadY) >= this.bottomPoint) && ((this.player.position.y - this.lookAheadY) <= this.topPoint) )
                this.transform.position = new Vector3(this.transform.position.x, this.player.position.y - this.lookAheadY, this.transform.position.z);
            this.lookAheadY = (Mathf.Lerp(this.lookAheadY, (this.aheadDistance * this.player.localScale.y), (this.cameraSpeed / 2) * Time.deltaTime)) / 1.25f;
        }
        else
        {    
            this.transform.position = Vector3.SmoothDamp(this.transform.position, new Vector3(this.roomPosX, this.roomPosY, this.transform.position.z), ref this.velocity, this.cameraSpeed);
        }
    }

    public void MoveToNewRoom(float _roomPosX, float _roomPosY, float _leftPoint, float _rightPoint, float _topPoint, float _bottomPoint)
    {
        this.roomPosX = _roomPosX;
        this.roomPosY = _roomPosY;

        this.leftPoint = _leftPoint;
        this.rightPoint = _rightPoint;
        this.topPoint = _topPoint;
        this.bottomPoint = _bottomPoint;

        StartCoroutine(this.MovingToNewRoom());
    }

    private IEnumerator MovingToNewRoom()
    {
        this.isMovingToNewRoom = true;
        yield return new WaitForSeconds(0.75f);
        this.isMovingToNewRoom = false;
    }
}
