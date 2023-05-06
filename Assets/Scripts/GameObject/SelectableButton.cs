using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SelectableButton : MonoBehaviour, ISelectHandler
{
    public static Action<string, string, Sprite> SelectedCharacter;

    [SerializeField] public string CharacterName;
    [TextAreaAttribute]
    [SerializeField] public string Bio;
    [SerializeField] public Sprite Icon;
    //[SerializeField] public GameObject Model;


    public void OnSelect(BaseEventData eventData)
    {
        SelectedCharacter(CharacterName, Bio, Icon);
        //Debug.Log(CharacterName);
    }

}
