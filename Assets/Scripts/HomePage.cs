using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePage : MonoBehaviour
{
    public GameObject confirmationPanel;
    public bool state = false;

    private void Start()
    {
        confirmationPanel.SetActive(false);
    }

    public void Panel()
    {
        state = !state;

        if (state)
        {
            confirmationPanel.SetActive(true);
        }
        else
        {
            confirmationPanel.SetActive(false);
        }
    }

    public void LoadScene(int num)
    {
        SceneManager.LoadScene(num);
    }
}
