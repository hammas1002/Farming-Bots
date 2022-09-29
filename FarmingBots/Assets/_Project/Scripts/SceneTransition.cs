using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransition : MonoBehaviour
{


    public void LoadScene()
    {
        SceneManager.LoadScene("level01");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
