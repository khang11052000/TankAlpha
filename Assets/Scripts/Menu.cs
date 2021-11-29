using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void LevelGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void HelpGame()
    {
        SceneManager.LoadScene("Help");
    }

    public void Mortal()
    {
        SceneManager.LoadScene("Main");
    }
    
    public void Distroy()
    {
        SceneManager.LoadScene("Main2");
    }

    public void PlayVsBot()
    {
        SceneManager.LoadScene("Main3");
    }
    
    public void BackButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
