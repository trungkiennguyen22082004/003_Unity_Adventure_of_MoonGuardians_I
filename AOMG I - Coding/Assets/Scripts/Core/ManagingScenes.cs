using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagingScenes : MonoBehaviour
{
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadOptionalScene(int _index)
    {
        SceneManager.LoadScene(_index);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadLevel1Scene()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLevel2Scene()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadLevel3Scene()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadLevel4Scene()
    {
        SceneManager.LoadScene(5);
    }
}
