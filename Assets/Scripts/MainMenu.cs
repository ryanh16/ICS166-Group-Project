using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void onStartButtonClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void onQuitButtonClick()
    {
        Application.Quit();
    }
}
