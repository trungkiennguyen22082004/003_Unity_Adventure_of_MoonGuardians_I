using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [Header ("Player's Health Management")]
    [SerializeField] private HealthSystem playerHealth;
    [SerializeField] private Image totalHealthbar;
    [SerializeField] private Image currentHealthbar;

    private void Awake()
    {
        this.totalHealthbar.fillAmount = this.playerHealth.startingHealth / 10;
    }

    private void Update()
    {
        this.currentHealthbar.fillAmount = this.playerHealth.currentHealth / 10;
    }
}
