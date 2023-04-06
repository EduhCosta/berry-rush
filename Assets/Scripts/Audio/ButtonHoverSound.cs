using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonHoverSound : MonoBehaviour, ISelectHandler
{
    [SerializeField] private string soundName = "Clique";

    public void OnSelect(BaseEventData eventData)
    {
        SoundManager.instance.Play(soundName);
    }
}
