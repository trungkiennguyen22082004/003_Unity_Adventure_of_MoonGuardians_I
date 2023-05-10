using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBulletBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private BoxCollider2D boxCollider2D;
    private Rigidbody2D body;
    [SerializeField] private GameObject piece1;
    [SerializeField] private GameObject piece2;

    private void Awake()
    {
        this.boxCollider2D = GetComponent<BoxCollider2D>();
        this.body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (this.hit) 
            return;

        this.body.velocity = new Vector2((this.speed * this.direction), 0);

        this.lifetime += Time.deltaTime;
        if (this.lifetime > 10) 
            this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D _collision) 
    {
        this.hit = true;
        this.boxCollider2D.enabled = false;
        StartCoroutine(this.Deactivate());
    }

    public void SetDirection(float _direction)
    {
        this.lifetime = 0;
        this.direction = _direction;
        
        this.gameObject.SetActive(true);
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<EnemyCollisionDamage>().enabled = true;
        this.hit = false;
        this.boxCollider2D.enabled = true;
    }

    private IEnumerator Deactivate()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<EnemyCollisionDamage>().enabled = false;
        this.piece1.GetComponent<BulletPieceBehaviour>().Activate(new Vector3(this.transform.position.x - 0.03f, this.transform.position.y, this.transform.position.z));
        this.piece2.GetComponent<BulletPieceBehaviour>().Activate(new Vector3(this.transform.position.x + 0.03f, this.transform.position.y, this.transform.position.z));
        yield return new WaitForSeconds(2f);
        this.piece1.GetComponent<BulletPieceBehaviour>().Deactivate();
        this.piece2.GetComponent<BulletPieceBehaviour>().Deactivate();
        this.gameObject.SetActive(false);
    }
}
