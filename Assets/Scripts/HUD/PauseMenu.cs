using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public Transform pauseMenu;
    [SerializeField] public GameObject eSystem;
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton9))
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
        eSystem.gameObject.SetActive(true);
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void Pause()
    {
        eSystem.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }


}
