using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public Transform pauseMenu;
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.gameObject.activeSelf) 
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void Pause()
    {
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }


}
