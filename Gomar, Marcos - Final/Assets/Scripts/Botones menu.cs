using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("SampleScene");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    public void Salir()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Application.Quit();
    }
}