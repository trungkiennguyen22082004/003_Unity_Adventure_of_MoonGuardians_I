using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    [Header ("Achievement")]
    [SerializeField] private float startingAchievement;

    [Header ("Audio")]
    [SerializeField] private AudioClip collectingSound;

    public float currentAchievement { get; private set; }

    private void Awake()
    {
        this.currentAchievement = this.startingAchievement;
    }

    public void GetAchievement(float _AchievementValue)
    {
        SoundManager.instance.PlaySound(this.collectingSound);
        this.currentAchievement = Mathf.Clamp(this.currentAchievement + _AchievementValue, 0, (3 - this.startingAchievement));
    }
}
