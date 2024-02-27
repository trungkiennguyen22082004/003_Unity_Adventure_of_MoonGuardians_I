using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievementbar : MonoBehaviour
{
    [Header ("Player's Achievement Management")]
    [SerializeField] private AchievementSystem playerAchievement;
    [SerializeField] private Image totalAchievementbar;
    [SerializeField] private Image currentAchievementbar;

    private void Awake()
    {
        this.totalAchievementbar.fillAmount = (3 - this.playerAchievement.currentAchievement) / 3;
    }

    private void Update()
    {
        this.currentAchievementbar.fillAmount = this.playerAchievement.currentAchievement / 3;
    }
}
