using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Collision Layers")]
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private LayerMask transportTerrainLayer;

    [Header ("Moving Ability")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [Header ("Audio")]
    [SerializeField] private AudioClip jumpSound;

    private float wallJumpCooldown;
    private bool doubleJump = false;

    public Rigidbody2D body;
    private Animator ani;
    private BoxCollider2D boxCollider2D;
    private AudioSource audioSource;

    private LimitedMoving limitedMoving;
    private float horizontalInput;

    private void Awake()
    {
        this.body = GetComponent<Rigidbody2D>();
        this.ani = GetComponent<Animator>();
        this.boxCollider2D = GetComponent<BoxCollider2D>();
        this.audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        this.horizontalInput = Input.GetAxis("Horizontal");

        if ( this.horizontalInput > 0.01f )
            this.transform.localScale = new Vector3(5, 5, 5);
        else if ( this.horizontalInput < -0.01f )
            this.transform.localScale = new Vector3(-5, 5, 5);
            
        if ( this.wallJumpCooldown >= 0.4f )
        {
            if ( (onWall()) && (!isGrounded()) )
            {
                this.body.gravityScale = 0.75f;
            }
            else
                this.body.gravityScale = 2;

            if ( !onWall() )
                this.body.velocity = new Vector2(this.horizontalInput * this.speed, this.body.velocity.y);

            if ( (Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKeyDown(KeyCode.Space)) || (Input.GetKeyDown(KeyCode.W)) )
                Jump(1f);

            if ( (this.horizontalInput != 0) && (isGrounded()) )
            {
                if ( !this.audioSource.isPlaying )
                    this.audioSource.Play();
            }
            else
                this.audioSource.Stop();                

            this.ani.SetBool("Run", (this.horizontalInput != 0) && (isGrounded()));
            this.ani.SetBool("Grounded", isGrounded());
            this.ani.SetBool("Climb", ( (onWall()) && (!isGrounded()) && !( (Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKeyDown(KeyCode.Space)) || (Input.GetKeyDown(KeyCode.W)) ) ));
        }
        else
            this.wallJumpCooldown += Time.deltaTime;

        if ( this.isTransported() )
        {
            if ( this.limitedMoving != null)
            {
                if ( this.limitedMoving.verticalSpeed > 0 )
                    this.body.velocity = new Vector2(this.body.velocity.x - this.limitedMoving.horizontalSpeed, this.body.velocity.y - this.limitedMoving.verticalSpeed);
                else
                    this.body.velocity = new Vector2(this.body.velocity.x - this.limitedMoving.horizontalSpeed, this.body.velocity.y);
            }
        }
    }

    public void Jump(float _ratio)
    {
        if ( !this.doubleJump )
        {
            if ( (isGrounded()) && (!onWall()) )
            {
                this.body.velocity = new Vector2(this.body.velocity.x, this.jumpPower * _ratio);
                SoundManager.instance.PlaySound(this.jumpSound);
                this.ani.SetTrigger("Jump");

                this.doubleJump = true;
            }
            else if ( onWall() )
            {
                if  ( !isGrounded() )
                {
                    this.body.velocity = new Vector2(this.body.velocity.x, this.jumpPower * this.body.gravityScale * 1f * _ratio);
                    this.wallJumpCooldown = 0;
                    SoundManager.instance.PlaySound(this.jumpSound);
                    this.ani.SetTrigger("Jump");
                }
                else
                {
                    this.body.velocity = new Vector2(this.body.velocity.x, this.body.velocity.y + this.jumpPower * this.body.gravityScale * 0.5f *_ratio);
                    this.wallJumpCooldown = 0;
                    SoundManager.instance.PlaySound(this.jumpSound);
                    this.ani.SetTrigger("Jump");
                }
            }
        }
        else if ( this.doubleJump )
        {
            float _velocityY = ( this.body.velocity.y + this.jumpPower );
            if ( _velocityY > 10 )
                _velocityY = 10;
            else if ( _velocityY < 9)
                _velocityY = 9;

            this.body.gravityScale = 0;
            this.body.velocity = new Vector2(this.body.velocity.x, _velocityY *_ratio);
            SoundManager.instance.PlaySound(this.jumpSound);
            this.ani.SetTrigger("DoubleJump");
            this.doubleJump = false;
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(this.boxCollider2D.bounds.center, this.boxCollider2D.bounds.size, 0, new Vector2(0, -1), 0.1f, this.terrainLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(this.boxCollider2D.bounds.center, this.boxCollider2D.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, this.terrainLayer);
        return raycastHit.collider != null;
    }

    private bool isTransported()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(this.boxCollider2D.bounds.center, this.boxCollider2D.bounds.size, 0, new Vector2(0, -1), 0.1f, this.transportTerrainLayer);
        return raycastHit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if ( _collision.gameObject.tag == "Transport Terrain" )
            this.limitedMoving = _collision.gameObject.GetComponent<LimitedMoving>();
    }
}
