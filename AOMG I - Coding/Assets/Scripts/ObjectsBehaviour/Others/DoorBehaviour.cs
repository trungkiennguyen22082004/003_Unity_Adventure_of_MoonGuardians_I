using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [Header ("Core")]
    [SerializeField] private string typeOfDoor;
    [SerializeField] private CameraController cam;

    [Header ("Old Room's Attributes")]
    [SerializeField] private float oldRoomPosX;
    [SerializeField] private float oldRoomPosY;
    [SerializeField] private float oldRoomLeftPoint;
    [SerializeField] private float oldRoomRightPoint;
    [SerializeField] private float oldRoomTopPoint;
    [SerializeField] private float oldRoomBottomPoint;

    [Header ("New Room's Attributes")]
    [SerializeField] private float newRoomPosX;
    [SerializeField] private float newRoomPosY;
    [SerializeField] private float newRoomLeftPoint;
    [SerializeField] private float newRoomRightPoint;
    [SerializeField] private float newRoomTopPoint;
    [SerializeField] private float newRoomBottomPoint;

    private bool isNotTriggered = false;

    private void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider.tag == "Player")
        {
            if ( !this.isNotTriggered )
            {
                StartCoroutine(this.NotTrigger());
                switch ( this.typeOfDoor )
                {
                    case "NorthToSouth":
                        if ( _collider.transform.position.y > this.transform.position.y )
                            this.cam.MoveToNewRoom(this.newRoomPosX, this.newRoomPosY, this.newRoomLeftPoint, this.newRoomRightPoint, this.newRoomTopPoint, this.newRoomBottomPoint);
                        else
                            this.cam.MoveToNewRoom(this.oldRoomPosX, this.oldRoomPosY, this.oldRoomLeftPoint, this.oldRoomRightPoint, this.oldRoomTopPoint, this.oldRoomBottomPoint);
                        break;
                    case "SouthToNorth":
                        if ( _collider.transform.position.y < this.transform.position.y )
                            this.cam.MoveToNewRoom(this.newRoomPosX, this.newRoomPosY, this.newRoomLeftPoint, this.newRoomRightPoint, this.newRoomTopPoint, this.newRoomBottomPoint);
                        else
                            this.cam.MoveToNewRoom(this.oldRoomPosX, this.oldRoomPosY, this.oldRoomLeftPoint, this.oldRoomRightPoint, this.oldRoomTopPoint, this.oldRoomBottomPoint);
                        break;
                    case "WestToEast":
                        if ( _collider.transform.position.x < this.transform.position.x )
                            this.cam.MoveToNewRoom(this.newRoomPosX, this.newRoomPosY, this.newRoomLeftPoint, this.newRoomRightPoint, this.newRoomTopPoint, this.newRoomBottomPoint);
                        else
                            this.cam.MoveToNewRoom(this.oldRoomPosX, this.oldRoomPosY, this.oldRoomLeftPoint, this.oldRoomRightPoint, this.oldRoomTopPoint, this.oldRoomBottomPoint);
                        break;
                    case "EastToWest":
                        if ( _collider.transform.position.x > this.transform.position.x )
                            this.cam.MoveToNewRoom(this.newRoomPosX, this.newRoomPosY, this.newRoomLeftPoint, this.newRoomRightPoint, this.newRoomTopPoint, this.newRoomBottomPoint);
                        else
                            this.cam.MoveToNewRoom(this.oldRoomPosX, this.oldRoomPosY, this.oldRoomLeftPoint, this.oldRoomRightPoint, this.oldRoomTopPoint, this.oldRoomBottomPoint);
                        break;
                    case "NorthWestToSouthEast":
                        if ( (_collider.transform.position.x - this.transform.position.x) < (_collider.transform.position.y - this.transform.position.y) )
                            this.cam.MoveToNewRoom(this.newRoomPosX, this.newRoomPosY, this.newRoomLeftPoint, this.newRoomRightPoint, this.newRoomTopPoint, this.newRoomBottomPoint);
                        else
                            this.cam.MoveToNewRoom(this.oldRoomPosX, this.oldRoomPosY, this.oldRoomLeftPoint, this.oldRoomRightPoint, this.oldRoomTopPoint, this.oldRoomBottomPoint);
                        break;
                    case "SouthEastToNorthWest":
                        if ( (_collider.transform.position.x - this.transform.position.x) > (_collider.transform.position.y - this.transform.position.y) )
                            this.cam.MoveToNewRoom(this.newRoomPosX, this.newRoomPosY, this.newRoomLeftPoint, this.newRoomRightPoint, this.newRoomTopPoint, this.newRoomBottomPoint);
                        else
                            this.cam.MoveToNewRoom(this.oldRoomPosX, this.oldRoomPosY, this.oldRoomLeftPoint, this.oldRoomRightPoint, this.oldRoomTopPoint, this.oldRoomBottomPoint);
                        break;
                    case "NorthEastToSouthWest":
                        if ( (_collider.transform.position.x - this.transform.position.x) > (this.transform.position.y - _collider.transform.position.y) )
                            this.cam.MoveToNewRoom(this.newRoomPosX, this.newRoomPosY, this.newRoomLeftPoint, this.newRoomRightPoint, this.newRoomTopPoint, this.newRoomBottomPoint);
                        else
                            this.cam.MoveToNewRoom(this.oldRoomPosX, this.oldRoomPosY, this.oldRoomLeftPoint, this.oldRoomRightPoint, this.oldRoomTopPoint, this.oldRoomBottomPoint);
                        break;
                    case "SouthWestToNorthEast":
                        if ( (_collider.transform.position.x - this.transform.position.x) < (this.transform.position.y - _collider.transform.position.y) )
                            this.cam.MoveToNewRoom(this.newRoomPosX, this.newRoomPosY, this.newRoomLeftPoint, this.newRoomRightPoint, this.newRoomTopPoint, this.newRoomBottomPoint);
                        else
                            this.cam.MoveToNewRoom(this.oldRoomPosX, this.oldRoomPosY, this.oldRoomLeftPoint, this.oldRoomRightPoint, this.oldRoomTopPoint, this.oldRoomBottomPoint);
                        break;
                    default:
                        Debug.Log("Wrong Door's Direction KeyWord");
                        break;
                }
            }
        }
    }

    private IEnumerator NotTrigger()
    {
        this.isNotTriggered = true;
        yield return new WaitForSeconds(0.75f);
        this.isNotTriggered = false;
    }
}
