using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public void goGame()
    {
        SoundManager.PlaySound("ClickMenu");
        SceneManager.LoadScene("Nivel1");
    }

    public void goOptions()
    {
        SoundManager.PlaySound("ClickMenu");
        SceneManager.LoadScene("OptionMenu");
    }

    public void goMainMenu()
    {
        SoundManager.PlaySound("ClickMenu");
        SceneManager.LoadScene("MainMenu");
    }

    public void goCredits()
    {
        SoundManager.PlaySound("ClickMenu");
        SoundManager.PlaySound("Electric");
        SceneManager.LoadScene("CreditsScene");
    }

    public void ReloadLevel()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void ExitGame()
    {


        Application.Quit();
    }


}
