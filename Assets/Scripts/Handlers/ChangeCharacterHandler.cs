using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCharacterHandler : MonoBehaviour
{
    public void OnEnable()
    {
        SelectableButton.SelectedCharacter += SetKartModel;
    }

    public void OnDisable()
    {
        SelectableButton.SelectedCharacter -= SetKartModel;
    }

    void SetKartModel(string CharacterName, string Bio, Sprite Icon, int id)
    {
        LocalStorage.SetSelectedCharacter(id);
        GetComponentInChildren<KartSelector>().CreateKart(id);
    }
}
