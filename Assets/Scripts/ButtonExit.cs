using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonExit : MonoBehaviour
{
    public void ExitMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
