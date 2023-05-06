using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class SetCharacterName : MonoBehaviour
{
    [SerializeField] public TMP_Text Title;

    public void OnEnable()
    {
        SelectableButton.SelectedCharacter += SetTitle;
    }

    public void OnDisable()
    {
        SelectableButton.SelectedCharacter -= SetTitle;
    }

    void SetTitle(string CharacterName, string Bio, Sprite Icon)
    {
        Title.text = CharacterName;
    }
}
