using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class SetCharacterBio : MonoBehaviour
{
    [SerializeField] public TMP_Text TxtBio;

    public void OnEnable()
    {
        SelectableButton.SelectedCharacter += SetBio;
    }

    public void OnDisable()
    {
        SelectableButton.SelectedCharacter -= SetBio;
    }

    void SetBio(string CharacterName, string Bio, Sprite Icon, int id)
    {
        TxtBio.text = Bio;
    }
}
