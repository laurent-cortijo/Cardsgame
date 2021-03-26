using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ecranFin : MonoBehaviour
{
    public void recommencer()
    {

      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
    }

    public void menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
