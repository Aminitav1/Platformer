using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{   
    //Called from start button
    public void StartGame() {
        //loads next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
