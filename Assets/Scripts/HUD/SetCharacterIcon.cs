using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SetCharacterIcon : MonoBehaviour
{
    [SerializeField] public GameObject ImageInput;
    Image image;

    public void OnEnable()
    {
        SelectableButton.SelectedCharacter += SetIcon;
    }

    public void OnDisable()
    {
        SelectableButton.SelectedCharacter -= SetIcon;
    }

    void Start()
    {
        image = ImageInput.GetComponent<Image>();
        
    }

    void SetIcon(string CharacterName, string Bio, Sprite Icon, int id)
    {
        image.sprite = Icon;
    }
}
