using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    public void StartGame(string level)
{
    SceneManager.LoadScene(level);
}

    public void GoToMenu()
    {
    
   }
}
