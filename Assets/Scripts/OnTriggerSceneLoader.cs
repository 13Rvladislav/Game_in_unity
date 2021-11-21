using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerSceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other) //��� 2D - � ����� ������, ��� ������� ����������� �� �����.
    {
        if (other.gameObject.tag == "Player") //��������� ��� �������. �������, ��� � ������ ���� ��� Player
        {
            SceneManager.LoadScene("End"); //��������� �����
        }
    }
}
