using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBehaviour : MonoBehaviour
{
    [Header ("Player")]
    [SerializeField] private GameObject player;

    [Header ("Effective Area")]
    [SerializeField] private float topYPosition;
    [SerializeField] private float bottomYPosition;
    [SerializeField] private float rightXPosition;
    [SerializeField] private float leftXPosition;
    [SerializeField] private GameObject[] effectiveSprites;
    [SerializeField] private int totalNumberofSprites;

    [Header ("Fan's Attributes")]
    [SerializeField] private string pushOrPull;
    [SerializeField] private float power;
    private bool playerInArea = false;
    
    private void FixedUpdate() 
    {
        if ( (this.player.transform.position.x <= this.rightXPosition) && (this.player.transform.position.x >= this.leftXPosition) && (this.player.transform.position.y <= this.topYPosition) && (this.player.transform.position.y >= this.bottomYPosition) )
            this.playerInArea = true;
        else
            this.playerInArea = false;

        if ( this.playerInArea )
        {
            if ( this.pushOrPull == "Push")
                this.player.GetComponent<PlayerMovement>().body.velocity = new Vector2(this.player.GetComponent<PlayerMovement>().body.velocity.x + (this.power * Mathf.Sign(this.transform.localScale.x)), this.player.GetComponent<PlayerMovement>().body.velocity.y);
            else if ( this.pushOrPull == "Pull")
                this.player.GetComponent<PlayerMovement>().body.velocity = new Vector2(this.player.GetComponent<PlayerMovement>().body.velocity.x - (this.power * Mathf.Sign(this.transform.localScale.x)), this.player.GetComponent<PlayerMovement>().body.velocity.y);
        }

        if ( (Random.Range(0, 1000) <= 80) && (this.NumberofSprites() <= this.totalNumberofSprites))
        {
            this.effectiveSprites[FindSprite()].GetComponent<FanEffectSpriteBehaviour>().SetUpSprite(this.rightXPosition, this.leftXPosition, this.topYPosition, this.bottomYPosition, this.pushOrPull, this.power);
        }
    }

    private int FindSprite()
    {
        for (int i = 0; i < this.effectiveSprites.Length; i++)
        {
            if ( !this.effectiveSprites[i].activeInHierarchy )
                return i;
        }
        return 0;
    }

    private int NumberofSprites()
    {
        int _sum = 0;
        for (int i = 0; i < this.effectiveSprites.Length; i++)
        {
            if ( this.effectiveSprites[i].activeInHierarchy )
                _sum += 1;
        }
        return _sum;
    }
}
