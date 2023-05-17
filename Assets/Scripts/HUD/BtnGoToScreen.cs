using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnGoToScreen : MonoBehaviour
{
    [Tooltip("Nome da cena que esse botão irá navegar")]
    [SerializeField] string SceneName;

    private Button _goToScreenButton;

    private void Awake()
    {
        _goToScreenButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _goToScreenButton?.onClick.AddListener(GoTo);
    }

    private void OnDisable()
    {
        _goToScreenButton?.onClick.RemoveListener(GoTo);
    }

    /// <summary>
    /// Go to Scene by name
    /// </summary>
    private void GoTo()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneName);
    }
}
