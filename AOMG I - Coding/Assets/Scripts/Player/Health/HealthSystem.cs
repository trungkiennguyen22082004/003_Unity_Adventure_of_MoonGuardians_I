using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] public float startingHealth;

    private Animator ani;
    public float currentHealth { get; private set; }
    private bool dead;

    [Header ("Invulnerable")]
    [SerializeField] private float invulnerableDuration;    
    [SerializeField] private int numberOfFlashes;

    [Header ("Audio")]
    [SerializeField] private AudioClip healingSound;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

    [Header ("UI")]
    [SerializeField] private UIManager uiManager;

    private SpriteRenderer spriteRenderer;
    private bool invulnerable;

    private void Awake()
    {
        this.currentHealth = this.startingHealth;
        this.ani = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (!this.invulnerable)
            this.currentHealth = Mathf.Clamp(this.currentHealth - _damage, 0, this.startingHealth);

        if (this.currentHealth > 0)
        {
            if (!this.invulnerable)
            {
                SoundManager.instance.PlaySound(this.hurtSound);
                this.ani.SetTrigger("Hurt");
                StartCoroutine(this.Invunerability());
            }
        }
        else
        {
            if (!(this.dead))
            {
                SoundManager.instance.PlaySound(this.deathSound);
                this.ani.SetTrigger("Disappear");
                GetComponent<PlayerMovement>().enabled = false;
                this.dead = true;
                StartCoroutine(this.DeadBehaviour());
            }
        }
    }

    private IEnumerator DeadBehaviour()
    {
        yield return new WaitForSeconds(0.45f);
        this.uiManager.GameOver();
        this.gameObject.SetActive(false);
    }

    public void Finish()
    {
        this.uiManager.FinishGame();
        this.gameObject.SetActive(false);
    }

    public void GetHealing(float _healingValue)
    {
        SoundManager.instance.PlaySound(this.healingSound);
        this.currentHealth = Mathf.Clamp(this.currentHealth + _healingValue, 0, this.startingHealth);
    }

    private IEnumerator Invunerability()
    {
        this.invulnerable = true;
        for (int i = 0; i < this.numberOfFlashes; i++)
        {
            this.spriteRenderer.color = new Color(1, 0.92f, 0.016f, 1);
            yield return new WaitForSeconds(this.invulnerableDuration / (this.numberOfFlashes * 2));
            this.spriteRenderer.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(this.invulnerableDuration / (this.numberOfFlashes * 2));
        }
        this.invulnerable = false;
    }
}
