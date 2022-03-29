using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dead : MonoBehaviour
{
    public Canvas dead;
    
    public void Replay()
    {
        SceneManager.LoadScene("GameLevel");
    }
}
