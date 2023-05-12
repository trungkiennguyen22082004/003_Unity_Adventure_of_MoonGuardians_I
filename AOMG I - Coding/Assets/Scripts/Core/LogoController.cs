using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoController : MonoBehaviour
{
    private Rigidbody2D body;
    private BoxCollider2D boxCollider2D;

    private bool ready;
    private float timer;

    private void Awake() 
    {
        this.body = GetComponent<Rigidbody2D>();
        this.boxCollider2D = GetComponent<BoxCollider2D>();

        this.ready = false;
        this.timer = 0;
    }

    private void Update() 
    {
        if ( (this.ready) && (this.body.velocity == Vector2.zero) )
        {
            this.timer += Time.deltaTime;
            Debug.Log(timer);
        }

        if (this.timer >= 1f)
        {
            SceneManager.LoadScene(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D _collision) 
    {
        this.ready = true;
    }
}
