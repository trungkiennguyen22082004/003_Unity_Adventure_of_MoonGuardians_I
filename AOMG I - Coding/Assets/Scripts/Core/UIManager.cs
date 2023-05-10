using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header ("Screens")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject finishGameScreen;
    [SerializeField] private GameObject pauseScreen;

    [Header ("Audio")]
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip finishGameSound;


    private void Awake()
    {
        this.gameOverScreen.SetActive(false);
        this.finishGameScreen.SetActive(false);

        Time.timeScale = 1;
    }

    private void Update() 
    {
        if ( Input.GetKeyDown(KeyCode.Escape) )
        {
            if ( this.pauseScreen.activeInHierarchy )
                this.PauseGame(false);
            else
                if ( (!this.gameOverScreen.activeInHierarchy) && (!this.finishGameScreen.activeInHierarchy) )
                    this.PauseGame(true);
        }
    }

    public void GameOver()
    {
        this.gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(this.gameOverSound);
    }

    public void FinishGame()
    {
        this.finishGameScreen.SetActive(true);
        SoundManager.instance.PlaySound(this.finishGameSound);
    }

    public void PauseGame(bool _status)
    {
        this.pauseScreen.SetActive(_status);

        if ( _status )
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void ResumeGame()
    {
        if ( this.pauseScreen.activeInHierarchy )
            this.PauseGame(false);
    }
}
