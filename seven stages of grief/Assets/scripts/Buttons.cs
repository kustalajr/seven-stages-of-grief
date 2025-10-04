using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public void Day_1()
    {
        SceneManager.LoadScene("Starting room");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Title_Screen");
    }
}
