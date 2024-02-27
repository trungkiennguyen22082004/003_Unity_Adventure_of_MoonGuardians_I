using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollectible : MonoBehaviour
{
    [SerializeField] private float achievementValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<AchievementSystem>().GetAchievement(this.achievementValue);
            gameObject.SetActive(false);
        }
    }
}
